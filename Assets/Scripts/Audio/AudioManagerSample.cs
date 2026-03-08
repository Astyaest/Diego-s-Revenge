using UnityEngine;

public class AudioManagerSample : MonoBehaviour
{
    public static AudioManagerSample Instance;

    [Header("Music")]
    public AudioSource gameMusic;

    [Header("Bomb")]
    public AudioClip bombExplosionClip;

    [Header("SFX")]
    public AudioClip playerHitClip;
    public AudioClip playerDeathClip;
    public AudioClip enemyHitClip;
    public AudioClip enemyAttackClip;
    public AudioClip enemyDeathClip;
    public AudioClip swordAttackClip;

    public AudioClip buttonClip;
    public AudioClip shopClip;

    [HideInInspector]
    public AudioSource sfxAudioSource;

    private bool isMuted = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            sfxAudioSource = gameObject.AddComponent<AudioSource>();
            sfxAudioSource.spatialBlend = 0; // 2D
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ToggleMute()
    {
        isMuted = !isMuted;

        if (gameMusic != null)
            gameMusic.mute = isMuted;

        if (sfxAudioSource != null)
            sfxAudioSource.mute = isMuted;
    }

    public void PlayMusic()
    {
        if (!isMuted && gameMusic != null)
        {
            gameMusic.Play();
        }
    }

    public void PlaySFX(AudioClip clip)
    {
        if (!isMuted && clip != null && sfxAudioSource != null)
        {
            sfxAudioSource.PlayOneShot(clip);
        }
    }
}
