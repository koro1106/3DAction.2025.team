using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager: MonoBehaviour
{
    [SerializeField] private GameObject puzzleBackButton;
    
    public void OnPuzzleBack()
    {
        SceneManager.LoadScene("MainScene");
    }
}
