using UnityEngine;

public class MenuManager: MonoBehaviour
{
    public GameObject checkWindowObj;
    [SerializeField] private SelectionManager selectionManager;
    [SerializeField] private SEManager seManager;//SE

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            seManager.ClickUISE();//SE
            checkWindowObj.SetActive(true);
        }
    }
    public void OnEscButton()
    {
        seManager.ClickUISE();//SE
        checkWindowObj.SetActive(true);
    }
    public void OnPuzzleYesButton()
    {
        seManager.DecideSE();//SE

        selectionManager.LoadStage(StageLoader.LastPlayedStageName);
    }
   
   public void OnPuzzleNoButton()
   {
        seManager.CancelSE();//SE

        checkWindowObj.SetActive(false);
   }

    public void OnSelectionYesButton()
    {
        seManager.DecideSE();//SE

        selectionManager.LoadStage("SelectionScene");
    }
    public void OnSelectionNoButton()
    {
        seManager.CancelSE();//SE

        checkWindowObj.SetActive(false);
    }
    public void OnTitleYesButton()
    {
        seManager.DecideSE();//SE

        selectionManager.LoadStage("TitleScene");
    }
    public void OnTitleNoButton()
    {
        seManager.CancelSE();//SE

        checkWindowObj.SetActive(false);
    }
}
