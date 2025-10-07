using System.Globalization;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectionManager : MonoBehaviour
{
    [SerializeField] private GameObject firstStageButton;
    [SerializeField] private GameObject secondStageButton;
    [SerializeField] private GameObject thirdStageButton;
    [SerializeField] private GameObject fourthStageButton;
    [SerializeField] private GameObject fifthStageButton;
    [SerializeField] private GameObject sixthStageButton;
    [SerializeField] private GameObject stagecheckWindowObj;

    private string selectedStageName = "";//�I�𒆂̃X�e�[�W���ۑ�
    public void LoadStage(string stageName)
    {
        StageLoader.NextStageName = stageName;
        StageLoader.LastPlayedStageName = stageName;
        SceneManager.LoadScene("LoadingScene");
    }
    //�e�X�e�[�W�{�^�����Ăяo��
    public void OnStageButtonClicked(string stageName)
    {
        selectedStageName = stageName;
        stagecheckWindowObj.SetActive(true);//�`�F�b�N�E�B���h�E�\��
    }

    //�`�F�b�N�E�B���h�EYes
    public void OnCheckYes()
    {
        StageLoader.NextStageName = selectedStageName;
        StageLoader.LastPlayedStageName = selectedStageName;
        SceneManager.LoadScene("LoadingScene");
    }
    //�`�F�b�N�E�B���h�ENo
    public void OnCheckNo()
    {
        selectedStageName = "";
        stagecheckWindowObj.SetActive(false); // �L�����Z��
    }
    public void OnFirstStage()
    {
        LoadStage("MainStage0");
    }
    public void OnSecondStage()
    {
        LoadStage("MainStage1");
    }
    public void OnThirdStage()
    {
        LoadStage("MainStage2");
    }
    public void OnFourthStage()
    {
        LoadStage("MainStage3");
    }
    public void OnFifthStage()
    {
        LoadStage("MainStage4");
    }
    public void OnSixthStage()
    {
        LoadStage("MainStage5");
    }   
}
