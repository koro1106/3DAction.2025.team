using UnityEngine;

public class PlayerSEManager : MonoBehaviour
{
    public AudioClip touchSoundClip;  // ìGÇ…êGÇÍÇΩÇ∆Ç´ÇÃâπ
    public AudioClip landSoundClip;   // ínñ Ç…íÖínÇµÇΩÇ∆Ç´ÇÃâπ

    private AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogError("AudioSource Ç™ SEManager Ç…Ç†ÇËÇ‹ÇπÇÒÅI");
        }
    }

    public void PlayTouchSound()
    {
        if (touchSoundClip != null && audioSource != null)
        {
            audioSource.PlayOneShot(touchSoundClip);
        }
    }

    public void PlayLandSound()
    {
        if (landSoundClip != null && audioSource != null)
        {
            audioSource.PlayOneShot(landSoundClip);
        }
    }
}
