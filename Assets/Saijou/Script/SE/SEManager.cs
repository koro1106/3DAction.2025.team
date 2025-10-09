using UnityEngine;

public class SEManager : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip clickStartButtonSE;
    public AudioClip clickUISE;
    public AudioClip decideSE;
    public AudioClip cancelSE;

    public void StartButtonSE()
    {
        audioSource.PlayOneShot(clickStartButtonSE);
    }
    public void ClickUISE()
    {
        audioSource.PlayOneShot(clickUISE);
    }
    public void DecideSE()
    {
        audioSource.PlayOneShot(decideSE);
    }
    public void CancelSE()
    {
        audioSource.PlayOneShot(cancelSE);
    }
}
