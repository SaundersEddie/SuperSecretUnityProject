using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    
    public AudioSource audioSource;
    public AudioClip moveSound;
    public AudioClip winSound;
    public AudioClip tieSound;
    public AudioClip buttonClickSound;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Ensure it persists across scenes
        }
        else
        {
            Destroy(gameObject); // Prevent duplicates
            return;
        }

        // Ensure AudioSource is assigned correctly
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    public void PlayMoveSound()
    {
        if (audioSource != null && moveSound != null)
            audioSource.PlayOneShot(moveSound);
    }

    public void PlayWinSound()
    {
        if (audioSource != null && winSound != null)
            audioSource.PlayOneShot(winSound);
    }

    public void PlayTieSound()
    {
        if (audioSource != null && tieSound != null)
            audioSource.PlayOneShot(tieSound);
    }

    public void PlayButtonClick()
    {
        if (audioSource != null && buttonClickSound != null)
            audioSource.PlayOneShot(buttonClickSound);
    }
}
