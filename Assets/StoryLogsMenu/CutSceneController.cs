using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class CutSceneController : MonoBehaviour
{
    public TextMeshProUGUI storyText;
    public string[] sentences;

    public string gameSceneName = "SampleScene";

    private int index = 0;
    private bool storyFinished = false;

    void Start()
    {
        ShowNextSentence();

        AudioManager.Instance.PlayMusicForPanel("CutScene");
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!storyFinished)
            {
                ShowNextSentence();
            }
            else
            {
                StartGame();
            }
        }
    }

    void ShowNextSentence()
    {
        if (index < sentences.Length)
        {
            storyText.text = sentences[index];
            index++;
        }
        else
        {
            storyFinished = true;
            storyText.text = "Click to start...";
        }
    }

    void StartGame()
    {
        SceneManager.LoadScene(gameSceneName);
    }
}