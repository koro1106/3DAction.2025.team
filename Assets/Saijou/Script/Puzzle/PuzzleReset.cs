using UnityEngine;

public class PuzzleReset : MonoBehaviour
{
    [SerializeField] private PuzzleCtrl puzzleCtrl;
    [SerializeField] private SEManager seManager;
    void Update()
    {
    if (Input.GetKeyDown(KeyCode.R))
       {
            seManager.PuzzleResetSE();//SE
           puzzleCtrl.InitializePuzzle();
       }
    }

}
