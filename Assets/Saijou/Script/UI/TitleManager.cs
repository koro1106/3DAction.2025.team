using UnityEngine;

public class TitleManager : MonoBehaviour
{
    [SerializeField] private GameObject playButton;
    [SerializeField] private SelectionManager selectionManager;
    [SerializeField] private SEManager seManager;
    public void OnSelectionButton()
    {
        seManager.StartButtonSE();
        Invoke(nameof(LoadNextScene), 0.2f); // Å© 0.2ïbå„Ç…ÉVÅ[ÉìëJà⁄
    }
    private void LoadNextScene()
    {
        selectionManager.LoadStage("SelectionScene");
    }
}

