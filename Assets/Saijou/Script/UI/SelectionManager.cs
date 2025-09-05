using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectionManager : MonoBehaviour
{
    [SerializeField] private GameObject firstStageButton;
    [SerializeField] private GameObject titleBackButton;

    public void OnFirstStage()
    {
        SceneManager.LoadScene("MainScene");
    }
    public void OnTitleStage()
    {
        SceneManager.LoadScene("TitleScene");
    }
}
