using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEndingTrigger : MonoBehaviour
{
    //
    public void WinGame()
    {
        PlayerPrefs.SetString("GameStatus", "Victory");
        SceneManager.LoadScene("EndingScene");
    }

    //
    public void LoseGame()
    {
        PlayerPrefs.SetString("GameStatus", "GameOver");
        SceneManager.LoadScene("EndingScene");
    }
}