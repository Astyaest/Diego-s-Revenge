using UnityEngine;
using TMPro;

public class HeadScore : MonoBehaviour
{
    public static HeadScore Instance;

    public int score = 0;
    public TextMeshProUGUI scoreText; // —сылка на UI текст

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void AddScore(int amount)
    {
        score += amount;
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (scoreText != null)
            scoreText.text = score.ToString();
    }
}
