using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectionManager : MonoBehaviour
{
    [SerializeField] private GameObject firstStageButton;

    public void OnFirstStage()
    {
        SceneManager.LoadScene("MainScene");
    }
}
