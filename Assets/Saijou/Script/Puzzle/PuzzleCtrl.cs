using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.Timeline;
using static UnityEditor.PlayerSettings;

public class PuzzleCtrl : MonoBehaviour
{
    public Transform puzzle; // パズル本体
    public GameObject piecePrefub; // ピースのプレハブ
    public GameObject fixedPrefub;  // 固定ピース
    public UnityEngine.UI.Slider progressSlider;//進捗度ゲージ

    GameObject[,] grid = new GameObject[3, 3];
    private Vector2Int blankPos = new Vector2Int(0, 0); // 空白マスの位置
    private Vector2Int fixedPiecePos = new Vector2Int(2, 0); // 固定マス
    private bool isRotating = false;

    private int[] currentPieceOrder; //現在の初期配置
    public int difficulty = 0; //難易度 0?3

   [SerializeField] private PlayableDirector playableDirector;
   [SerializeField] private GameObject UI;
   [SerializeField] private GameObject TimeLine;

    void Start()
    {
        InitializePuzzle();//初期化処理
        UpdateProgress();//進捗度ゲージ更新
    }

    //パズルのパターン
    private int[] GetPatternByDifficulty(int level)
    {
        switch (level)
        {
            case 0:
                return new int[] { 2, 0, 5, 3, 1, 4, 6 }; //簡単
            case 1:
                return new int[] { 6, 5, 4, 3, 2, 1, 0 }; //難しめ
            case 2:
                return new int[] { 1, 4, 0, 2, 5, 6, 3 }; //ランダム1
            case 3:
                return new int[] { 3, 1, 6, 0, 5, 4, 2 }; //ランダム2
            default:
                return new int[] { 0, 1, 2, 3, 4, 5, 6 }; //デフォルト
        }
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
        if (Approximately(angleZ, 0)) downDir = new Vector2Int(0, 1); // 下
        else if (Approximately(angleZ, 90)) downDir = new Vector2Int(-1, 0); // 右
        else if (Approximately(angleZ, 180)) downDir = new Vector2Int(0, -1); // 上
        else if (Approximately(angleZ, 270)) downDir = new Vector2Int(1, 0); // 左
        else downDir = new Vector2Int(0, -1); // デフォルト下

        MovePiecesDown(downDir); // ピースを移動させる
        StartCoroutine(DelayedCheckClear());
    }

    IEnumerator DelayedCheckClear()
    {
        yield return new WaitForSeconds(0.6f); // 移動が終わるのを少し待つ

        //進捗度ゲージ更新
        UpdateProgress();
        if (CheckClear())
        {
            Debug.Log("クリア！");
            UI.SetActive(false);
            TimeLine.SetActive(true);
            if (playableDirector != null)
            {
                playableDirector.Play();  // Timeline再生開始
            }
            if (Input.GetKeyDown(KeyCode.KeypadEnter))
            {
                SceneManager.LoadScene("MainStage1");
            }
        }
    }
    public void InitializePuzzle()//初期化処理
    {
        //ボトル以外の子オブジェクト全削除（リセットのため）
        foreach (Transform child in puzzle)
        {
            if (child.gameObject.CompareTag("Bottle"))
            {
                continue;//ボトル消さない
            }
            Destroy(child.gameObject);
        }

        //グリッドと空白位置の初期化
        grid = new GameObject[3, 3];
        blankPos = new Vector2Int(0, 0);
        puzzle.localRotation = Quaternion.identity; //パズル回転をリセット

        // 空白.固定を除いたポジションリスト作成
        List<Vector2Int> availablePositions = new List<Vector2Int>();
        for (int y = 0; y < 3; y++)
        {
            for (int x = 0; x < 3; x++)
            {
                Vector2Int pos = new Vector2Int(x, y);
                if (pos == blankPos || pos == fixedPiecePos) continue;
                availablePositions.Add(pos);
            }
        }

        // ピースIDの並びを難易度で決定
        currentPieceOrder = GetPatternByDifficulty(difficulty);

        // ピース生成
        for (int i = 0; i < availablePositions.Count; i++)
        {
            Vector2Int pos = availablePositions[i];
            Vector3 localPos = new Vector3(pos.x - 1, -(pos.y - 1), 0);
            GameObject piece = Instantiate(piecePrefub, localPos, Quaternion.identity, puzzle);

            PuzzlePiece pp = piece.GetComponent<PuzzlePiece>();
            pp.pieceID = currentPieceOrder[i]; // IDを割り当て

            grid[pos.x, pos.y] = piece;
        }

        // 固定ピース生成
        Vector3 fixedLocalPos = new Vector3(fixedPiecePos.x - 1, -(fixedPiecePos.y - 1), 0);
        Instantiate(fixedPrefub, fixedLocalPos, Quaternion.identity, puzzle);
    }
    bool Approximately(float a, float b, float threshold = 1f) //より厳密な比較
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
                        Vector3 startPos = grid[nx, ny].transform.localPosition;
                        Vector3 targetPos = new Vector3(nx - 1, -(ny - 1), 0);

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

        while (Vector3.Distance(piece.transform.localPosition, targetPos) > 0.05f)//少しずつ近づける
        {
            float dis = (Time.time - startTime) * moveSpeed;//移動した距離
            float percentage = dis / distance;//移動の進捗割合

            piece.transform.localPosition = Vector3.Lerp(startPos, targetPos, percentage);

            yield return null;
        }
        piece.transform.localPosition = targetPos;//目標位置に設定
    }

    //クリア判定
    bool CheckClear()
    {
        //正解パターン
        int[] correntOrder = new int[] { 0, 1, 2, 3, 4, 5, 6 }; 
        
        int orderIndex = 0;
        for (int y = 0; y < 3; y++)
        {
            for (int x = 0; x < 3; x++)
            {
                Vector2Int pos = new Vector2Int(x, y);
                if (pos == blankPos || pos == fixedPiecePos) continue;

                GameObject piece = grid[x, y];
                if (piece == null) return false;

                PuzzlePiece p = piece.GetComponent<PuzzlePiece>();
                if (p.pieceID != correntOrder[orderIndex])
                {
                    return false;
                }
                orderIndex++;
            }
        }

        return true;
    }

    //正解しているピースの数を返す
    public int GetCorrectPieceCount()
    {
        int[] correctOrderFull = new int[9] { -1, 0, 1, 2, -1, 3, 4, 5, 6 };
        // -1 は空白や固定マスの位置（そこは無視）

        int correctCount = 0;

        for (int y = 0; y < 3; y++)
        {
            for (int x = 0; x < 3; x++)
            {
                int index = y * 3 + x;
                Vector2Int pos = new Vector2Int(x, y);

                if (pos == blankPos || pos == fixedPiecePos)
                    continue; // 空白と固定は無視

                GameObject piece = grid[x, y];
                if (piece == null) continue;

                PuzzlePiece p = piece.GetComponent<PuzzlePiece>();

                if (p.pieceID == correctOrderFull[index])
                {
                    correctCount++;
                }
            }
        }
        return correctCount;
    }

    void UpdateProgress()
    {
        int correct = GetCorrectPieceCount();
        int total = 7;// 全部で７個の可動ピース

        progressSlider.value = (float)correct / total;// 0.0から1,0の割合でセットする
        Debug.Log("進捗度" + correct);
    }
}
