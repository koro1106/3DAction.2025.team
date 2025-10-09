using UnityEngine;

public class TitleManager : MonoBehaviour
{
    [SerializeField] private GameObject playButton;
    [SerializeField] private SelectionManager selectionManager;
    [SerializeField] private SEManager seManager;
    public void OnSelectionButton()
    {
        seManager.StartButtonSE();
        Invoke(nameof(LoadNextScene), 0.2f); // �� 0.2�b��ɃV�[���J��
    }
    private void LoadNextScene()
    {
        selectionManager.LoadStage("SelectionScene");
    }
}

