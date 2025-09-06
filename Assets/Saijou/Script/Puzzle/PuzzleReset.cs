using UnityEngine;

public class PuzzleReset : MonoBehaviour
{
    [SerializeField] private PuzzleCtrl puzzleCtrl;
    public void OnResetButton()
    {
        puzzleCtrl.InitializePuzzle();
    }
}
