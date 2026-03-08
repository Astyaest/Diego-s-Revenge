using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopController : MonoBehaviour
{
    public static ShopController Instance;

    [Header("Shop UI")]
    public GameObject shopPanel;
    public Button healthButton;
    public Button bombButton;
    public Button closeButton;
    public TextMeshProUGUI feedbackText;

    [Header("Bombs")]
    public int bombScore = 0;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        shopPanel.SetActive(false);
        if (feedbackText != null)
            feedbackText.gameObject.SetActive(false);

        if (healthButton != null)
            healthButton.onClick.AddListener(OnHealthButton);
        if (bombButton != null)
            bombButton.onClick.AddListener(OnBombButton);
        if (closeButton != null)
            closeButton.onClick.AddListener(CloseShop);
    }


    public void OpenShop()
    {
        if (shopPanel != null)
            shopPanel.SetActive(true);
    }

    private void CloseShop()
    {
        if (shopPanel != null)
            shopPanel.SetActive(false);
    }

    // Кнопка Health
    private void OnHealthButton()
    {
        bool purchased = false;


        while (HeadScore.Instance.score >= 2 && Player.Instance.CurrentHealth < Player.Instance.MaxHealth)
        {
            HeadScore.Instance.AddScore(-2);    // Списываем 2 головы
            Player.Instance.Heal(1);            // Добавляем 1 здоровье
            purchased = true;
        }

        AudioManagerSample.Instance.PlaySFX(AudioManagerSample.Instance.buttonClip);

        StartCoroutine(ShowFeedback(purchased));
    }

    // Кнопка Bomb
    private void OnBombButton()
    {
        if (HeadScore.Instance.score >= 4)
        {
            HeadScore.Instance.AddScore(-4);

            // Увеличиваем количество бомб и обновляем счетчик UI
            bombScore += 1; // внутренний счетчик для логики
            BombCounter.Instance.AddBomb(1);

            StartCoroutine(ShowFeedback(true));
            AudioManagerSample.Instance.PlaySFX(AudioManagerSample.Instance.buttonClip);
        }
        else
        {
            StartCoroutine(ShowFeedback(false));
        }
    }

    // Показ текста feedback ("good" или "bad") на 2 секунды
    private IEnumerator ShowFeedback(bool success)
    {
        if (feedbackText == null)
            yield break;

        feedbackText.text = success ? "Good" : "Not enough heads";
        feedbackText.gameObject.SetActive(true);

        yield return new WaitForSeconds(2f);

        feedbackText.gameObject.SetActive(false);
    }


    private void OnMouseDown()
    {
        OpenShop();

        AudioManagerSample.Instance.PlaySFX(AudioManagerSample.Instance.shopClip);
    }
}