using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    [SerializeField] private GameObject playButton;

    public void OnPlayButton()
    {
        SceneManager.LoadScene("SelectionScene");
    }
}

