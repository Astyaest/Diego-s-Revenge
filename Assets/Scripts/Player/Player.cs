using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[SelectionBase]
public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }
    public event EventHandler OnPlayerDeath;
    public event EventHandler OnFlashBlink;

    [SerializeField] private float movingSpeed = 10f;
    [SerializeField] private int maxHealth = 10;

    public int MaxHealth => maxHealth;
    public int CurrentHealth => _currentHealth;

    [SerializeField] private float damageRecoveryTime = 0.5f;
    [Space(20)]
    [SerializeField] private int dashSpeed = 4;
    [SerializeField] private float dashTime = 0.2f;
    [SerializeField] private TrailRenderer trailRenderer;
    [SerializeField] private float dashCoolDownTime = 0.25f;

    [Header("UI Elements")]
    [SerializeField] private RectTransform healthFill; // drag сюда HealthFill panel
    [SerializeField] private GameObject gameOverText;   // текст "Game Over"
    [Header("Bomb")]
    [SerializeField] private GameObject bombPrefab;
    [SerializeField] private float bombOffset = 0.5f;

    private Vector2 _inputVector;

    private Rigidbody2D _rb;
    private KnockBack _knockBack;

    private readonly float _minMovingSpeed = 0.1f;
    private bool _isRunning = false;

    private int _currentHealth;
    private bool _canTakeDamage;
    private bool _isAlive;
    private bool _isDashing;
    private float _initialMovingSpeed;
    
    private Camera _mainCamera;

    private void Awake()
    {
        Instance = this;
        _rb = GetComponent<Rigidbody2D>();
        _knockBack = GetComponent<KnockBack>();

        _mainCamera = Camera.main;
        
        _initialMovingSpeed  = movingSpeed;
    }

    private void Start()
    {
        _currentHealth = maxHealth;
        _canTakeDamage = true;
        _isAlive = true;
        
        GameInput.Instance.OnPlayerAttack += GameInput_OnPlayerAttack;
        GameInput.Instance.OnPlayerDash += GameInput_OnPlayerDash;

        OnPlayerDeath += (sender, e) =>
        {
            // Задержка, чтобы дождаться анимации смерти
            StartCoroutine(ShowGameOverWithDelay(1f));
        };
    }

    private IEnumerator ShowGameOverWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        GameUI.Instance.ShowGameOver();
    }

    private void Update()
    {
        _inputVector = GameInput.Instance.GetMovementVector();

        if (Input.GetKeyDown(KeyCode.Q))
        {
            UseBomb();
        }
    }


    private void FixedUpdate()
    {
        if (_knockBack.IsGettingKnockedBack)
            return;

        HandleMovement();
    }

    public bool IsAlive() => _isAlive;


    public void TakeDamage(Transform damageSource, int damage)
    {
        if (_canTakeDamage && _isAlive)
        {
            _canTakeDamage = false;
            _currentHealth = Mathf.Max(0, _currentHealth - damage);
            UpdateHealthBar();

            AudioManagerSample.Instance.PlaySFX(AudioManagerSample.Instance.playerHitClip);

            Debug.Log("Player HP: " + _currentHealth);

            _knockBack.GetKnockedBack(damageSource);
            OnFlashBlink?.Invoke(this, EventArgs.Empty);

            StartCoroutine(DamageRecoveryRoutine());
        }

        DetectDeath();
    }

    private void UpdateHealthBar()
    {
        if (healthFill != null)
        {
            float healthPercent = (float)_currentHealth / maxHealth;
            Vector3 scale = healthFill.localScale;
            scale.x = healthPercent;
            healthFill.localScale = scale;
        }
    }

    private void DetectDeath()
    {
        if (_currentHealth == 0 && _isAlive)
        {
            _isAlive = false;

            _knockBack.StopKnockBackMovement();
            GameInput.Instance.DisableMovement();

            AudioManagerSample.Instance.PlaySFX(AudioManagerSample.Instance.playerDeathClip);

            OnPlayerDeath?.Invoke(this, EventArgs.Empty);


            // Запуск корутины GameOver после анимации
            StartCoroutine(HandleDeathRoutine());
        }
    }

    private IEnumerator HandleDeathRoutine()
    {
        // Ждём время анимации смерти (в секундах)
        float deathAnimDuration = 1.5f; // <- поменяй на реальное время
        yield return new WaitForSeconds(deathAnimDuration);

        // Показываем GameOver текст
        GameUI.Instance.ShowGameOver();

        // Если нужно, можно полностью остановить игру
        Time.timeScale = 0f;
    }


    private void GameInput_OnPlayerDash(object sender, System.EventArgs e) {
        Dash();
    }

    private void Dash()
    {
        if (!_isDashing)
            StartCoroutine(DashRoutine());
    }

    private IEnumerator DashRoutine()
    {
        _isDashing  = true;
        movingSpeed *= dashSpeed;
        trailRenderer.emitting = true;
        yield return new WaitForSeconds(dashTime);
        
        trailRenderer.emitting = false;
        movingSpeed = _initialMovingSpeed;
        
        yield return new WaitForSeconds(dashCoolDownTime);
        _isDashing  = false;
    }


    private IEnumerator DamageRecoveryRoutine()
    {
        yield return new WaitForSeconds(damageRecoveryTime);
        _canTakeDamage = true;
    }

    public bool IsRunning()
    {
        return _isRunning;
    }

    private void GameInput_OnPlayerAttack(object sender, System.EventArgs e)
    {
        ActiveWeapon.Instance.GetActiveWeapon().Attack();
    }

    private void HandleMovement()
    {
        _rb.MovePosition(_rb.position + _inputVector * (movingSpeed * Time.fixedDeltaTime));
        if (Mathf.Abs(_inputVector.x) > _minMovingSpeed || Mathf.Abs(_inputVector.y) > _minMovingSpeed)
        {
            _isRunning = true;
        } else
        {
            _isRunning = false;
        }
    }

    public Vector3 GetPlayerScreenPosition()
    {
        Vector3 playerScreenPosition = _mainCamera.WorldToScreenPoint(transform.position);
        return playerScreenPosition;
    }

    private void OnDestroy()
    {
        GameInput.Instance.OnPlayerAttack -= GameInput_OnPlayerAttack;
    }

    private void ShowGameOver()
    {
        if (gameOverText != null)
        {
            gameOverText.SetActive(true);
        }
        Time.timeScale = 0f; // останавливает игру
    }

    public void Heal(int amount)
    {
        _currentHealth = Mathf.Min(_currentHealth + amount, maxHealth);
        UpdateHealthBar(); // Обновляем UI полоски здоровья
    }
    private void UseBomb()
    {
        if (ShopController.Instance.bombScore <= 0)
            return;

        ShopController.Instance.bombScore--;

        Vector3 spawnPos = transform.position + Vector3.right * bombOffset;

        Instantiate(bombPrefab, spawnPos, Quaternion.identity);
    }
}
