using System.Collections;
using UnityEngine;

public class PuzzleCtrl : MonoBehaviour
{
    public Transform puzzle; // �p�Y���{��
    public GameObject piecePrefub; // �s�[�X�̃v���n�u
    public GameObject fixedPrefub;  // �Œ�s�[�X
    GameObject[,] grid = new GameObject[3, 3];
    private Vector2Int blankPos = new Vector2Int(0, 0); // �󔒃}�X�̈ʒu
    private Vector2Int fixedPiecePos = new Vector2Int(2, 0); // �Œ�}�X
    private bool isRotating = false;

    void Start()
    {
        // �s�[�X�������Ĕz�u�i0,0���󔒂Ƃ���j
        for (int x = 0; x < 3; x++)
        {
            for (int y = 0; y < 3; y++)
            {
                Vector2Int pos = new Vector2Int(x, y);

                if (pos == blankPos || pos == fixedPiecePos) continue; // �󔒂�Œ�s�[�X���X�L�b�v

                // �s�[�X�𐮐����W�Ő���
                Vector2Int piecePos = new Vector2Int(x, y);  // �������W���g�p
                GameObject piece = Instantiate(piecePrefub, new Vector3(piecePos.x, piecePos.y, 0), Quaternion.identity, puzzle);
                grid[x, y] = piece;
            }
        }

        // �Œ�s�[�X�����i�������W�Ŕz�u�j
        Vector2Int fixedPos = new Vector2Int(fixedPiecePos.x, fixedPiecePos.y);
        Instantiate(fixedPrefub, new Vector3(fixedPos.x, fixedPos.y, 0), Quaternion.identity, puzzle);
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
        float angleZ = puzzle.eulerAngles.z; // 2D�Ȃ̂�Z�����̉�]�������l��
        angleZ = Mathf.Round(angleZ) % 360;

        // ��]�p�x�ɉ����āu�������̔z��ړ��x�N�g���v������
        Vector2Int downDir;
        if (Approximately(angleZ, 0)) downDir = new Vector2Int(0, -1); // ��
        else if (Approximately(angleZ, 90)) downDir = new Vector2Int(1, 0); // �E
        else if (Approximately(angleZ, 180)) downDir = new Vector2Int(0, 1); // ��
        else if (Approximately(angleZ, 270)) downDir = new Vector2Int(-1, 0); // ��
        else downDir = new Vector2Int(0, -1); // �f�t�H���g��

        MovePiecesDown(downDir); // �s�[�X���ړ�������
    }

    bool Approximately(float a, float b, float threshold = 1f) // ��茵���Ȕ�r
    {
        return Mathf.Abs(Mathf.DeltaAngle(a, b)) < threshold;
    }

    void MovePiecesDown(Vector2Int downDir)
    {
        bool moved;
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
                        // �����ō��W�𐮐��ɋ����I�Ɋۂ߂�
                        Vector3 newPosition = new Vector3(Mathf.Round(nx), Mathf.Round(ny), 0);
                        grid[nx, ny].transform.localPosition = newPosition;

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
}
