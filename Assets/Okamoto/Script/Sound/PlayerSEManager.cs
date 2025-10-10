using UnityEngine;

public class PlayerSEManager : MonoBehaviour
{
    public AudioClip touchSoundClip;  // �G�ɐG�ꂽ�Ƃ��̉�
    public AudioClip landSoundClip;   // �n�ʂɒ��n�����Ƃ��̉�

    private AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogError("AudioSource �� SEManager �ɂ���܂���I");
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
