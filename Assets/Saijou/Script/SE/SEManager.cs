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


    public void StartButtonSE()�@//�X�^�[�g�{�^��SE
    {
        audioSource.PlayOneShot(clickStartButtonSE);
    }
    public void ClickUISE()�@//UI�N���b�NSE
    {
        audioSource.PlayOneShot(clickUISE);
    }
    public void DecideSE()�@//����SE
    {
        audioSource.PlayOneShot(decideSE);
    }
    public void CancelSE()�@//�L�����Z��SE
    {
        audioSource.PlayOneShot(cancelSE);
    }
    public void PuzzleResetSE() //�p�Y�����Z�b�gSE
    {
        audioSource.PlayOneShot(puzzleResetSE);
    }
    public void PuzzleRotateSE() //�p�Y����]SE
    {
        audioSource.PlayOneShot(puzzleRotateSE);
    }
    public void PuzzleFinishSE() //�p�Y������SE
    {
        audioSource.PlayOneShot(puzzleRotateSE);
    }
    public void SpringSE() //�΂�SE
    {
        audioSource.PlayOneShot(springSE);
    }
    public void GoalSE() //�S�[��SE
    {
        audioSource.PlayOneShot(goalSE);
    }
    public void DoorSE() //�p�Y���V�[���ڍsSE
    {
        audioSource.PlayOneShot(doorSE);
    }
    public void ClearDoorSE() //�p�Y���V-���N���A�h�ASE
    {
        audioSource.PlayOneShot(clearDoorSE);
    }
    public void PlayerDamageSE() //�v���C���[�_���[�WSE
    {
        audioSource.PlayOneShot(playerDamageSE);
    }
    public void PlayerDestroySE() //�v���C���[���SSE
    {
        audioSource.PlayOneShot(playerDestroySE);
    }
    
}
