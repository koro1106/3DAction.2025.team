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


    public void LoadStage(string stageName)
    {
        StageLoader.NextStageName = stageName;
        StageLoader.LastPlayedStageName = stageName;
        SceneManager.LoadScene("LoadingScene");
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
