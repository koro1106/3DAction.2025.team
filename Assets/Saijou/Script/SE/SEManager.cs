using UnityEngine;

public class SEManager : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip clickStartButtonSE;
    public AudioClip clickUISE;
    public AudioClip decideSE;
    public AudioClip cancelSE;

    // Puzzle
    public AudioClip puzzleResetSE;
    public AudioClip puzzleRotateSE;
    public AudioClip puzzleFinishSE;

    // Gimmick
    public AudioClip springSE;
    public AudioClip crashBrockSE;
    public AudioClip goalSE;
    public AudioClip doorSE;
    public AudioClip clearDoorSE;

    // Player
    public AudioClip playerDamageSE;
    public AudioClip playerDestroySE;


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
    public void ClearDoorSE() //パズルシ-ンクリアドアSE
    {
        audioSource.PlayOneShot(clearDoorSE);
    }
    public void PlayerDamageSE() //プレイヤーダメージSE
    {
        audioSource.PlayOneShot(playerDamageSE);
    }
    public void PlayerDestroySE() //プレイヤー死亡SE
    {
        audioSource.PlayOneShot(playerDestroySE);
    }
    
}
