using UnityEngine;
using System.Collections;
using UnityEngine;

public class PlayerBomb : MonoBehaviour
{
    public GameObject bombPrefab;
    public Transform throwPoint;
    public float explosionDelay = 5f;
    public float explosionRadius = 8f;
    public int explosionDamage = 8;
    public KeyCode throwKey = KeyCode.Q;   // Клавиша броска

    private void Update()
    {
        if (Input.GetKeyDown(throwKey))
        {
            TryThrowBomb();
        }
    }

    private void TryThrowBomb()
    {
        if (BombCounter.Instance == null || BombCounter.Instance.bombCount <= 0)
            return;

        if (bombPrefab == null || throwPoint == null)
            return;

        // Создаём
        GameObject bomb = Instantiate(bombPrefab, throwPoint.position, Quaternion.identity);

        // Уменьшаем счетчик
        BombCounter.Instance.AddBomb(-1);

        // Проигрываем звук 
        AudioManagerSample.Instance.PlaySFX(AudioManagerSample.Instance.bombExplosionClip);

        bomb.GetComponent<Bomb>().Initialize(explosionDelay, explosionRadius, explosionDamage);
    }
}