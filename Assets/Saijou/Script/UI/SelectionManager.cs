using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectionManager : MonoBehaviour
{
    [SerializeField] private GameObject firstStageButton;
    [SerializeField] private GameObject titleBackButton;

    public void OnFirstStage()
    {
        SceneManager.LoadScene("MainStage1");
    }
    public void OnSecondStage()
    {
        SceneManager.LoadScene("MainStage2");
    }
    public void OnThirdStage()
    {
        SceneManager.LoadScene("MainStage3");
    }
    public void OnFourthStage()
    {
        SceneManager.LoadScene("MainStage4");
    }
    public void OnFifthStage()
    {
        SceneManager.LoadScene("MainStage5");
    }
    public void OnSixthStage()
    {
        SceneManager.LoadScene("MainStage6");
    }
    public void OnTitleStage()
    {
        SceneManager.LoadScene("TitleScene");
    }
}
