using System.Collections;
using System;
using UnityEngine;

public class PuzzleCtrl : MonoBehaviour
{
    public Transform puzzle;
    private bool isRotating = false;
    public GameObject piecePrefub;
    GameObject[,] grid = new GameObject[3, 3];

    void Start()
    {
        for (int x = 0; x < 3; x++)
        {
            for (int y = 0; y < 3; y++)
            {
                if (x == 0 && y == 0) continue;//空白マス
                GameObject piece = Instantiate(piecePrefub);
                piece.transform.position = new Vector3(x, 0, y);
                grid[x, y] = piece;
            }
        }
    }
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
    // 回転したあとに呼ぶ例（回転完了後）
    void OnPuzzleRotated()
    {
        // 現状の箱の回転角度を取得
        float angleY = puzzle.eulerAngles.y;

        // 回転角度に応じて「下方向の配列移動ベクトル」を決定
        Vector2Int downDir;
        if (Approximately(angleY, 0)) downDir = new Vector2Int(0, -1);
        else if (Approximately(angleY, 90)) downDir = new Vector2Int(1, 0);
        else if (Approximately(angleY, 180)) downDir = new Vector2Int(0, 1);
        else if (Approximately(angleY, 270)) downDir = new Vector2Int(-1, 0);
        else downDir = new Vector2Int(0, -1); // デフォルト下

        TryMovePiecesDown(downDir);
    }

    bool Approximately(float a, float b, float threshold = 5f)
    {
        return Mathf.Abs(a - b) < threshold;
    }

    // 下方向に空白があればピースを動かすロジック例
    void TryMovePiecesDown(Vector2Int downDir)
    {
        // 例えば、空白の位置を空X,空Yとして
        int blankX = 0;
        int blankY = 0;

        // 下方向に隣接するピースがあれば、空白に移動
        int targetX = blankX + downDir.x;
        int targetY = blankY + downDir.y;

        if (IsInBounds(targetX, targetY) && grid[targetX, targetY] != null)
        {
            // 移動処理
            GameObject piece = grid[targetX, targetY];
            grid[blankX, blankY] = piece;
            grid[targetX, targetY] = null;

            // 座標移動（ワールド座標）
            piece.transform.position = new Vector3(blankX, 0, blankY);

            // 空白位置更新
            blankX = targetX;
            blankY = targetY;
        }
    }

    private bool IsInBounds(int targetX, int targetY)
    {
        return targetX >= 0 && targetX < 3 && targetY >= 0 && targetY < 3;
    }


    public void RotateRight()
    {
        if (!isRotating) StartCoroutine(RotatePazzle(90f));
        //puzzle.Rotate(0f,0f,90f);
    }
    public void RotateLeft()
    {
        if (!isRotating) StartCoroutine(RotatePazzle(-90f));
        //puzzle.Rotate(0f, 0f, -90f);
    }


    IEnumerator RotatePazzle(float angle)
    {
        isRotating = true;

        float duration = 0.5f;
        float time = 0f;
        Quaternion startRot = puzzle.rotation;
        Quaternion endRot = startRot * Quaternion.Euler(0f, 0f, angle);

        while (time < duration)
        {
            //Slerp＝開始から終了に向かってｔの割合だけ進んだ回転を返す
            puzzle.rotation = Quaternion.Slerp(startRot, endRot, time / duration);
            time += Time.deltaTime;//舞フレーム経過時間を加算
            yield return null;
        }

        puzzle.rotation = endRot;
        isRotating = false;

        OnPuzzleRotated();
    }
}
