using UnityEngine;
using TMPro;

public class BombCounter : MonoBehaviour
{
    public static BombCounter Instance;
    public int bombCount = 0;
    public TextMeshProUGUI bombText;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void AddBomb(int amount)
    {
        bombCount += amount;
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (bombText != null)
            bombText.text = bombCount.ToString();
    }
}