using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    void Start()
    {
        // Hides the cursor in the gameplay scene
        Cursor.visible = false;

        // Locks the cursor to the center of the screen
        Cursor.lockState = CursorLockMode.Locked;

        AudioManagerSample.Instance.PlayMusic();
    }
}