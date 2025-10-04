using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager: MonoBehaviour
{
    public GameObject checkWindowObj;
    [SerializeField] private SelectionManager selectionManager;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            checkWindowObj.SetActive(true);
        }
    }
   
    public void OnPuzzleYesButton()
    {
        selectionManager.LoadStage("MainStage1");
    }
   public void OnPuzzleNoButton()
   {
        checkWindowObj.SetActive(false);
   }

    public void OnSelectionYesButton()
    {
        selectionManager.LoadStage("SelectionScene");
    }
    public void OnSelectionNoButton()
    {
        checkWindowObj.SetActive(false);
    }
    public void OnTitleYesButton()
    {
        selectionManager.LoadStage("TitleScene");
    }
    public void OnTitleNoButton()
    {
        checkWindowObj.SetActive(false);
    }
}
