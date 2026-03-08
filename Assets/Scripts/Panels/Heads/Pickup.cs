using UnityEngine;

public class PickupHead : MonoBehaviour
{
    public int scoreValue = 1; // Сколько очков даёт при подборе

    // Можно добавить визуальный эффект при наведении/подсветке
    private void OnMouseDown()
    {
        // Проверяем, что игрок рядом (опционально)
        if (Player.Instance != null)
        {
            float distance = Vector3.Distance(Player.Instance.transform.position, transform.position);
            if (distance <= 3f) // допустим, 3 единицы
            {
                // Добавляем очки
                HeadScore.Instance.AddScore(scoreValue);

                // Убираем объект с сцены
                Destroy(gameObject);
            }
        }

        AudioManagerSample.Instance.PlaySFX(AudioManagerSample.Instance.buttonClip);
    }
}
