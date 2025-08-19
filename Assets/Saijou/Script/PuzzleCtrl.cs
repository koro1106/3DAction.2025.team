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
                if (x == 0 && y == 0) continue;//�󔒃}�X
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
    // ��]�������ƂɌĂԗ�i��]������j
    void OnPuzzleRotated()
    {
        // ����̔��̉�]�p�x���擾
        float angleY = puzzle.eulerAngles.y;

        // ��]�p�x�ɉ����āu�������̔z��ړ��x�N�g���v������
        Vector2Int downDir;
        if (Approximately(angleY, 0)) downDir = new Vector2Int(0, -1);
        else if (Approximately(angleY, 90)) downDir = new Vector2Int(1, 0);
        else if (Approximately(angleY, 180)) downDir = new Vector2Int(0, 1);
        else if (Approximately(angleY, 270)) downDir = new Vector2Int(-1, 0);
        else downDir = new Vector2Int(0, -1); // �f�t�H���g��

        TryMovePiecesDown(downDir);
    }

    bool Approximately(float a, float b, float threshold = 5f)
    {
        return Mathf.Abs(a - b) < threshold;
    }

    // �������ɋ󔒂�����΃s�[�X�𓮂������W�b�N��
    void TryMovePiecesDown(Vector2Int downDir)
    {
        // �Ⴆ�΁A�󔒂̈ʒu����X,��Y�Ƃ���
        int blankX = 0;
        int blankY = 0;

        // �������ɗאڂ���s�[�X������΁A�󔒂Ɉړ�
        int targetX = blankX + downDir.x;
        int targetY = blankY + downDir.y;

        if (IsInBounds(targetX, targetY) && grid[targetX, targetY] != null)
        {
            // �ړ�����
            GameObject piece = grid[targetX, targetY];
            grid[blankX, blankY] = piece;
            grid[targetX, targetY] = null;

            // ���W�ړ��i���[���h���W�j
            piece.transform.position = new Vector3(blankX, 0, blankY);

            // �󔒈ʒu�X�V
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
            //Slerp���J�n����I���Ɍ������Ă��̊��������i�񂾉�]��Ԃ�
            puzzle.rotation = Quaternion.Slerp(startRot, endRot, time / duration);
            time += Time.deltaTime;//���t���[���o�ߎ��Ԃ����Z
            yield return null;
        }

        puzzle.rotation = endRot;
        isRotating = false;

        OnPuzzleRotated();
    }
}
