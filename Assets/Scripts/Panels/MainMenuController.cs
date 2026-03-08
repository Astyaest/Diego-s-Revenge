using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [Header("Panels")]
    public GameObject mainMenuPanel;
    public GameObject cutScenePanel;

    void Start()
    {
        // при старте сцены включаем музыку главного меню
        AudioManager.Instance.PlayMusicForPanel("MainMenu");
    }
    public void StartGame()
    {
        // скрываем главное меню
        mainMenuPanel.SetActive(false);

        // показываем панель катсцены
        cutScenePanel.SetActive(true);

        // ВАЖНО: включаем музыку для активной панели
        AudioManager.Instance.PlayMusicForPanel("CutScene"); // т.к. открыта катсцена
    }

    public void ExitGame()
    {
        Application.Quit();

    }
}