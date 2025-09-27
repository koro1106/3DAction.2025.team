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
    public void OnTitleStage()
    {
        SceneManager.LoadScene("TitleScene");
    }
}
