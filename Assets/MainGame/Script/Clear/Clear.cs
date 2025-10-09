using UnityEngine;
using UnityEngine.SceneManagement;

public class Clear : MonoBehaviour
{
    public GameObject player;  // �v���C���[�̎Q��
    public Vector3 goalPosition;  // �S�[���I�u�W�F�N�g�̈ʒu
    public float triggerRange = 2f;  // �ʉߔ���Ɏg�p���鋗���͈�
    private bool hasPlayerPassed = false;  // �v���C���[���ʉ߂������ǂ���
    [SerializeField] private SEManager seManager;

    void Update()
    {
        if (!hasPlayerPassed)
        {
            // �v���C���[�̈ʒu�ƃS�[���I�u�W�F�N�g�̈ʒu���r
            if (player.transform.position.x > goalPosition.x + triggerRange)
            {
                hasPlayerPassed = true;
                seManager.GoalSE();
                Debug.Log("�v���C���[���S�[����ʉ߂��܂����I�V�[���J�ڂ��܂��B");
                Invoke(nameof(LoadNextScene), 0.2f); // �� 0.2�b��ɃV�[���J��
            }
        }
    }
    private void LoadNextScene()
    {
        SceneManager.LoadScene("ClearScene");  // �V�[���J��
    }
}
