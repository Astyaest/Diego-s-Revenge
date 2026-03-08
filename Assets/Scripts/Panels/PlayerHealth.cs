using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;

    public RectTransform healthFill;
    private float startWidth;

    public GameObject gameOverText;

    void Start()
    {
        currentHealth = maxHealth;
        startWidth = healthFill.sizeDelta.x;

        gameOverText.SetActive(false);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth < 0)
            currentHealth = 0;

        UpdateBar();

        if (currentHealth == 0)
            GameOver();
    }

    void UpdateBar()
    {
        float percent = (float)currentHealth / maxHealth;

        healthFill.sizeDelta = new Vector2(startWidth * percent, healthFill.sizeDelta.y);
    }

    void GameOver()
    {
        gameOverText.SetActive(true);

        Time.timeScale = 0f;
    }
}