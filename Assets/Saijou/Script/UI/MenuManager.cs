using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager: MonoBehaviour
{
    public GameObject checkWindowObj;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            checkWindowObj.SetActive(true);
        }
    }
   
    public void OnPuzzleYesButton()
    {
        SceneManager.LoadScene("MainStage1");
    }
   public void OnPuzzleNoButton()
   {
        checkWindowObj.SetActive(false);
   }

    public void OnSelectionYesButton()
    {
        SceneManager.LoadScene("SelectionScene");
    }
    public void OnSelectionNoButton()
    {
        checkWindowObj.SetActive(false);
    }
    public void OnTitleYesButton()
    {
        SceneManager.LoadScene("TitleScene");
    }
    public void OnTitleNoButton()
    {
        checkWindowObj.SetActive(false);
    }
}
