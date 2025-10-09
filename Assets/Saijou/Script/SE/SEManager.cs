using UnityEngine;

public class SEManager : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip clickStartButtonSE;
    public AudioClip clickUISE;
    public AudioClip decideSE;
    public AudioClip cancelSE;

    public AudioClip puzzleResetSE;
    public AudioClip puzzleRotateSE;
    public AudioClip puzzleFinishSE;

    public AudioClip springSE;
    public AudioClip crashBrockSE;
    public AudioClip goalSE;
    public AudioClip doorSE;
    public AudioClip clearDoorSE;


    public void StartButtonSE()　//スタートボタンSE
    {
        audioSource.PlayOneShot(clickStartButtonSE);
    }
    public void ClickUISE()　//UIクリックSE
    {
        audioSource.PlayOneShot(clickUISE);
    }
    public void DecideSE()　//決定SE
    {
        audioSource.PlayOneShot(decideSE);
    }
    public void CancelSE()　//キャンセルSE
    {
        audioSource.PlayOneShot(cancelSE);
    }
    public void PuzzleResetSE() //パズルリセットSE
    {
        audioSource.PlayOneShot(puzzleResetSE);
    }
    public void PuzzleRotateSE() //パズル回転SE
    {
        audioSource.PlayOneShot(puzzleRotateSE);
    }
    public void PuzzleFinishSE() //パズル完成SE
    {
        audioSource.PlayOneShot(puzzleRotateSE);
    }
    public void SpringSE() //ばねSE
    {
        audioSource.PlayOneShot(springSE);
    }
    public void GoalSE() //ゴールSE
    {
        audioSource.PlayOneShot(goalSE);
    }
    public void DoorSE() //パズルシーン移行SE
    {
        audioSource.PlayOneShot(doorSE);
    }
    public void ClearDoorSE() //パズルシーン移行SE
    {
        audioSource.PlayOneShot(clearDoorSE);
    }
    
}
