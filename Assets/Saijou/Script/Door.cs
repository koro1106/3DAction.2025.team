using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    public float distance = 3f;//�h�A�J�����͈�
    public string sceneToLoad;//�ړ���̃V�[����
    private bool isPlayerNear = false;//Player���h�A�߂Â��Ă��邩
    private bool isDoorOpne = false;//�h�A���J������

    private void Update()
    {
        if (isPlayerNear && Input.GetKeyDown(KeyCode.K) && !isDoorOpne)
        {
            OpenDoor();
        }
    }
    void OpenDoor()
    {
        isDoorOpne = true;
        Debug.Log(isDoorOpne);

        // �v���C���[�̈ʒu��ۑ�
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            PlayerPositionStorage.savedPosition = player.transform.position;
        }
        //�V�[���ړ�
        SceneManager.LoadScene(sceneToLoad);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Door"))
        {
            isPlayerNear = true;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Door"))
        {
            isPlayerNear = false;
        }
    }
}
