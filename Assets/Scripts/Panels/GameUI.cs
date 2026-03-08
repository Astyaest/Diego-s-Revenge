using UnityEngine;
using TMPro;

public class GameUI : MonoBehaviour
{
    public static GameUI Instance { get; private set; }

    [Header("Game Over")]
    public GameObject gameOverText;

    private void Awake()
    {
        Instance = this;
        gameOverText.SetActive(false); // Скрываем в начале
    }

    public void ShowGameOver()
    {
        gameOverText.SetActive(true);
    }
}
