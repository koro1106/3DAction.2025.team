using System.Collections;
using UnityEngine;

public class PuzzleCtrl : MonoBehaviour
{
    public Transform puzzle; // パズル本体
    public GameObject piecePrefub; // ピースのプレハブ
    public GameObject fixedPrefub;  // 固定ピース
    GameObject[,] grid = new GameObject[3, 3];
    private Vector2Int blankPos = new Vector2Int(0, 2); // 空白マスの位置
    private Vector2Int fixedPiecePos = new Vector2Int(2, 2); // 固定マス
    private bool isRotating = false;

    void Start()
    {
        // ピース生成して配置（0,0を空白とする）
        for (int x = 0; x < 3; x++)
        {
            for (int y = 0; y < 3; y++)
            {
                Vector2Int pos = new Vector2Int(x, y);

                if (pos == blankPos || pos == fixedPiecePos) continue; // 空白や固定ピースをスキップ

                // ピースを整数座標で生成
                Vector2Int piecePos = new Vector2Int(x, y);  
                GameObject piece = Instantiate(piecePrefub, new Vector3(piecePos.x, piecePos.y, 0), Quaternion.identity, puzzle);
                grid[x, y] = piece;
            }
        }

        // 固定ピース生成
        Vector2Int fixedPos = new Vector2Int(fixedPiecePos.x, fixedPiecePos.y);
        Instantiate(fixedPrefub, new Vector3(fixedPos.x, fixedPos.y, 0), Quaternion.identity, puzzle);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            RotatePuzzle(90f); // 左回転
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            RotatePuzzle(-90f); // 右回転
        }
    }

    void OnPuzzleRotated()
    {
        // 現状の箱の回転角度を取得
        float angleZ = puzzle.eulerAngles.z; // 2DなのでZ軸回りの回転だけを考慮する
        angleZ = Mathf.Round(angleZ) % 360;

        // 回転角度に応じて「下方向の配列移動ベクトル」を決定
        Vector2Int downDir;
        if (Approximately(angleZ, 0)) downDir = new Vector2Int(0, -1); // 下
        else if (Approximately(angleZ, 90)) downDir = new Vector2Int(-1, 0); // 右
        else if (Approximately(angleZ, 180)) downDir = new Vector2Int(0, 1); // 上
        else if (Approximately(angleZ, 270)) downDir = new Vector2Int(1, 0); // 左
        else downDir = new Vector2Int(0, -1); // デフォルト下

        MovePiecesDown(downDir); // ピースを移動させる
    }

    bool Approximately(float a, float b, float threshold = 1f) // より厳密な比較
    {
        return Mathf.Abs(Mathf.DeltaAngle(a, b)) < threshold;
    }

    void MovePiecesDown(Vector2Int downDir)
    {
        bool moved;
        float moveSpeed = 3f;

        do
        {
            moved = false;

            // 重力方向に合わせてループ順を決める
            for (int x = 0; x < 3; x++) // x軸はそのままループ
            {
                for (int y = 0; y < 3; y++) // y軸もそのままループ
                {
                    int nx = x + downDir.x;
                    int ny = y + downDir.y;

                    if (!IsInBounds(nx, ny)) continue;

                    // ピースが存在し、移動先が空白で、移動先が固定ピースでない場合
                    if (grid[x, y] != null &&
                        grid[nx, ny] == null &&
                        new Vector2Int(nx, ny) != fixedPiecePos)
                    {
                        // ピースの位置を更新
                        grid[nx, ny] = grid[x, y];
                        grid[x, y] = null;

                        // 新しい位置にピースを移動
                        //Vector3 newPosition = new Vector3(nx,ny, 0);
                        //grid[nx, ny].transform.localPosition = newPosition;
                        Vector3 startPos = grid[nx, ny].transform.localPosition;
                        Vector3 targetPos = new Vector3(nx, ny, 0);

                        StartCoroutine(MovePieceSmoothly(grid[nx, ny], startPos, targetPos, moveSpeed));

                        // 空白位置を更新
                        blankPos = new Vector2Int(x, y);
                        moved = true;
                    }
                }
            }
        } while (moved); // ピースが移動する限りループ
    }

    private bool IsInBounds(int x, int y)
    {
        return x >= 0 && x < 3 && y >= 0 && y < 3;
    }

    public void RotatePuzzle(float angle)
    {
        if (!isRotating)
        {
            StartCoroutine(RotatePuzzleCoroutine(angle));
        }
    }

    IEnumerator RotatePuzzleCoroutine(float angle)
    {
        isRotating = true;

        float duration = 0.5f;
        float time = 0f;
        Quaternion startRot = puzzle.rotation;
        Quaternion endRot = startRot * Quaternion.Euler(0f, 0f, angle); // Z軸回りのみ

        //回転軸（1,1,1）に
       // Vector3 rotationCenter = new Vector3(1, 1, 1);

        while (time < duration)
        {
            // Slerp＝開始から終了に向かってtの割合だけ進んだ回転を返す
            puzzle.rotation = Quaternion.Slerp(startRot, endRot, time / duration);
            time += Time.deltaTime; // 毎フレーム経過時間を加算
            yield return null;
        }

        puzzle.rotation = endRot;
        isRotating = false;

        // 回転終了したら重力方向に従ってピース動かす
        OnPuzzleRotated();
    }

    IEnumerator MovePieceSmoothly(GameObject piece, Vector3 startPos, Vector3 targetPos, float moveSpeed)
    {
        float distance = Vector3.Distance(startPos, targetPos);//移動距離
        float startTime = Time.time;//移動開始時間

        while(Vector3.Distance(piece.transform.localPosition, targetPos) > 0.05f)//少しずつ近づける
        {
            float dis = (Time.time - startTime) * moveSpeed;//移動した距離
            float percentage = dis / distance;//移動の進捗割合

            piece.transform.localPosition = Vector3.Lerp(startPos, targetPos, percentage);

            yield return null;
        }
        piece.transform.localPosition = targetPos;//目標位置に設定
    }
}
