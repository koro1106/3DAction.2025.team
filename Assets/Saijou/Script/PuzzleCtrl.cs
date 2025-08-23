using System.Collections;
using UnityEngine;

public class PuzzleCtrl : MonoBehaviour
{
    public Transform puzzle;//�p�Y���{��
    public GameObject piecePrefub;//�s�[�X�̃v���n�u
    GameObject[,] grid = new GameObject[3, 3];
    private Vector2Int blankPos = new Vector2Int(0,0);//�󔒃}�X�̈ʒu
  �@private Vector2Int fixedPiecePos = new Vector2Int(2,0);//�Œ�}�X
    private bool isRotating = false;


    void Start()
    {
        //�s�[�X�������Ĕz�u�i0,0���󔒂Ƃ���j
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
        // ����̔��̉�]�p�x���擾
        float angleY = puzzle.eulerAngles.y;
        angleY = Mathf.Round(angleY) % 360;

        // ��]�p�x�ɉ����āu�������̔z��ړ��x�N�g���v������
        Vector2Int downDir;
        if (Approximately(angleY, 0)) downDir = new Vector2Int(0, -1);
        else if (Approximately(angleY, 90)) downDir = new Vector2Int(1, 0);
        else if (Approximately(angleY, 180)) downDir = new Vector2Int(0, 1);
        else if (Approximately(angleY, 270)) downDir = new Vector2Int(-1, 0);
        else downDir = new Vector2Int(0, -1); // �f�t�H���g��

        MovePiecesDown(downDir);
    }

    bool Approximately(float a, float b, float threshold = 5f)
    {
        return Mathf.Abs(Mathf.DeltaAngle(a, b)) < threshold;
    }

    // �������ɋ󔒂�����΃s�[�X�𓮂������W�b�N��
    void MovePiecesDown(Vector2Int downDir)
    {
        bool moved;

        //���x����������悤�Ƀ��[�v
        do
        {
            moved = false;

            //�d�͕����ɍ��킹�ă��[�v�������߂�
            for(int x = (downDir.x > 0 ? 1: 1); x >= 0 && x < 3; x += (downDir.x >= 0 ? -1 : 1))
            {
                for (int y = (downDir.y > 0 ? 1 : 1); y >= 0 && y < 3; y += (downDir.y >= 0 ? -1 : 1))
                {
                    int nx = x + downDir.x;
                    int ny = y + downDir.y;

                    if (!IsInBounds(nx, ny)) continue;

                    if (grid[x, y] != null &&
                  �@�@�@ grid[nx, ny] == null &&
                   �@�@�@�@new Vector2Int(nx, ny) != fixedPiecePos)
                    {
                        //�ړ�
                        grid[nx,ny] = grid[x,y];
                        grid[x, y] = null;
                        grid[nx, ny].transform.localPosition = new Vector3(nx, 0,ny);
                   
                        //�󔒈ʒu�X�V
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
            //Slerp���J�n����I���Ɍ������Ă��̊��������i�񂾉�]��Ԃ�
            puzzle.rotation = Quaternion.Slerp(startRot, endRot, time / duration);
            time += Time.deltaTime;//���t���[���o�ߎ��Ԃ����Z
            yield return null;
        }

        puzzle.rotation = endRot;
        isRotating = false;

        //��]�I��������d�͕����ɏ]���ăs�[�X������
        OnPuzzleRotated();
    }
}
