using UnityEngine;

public class GameMusicStarter : MonoBehaviour
{
    void Start()
    {
        // ¬ключаем музыку игры при старте SampleScene
        AudioManagerSample.Instance.PlayMusic();
    }
}