using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    [SerializeField] private GameObject playButton;
    [SerializeField] private SelectionManager selectionManager;

    public void OnSelectionButton()
    {
        selectionManager.LoadStage("SelectionScene");
    }
}

