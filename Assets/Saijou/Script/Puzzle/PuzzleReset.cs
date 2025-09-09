using UnityEngine;

public class PuzzleReset : MonoBehaviour
{
    [SerializeField] private PuzzleCtrl puzzleCtrl;
    void Update()
    {
    if (Input.GetKeyDown(KeyCode.R))
       {
           puzzleCtrl.InitializePuzzle();
       }
    }

}
