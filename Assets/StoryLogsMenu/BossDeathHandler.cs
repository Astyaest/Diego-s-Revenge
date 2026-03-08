using UnityEngine;
using UnityEngine.SceneManagement;

public class BossDeathHandler : MonoBehaviour
{
    private EnemyAI enemyAI;
    private bool victoryTriggered = false;

    void Start()
    {
        enemyAI = GetComponent<EnemyAI>();
    }

    void Update()
    {
        // Check if the enemyAI state has changed to Death
        if (enemyAI != null && enemyAI.IsDead && !victoryTriggered)
        {
            victoryTriggered = true;
            Invoke("LoadVictoryScene", 1.5f); // 1.5 second delay for death animation
        }
    }

    private void LoadVictoryScene()
    {
        Debug.Log("Victory! Loading Ending Scene...");
        PlayerPrefs.SetString("GameStatus", "Victory");
        SceneManager.LoadScene("EndingScene");
    }
}