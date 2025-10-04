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
        SceneManager.LoadScene("LoadingScene");
    }
    public void OnFirstStage()
    {
        LoadStage("MainStage1");
    }
    public void OnSecondStage()
    {
        LoadStage("MainStage2");
    }
    public void OnThirdStage()
    {
        LoadStage("MainStage3");
    }
    public void OnFourthStage()
    {
        LoadStage("MainStage4");
    }
    public void OnFifthStage()
    {
        LoadStage("MainStage5");
    }
    public void OnSixthStage()
    {
        LoadStage("MainStage6");
    }   
}
