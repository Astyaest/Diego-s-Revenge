using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndGameManager : MonoBehaviour
{
    public Image displayImage;
    public Sprite victoryBackground;
    public Sprite gameOverBackground;
    public AudioClip victoryMusic;
    public AudioClip gameOverMusic;
    public GameObject rainEffect;
    public GameObject winEffect;

    void Start()
    {
        
        displayImage = GameObject.Find("Background").GetComponent<Image>();
        AudioSource audioSource = GetComponent<AudioSource>();

        string status = PlayerPrefs.GetString("GameStatus", "None");

        if (status == "Victory")
        {
            displayImage.sprite = victoryBackground;
            if (victoryMusic != null) audioSource.PlayOneShot(victoryMusic);
            if (winEffect != null) winEffect.SetActive(true);
        }
        else
        {
            displayImage.sprite = gameOverBackground;
            if (gameOverMusic != null) audioSource.PlayOneShot(gameOverMusic);
            if (rainEffect != null) rainEffect.SetActive(true);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M)) SceneManager.LoadScene("MainMenu");
    }
}