using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    [SerializeField] private GameObject backSelectionButton;

    public void OnBackSelectionButton()
    {
        SceneManager.LoadScene("SelectionScene");
    }
}
