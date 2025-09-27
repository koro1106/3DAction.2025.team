using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.Timeline;
using static UnityEditor.PlayerSettings;

public class PuzzleCtrl : MonoBehaviour
{
    public Transform puzzle; // �p�Y���{��
    public GameObject piecePrefub; // �s�[�X�̃v���n�u
    public GameObject fixedPrefub;  // �Œ�s�[�X
    public UnityEngine.UI.Slider progressSlider;//�i���x�Q�[�W

    GameObject[,] grid = new GameObject[3, 3];
    private Vector2Int blankPos = new Vector2Int(0, 0); // �󔒃}�X�̈ʒu
    private Vector2Int fixedPiecePos = new Vector2Int(2, 0); // �Œ�}�X
    private bool isRotating = false;

    private int[] currentPieceOrder; //���݂̏����z�u
    public int difficulty = 0; //��Փx 0?3

   [SerializeField] private PlayableDirector playableDirector;
   [SerializeField] private GameObject UI;
   [SerializeField] private GameObject TimeLine;

    void Start()
    {
        InitializePuzzle();//����������
        UpdateProgress();//�i���x�Q�[�W�X�V
    }

    //�p�Y���̃p�^�[��
    private int[] GetPatternByDifficulty(int level)
    {
        switch (level)
        {
            case 0:
                return new int[] { 2, 0, 5, 3, 1, 4, 6 }; //�ȒP
            case 1:
                return new int[] { 6, 5, 4, 3, 2, 1, 0 }; //���
            case 2:
                return new int[] { 1, 4, 0, 2, 5, 6, 3 }; //�����_��1
            case 3:
                return new int[] { 3, 1, 6, 0, 5, 4, 2 }; //�����_��2
            default:
                return new int[] { 0, 1, 2, 3, 4, 5, 6 }; //�f�t�H���g
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            RotatePuzzle(90f); // ����]
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            RotatePuzzle(-90f); // �E��]
        }
    }

    void OnPuzzleRotated()
    {
        // ����̔��̉�]�p�x���擾
        float angleZ = puzzle.eulerAngles.z; // 2D�Ȃ̂�Z�����̉�]�������l������
        angleZ = Mathf.Round(angleZ) % 360;

        // ��]�p�x�ɉ����āu�������̔z��ړ��x�N�g���v������
        Vector2Int downDir;
        if (Approximately(angleZ, 0)) downDir = new Vector2Int(0, 1); // ��
        else if (Approximately(angleZ, 90)) downDir = new Vector2Int(-1, 0); // �E
        else if (Approximately(angleZ, 180)) downDir = new Vector2Int(0, -1); // ��
        else if (Approximately(angleZ, 270)) downDir = new Vector2Int(1, 0); // ��
        else downDir = new Vector2Int(0, -1); // �f�t�H���g��

        MovePiecesDown(downDir); // �s�[�X���ړ�������
        StartCoroutine(DelayedCheckClear());
    }

    IEnumerator DelayedCheckClear()
    {
        yield return new WaitForSeconds(0.6f); // �ړ����I���̂������҂�

        //�i���x�Q�[�W�X�V
        UpdateProgress();
        if (CheckClear())
        {
            Debug.Log("�N���A�I");
            UI.SetActive(false);
            TimeLine.SetActive(true);
            if (playableDirector != null)
            {
                playableDirector.Play();  // Timeline�Đ��J�n
            }
            if (Input.GetKeyDown(KeyCode.KeypadEnter))
            {
                SceneManager.LoadScene("MainStage1");
            }
        }
    }
    public void InitializePuzzle()//����������
    {
        //�{�g���ȊO�̎q�I�u�W�F�N�g�S�폜�i���Z�b�g�̂��߁j
        foreach (Transform child in puzzle)
        {
            if (child.gameObject.CompareTag("Bottle"))
            {
                continue;//�{�g�������Ȃ�
            }
            Destroy(child.gameObject);
        }

        //�O���b�h�Ƌ󔒈ʒu�̏�����
        grid = new GameObject[3, 3];
        blankPos = new Vector2Int(0, 0);
        puzzle.localRotation = Quaternion.identity; //�p�Y����]�����Z�b�g

        // ��.�Œ���������|�W�V�������X�g�쐬
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

        // �s�[�XID�̕��т��Փx�Ō���
        currentPieceOrder = GetPatternByDifficulty(difficulty);

        // �s�[�X����
        for (int i = 0; i < availablePositions.Count; i++)
        {
            Vector2Int pos = availablePositions[i];
            Vector3 localPos = new Vector3(pos.x - 1, -(pos.y - 1), 0);
            GameObject piece = Instantiate(piecePrefub, localPos, Quaternion.identity, puzzle);

            PuzzlePiece pp = piece.GetComponent<PuzzlePiece>();
            pp.pieceID = currentPieceOrder[i]; // ID�����蓖��

            grid[pos.x, pos.y] = piece;
        }

        // �Œ�s�[�X����
        Vector3 fixedLocalPos = new Vector3(fixedPiecePos.x - 1, -(fixedPiecePos.y - 1), 0);
        Instantiate(fixedPrefub, fixedLocalPos, Quaternion.identity, puzzle);
    }
    bool Approximately(float a, float b, float threshold = 1f) //��茵���Ȕ�r
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

            // �d�͕����ɍ��킹�ă��[�v�������߂�
            for (int x = 0; x < 3; x++) // x���͂��̂܂܃��[�v
            {
                for (int y = 0; y < 3; y++) // y�������̂܂܃��[�v
                {
                    int nx = x + downDir.x;
                    int ny = y + downDir.y;

                    if (!IsInBounds(nx, ny)) continue;

                    // �s�[�X�����݂��A�ړ��悪�󔒂ŁA�ړ��悪�Œ�s�[�X�łȂ��ꍇ
                    if (grid[x, y] != null &&
                        grid[nx, ny] == null &&
                        new Vector2Int(nx, ny) != fixedPiecePos)
                    {
                        // �s�[�X�̈ʒu���X�V
                        grid[nx, ny] = grid[x, y];
                        grid[x, y] = null;

                        // �V�����ʒu�Ƀs�[�X���ړ�
                        Vector3 startPos = grid[nx, ny].transform.localPosition;
                        Vector3 targetPos = new Vector3(nx - 1, -(ny - 1), 0);

                        StartCoroutine(MovePieceSmoothly(grid[nx, ny], startPos, targetPos, moveSpeed));

                        // �󔒈ʒu���X�V
                        blankPos = new Vector2Int(x, y);
                        moved = true;
                    }
                }
            }
        } while (moved); // �s�[�X���ړ�������胋�[�v
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
        Quaternion endRot = startRot * Quaternion.Euler(0f, 0f, angle); // Z�����̂�

        while (time < duration)
        {
            // Slerp���J�n����I���Ɍ�������t�̊��������i�񂾉�]��Ԃ�
            puzzle.rotation = Quaternion.Slerp(startRot, endRot, time / duration);
            time += Time.deltaTime; // ���t���[���o�ߎ��Ԃ����Z
            yield return null;
        }

        puzzle.rotation = endRot;
        isRotating = false;

        // ��]�I��������d�͕����ɏ]���ăs�[�X������
        OnPuzzleRotated();
    }

    IEnumerator MovePieceSmoothly(GameObject piece, Vector3 startPos, Vector3 targetPos, float moveSpeed)
    {
        float distance = Vector3.Distance(startPos, targetPos);//�ړ�����
        float startTime = Time.time;//�ړ��J�n����

        while (Vector3.Distance(piece.transform.localPosition, targetPos) > 0.05f)//�������߂Â���
        {
            float dis = (Time.time - startTime) * moveSpeed;//�ړ���������
            float percentage = dis / distance;//�ړ��̐i������

            piece.transform.localPosition = Vector3.Lerp(startPos, targetPos, percentage);

            yield return null;
        }
        piece.transform.localPosition = targetPos;//�ڕW�ʒu�ɐݒ�
    }

    //�N���A����
    bool CheckClear()
    {
        //�����p�^�[��
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

    //�������Ă���s�[�X�̐���Ԃ�
    public int GetCorrectPieceCount()
    {
        int[] correctOrderFull = new int[9] { -1, 0, 1, 2, -1, 3, 4, 5, 6 };
        // -1 �͋󔒂�Œ�}�X�̈ʒu�i�����͖����j

        int correctCount = 0;

        for (int y = 0; y < 3; y++)
        {
            for (int x = 0; x < 3; x++)
            {
                int index = y * 3 + x;
                Vector2Int pos = new Vector2Int(x, y);

                if (pos == blankPos || pos == fixedPiecePos)
                    continue; // �󔒂ƌŒ�͖���

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
        int total = 7;// �S���łV�̉��s�[�X

        progressSlider.value = (float)correct / total;// 0.0����1,0�̊����ŃZ�b�g����
        Debug.Log("�i���x" + correct);
    }
}
