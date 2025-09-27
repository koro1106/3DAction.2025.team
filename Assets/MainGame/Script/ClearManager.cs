using UnityEngine;
using UnityEngine.SceneManagement;

public class ClearManager : MonoBehaviour
{
    [SerializeField] private GameObject backSelectionButton;

    void Update()
    {
        // Enterキーが押されたらシーン移動
        if (Input.GetKeyDown(KeyCode.Return))
        {
            SceneManager.LoadScene("SelectionScene");
        }
    }

    public void OnBackSelectionButton()
    {
        SceneManager.LoadScene("SelectionScene");
    }
}
