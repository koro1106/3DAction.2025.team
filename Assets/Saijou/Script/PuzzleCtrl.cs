using System.Collections;
using UnityEngine;

public class PuzzleCtrl : MonoBehaviour
{
    public Transform puzzle;//パズル本体
    public GameObject piecePrefub;//ピースのプレハブ
    GameObject[,] grid = new GameObject[3, 3];
    private Vector2Int blankPos = new Vector2Int(0,0);//空白マスの位置
  　private Vector2Int fixedPiecePos = new Vector2Int(2,0);//固定マス
    private bool isRotating = false;


    void Start()
    {
        //ピース生成して配置（0,0を空白とする）
        for (int x = 0; x < 3; x++)
        {
            for (int y = 0; y < 3; y++)
            {
                Vector2Int pos = new Vector2Int(x, y);

                if (pos == blankPos || pos == fixedPiecePos) continue;

                GameObject piece = 
                    Instantiate(piecePrefub, new Vector3(x, 0, y), Quaternion.identity, puzzle);
                grid[x, y] = piece;
            }
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            RotatePuzzle(90f);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            RotatePuzzle(-90f);
        }

    }
    void OnPuzzleRotated()
    {
        // 現状の箱の回転角度を取得
        float angleY = puzzle.eulerAngles.y;
        angleY = Mathf.Round(angleY) % 360;

        // 回転角度に応じて「下方向の配列移動ベクトル」を決定
        Vector2Int downDir;
        if (Approximately(angleY, 0)) downDir = new Vector2Int(0, -1);
        else if (Approximately(angleY, 90)) downDir = new Vector2Int(1, 0);
        else if (Approximately(angleY, 180)) downDir = new Vector2Int(0, 1);
        else if (Approximately(angleY, 270)) downDir = new Vector2Int(-1, 0);
        else downDir = new Vector2Int(0, -1); // デフォルト下

        MovePiecesDown(downDir);
    }

    bool Approximately(float a, float b, float threshold = 5f)
    {
        return Mathf.Abs(Mathf.DeltaAngle(a, b)) < threshold;
    }

    // 下方向に空白があればピースを動かすロジック例
    void MovePiecesDown(Vector2Int downDir)
    {
        bool moved;

        //何度も動かせるようにループ
        do
        {
            moved = false;

            //重力方向に合わせてループ順を決める
            for(int x = (downDir.x > 0 ? 1: 1); x >= 0 && x < 3; x += (downDir.x >= 0 ? -1 : 1))
            {
                for (int y = (downDir.y > 0 ? 1 : 1); y >= 0 && y < 3; y += (downDir.y >= 0 ? -1 : 1))
                {
                    int nx = x + downDir.x;
                    int ny = y + downDir.y;

                    if (!IsInBounds(nx, ny)) continue;

                    if (grid[x, y] != null &&
                  　　　 grid[nx, ny] == null &&
                   　　　　new Vector2Int(nx, ny) != fixedPiecePos)
                    {
                        //移動
                        grid[nx,ny] = grid[x,y];
                        grid[x, y] = null;
                        grid[nx, ny].transform.localPosition = new Vector3(nx, 0,ny);
                   
                        //空白位置更新
                        blankPos = new Vector2Int(x, y);
                        moved = true;
                    }
                }
            }

        }while(moved);

    }

    private bool IsInBounds(int x, int y)
    {
        return x >= 0 && x < 3 && y >= 0 && y < 3;
    }


   public void RotatePuzzle(float angle)
    {
        if (!isRotating)
        {
            StartCoroutine(RotatePazzle(angle));
        }
    }


    IEnumerator RotatePazzle(float angle)
    {
        isRotating = true;

        float duration = 0.5f;
        float time = 0f;
        Quaternion startRot = puzzle.rotation;
        Quaternion endRot = startRot * Quaternion.Euler(0f, angle, 0f);

        while (time < duration)
        {
            //Slerp＝開始から終了に向かってｔの割合だけ進んだ回転を返す
            puzzle.rotation = Quaternion.Slerp(startRot, endRot, time / duration);
            time += Time.deltaTime;//舞フレーム経過時間を加算
            yield return null;
        }

        puzzle.rotation = endRot;
        isRotating = false;

        //回転終了したら重力方向に従ってピース動かす
        OnPuzzleRotated();
    }
}
