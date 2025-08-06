using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleCtrl : MonoBehaviour
{
    public Transform puzzle;
    private bool isRotating = false;
   
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            RotateRight();
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            RotateLeft();
        }
    }
    public void RotateRight()
    {
        puzzle.Rotate(0f,0f,90f);
    }
    public void RotateLeft()
    {
        puzzle.Rotate(0f, 0f, -90f);
    }

    IEnumerator RotatePazzle(float angle)
    {
        isRotating = true;

        float duration = 0.5f;
        yield return null;
    }
}
