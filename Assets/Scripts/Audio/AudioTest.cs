using UnityEngine;

public class AudioTest : MonoBehaviour
{
    public AudioSource testSource;

    void Start()
    {
        if (testSource != null)
        {
            testSource.Play(); // музыка или SFX сразу при старте
            Debug.Log("AudioTest: звук должен играть");
        }
    }
}
