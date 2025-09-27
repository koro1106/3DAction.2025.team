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
   
    public void OnYesButton()
    {
        SceneManager.LoadScene("MainStage1");
    }
   public void OnNoButton()
    {
        checkWindowObj.SetActive(false);
    }
}
