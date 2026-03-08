using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Music")]
    public AudioSource mainMenuMusic;
    public AudioSource cutSceneMusic;

    private bool isMuted = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ToggleMute()
    {
        isMuted = !isMuted;
        mainMenuMusic.mute = isMuted;
        cutSceneMusic.mute = isMuted;
    }

    public void PlayMusicForPanel(string panelName)
    {
        mainMenuMusic.Stop();
        cutSceneMusic.Stop();

        switch (panelName)
        {
            case "MainMenu":
                if (mainMenuMusic != null) mainMenuMusic.Play();
                break;
            case "CutScene":
                if (cutSceneMusic != null) cutSceneMusic.Play();
                break;
        }
    }
}