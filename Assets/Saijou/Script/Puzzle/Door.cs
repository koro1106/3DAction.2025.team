using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    public float distance = 3f;//�h�A�J�����͈�
    public string sceneToLoad;//�ړ���̃V�[����
    private bool isPlayerNear = false;//Player���h�A�߂Â��Ă��邩
    private bool isDoorOpne = false;//�h�A���J������
    [SerializeField] private SEManager seManager; // SE

    private void Update()
    {
        if (isPlayerNear && Input.GetKeyDown(KeyCode.F) && !isDoorOpne)
        {
            OpenDoor();
        }
    }
    void OpenDoor()
    {
        isDoorOpne = true;
        Debug.Log(isDoorOpne);
        // �V�[������ۑ�
        StageLoader.LastPlayedStageName = SceneManager.GetActiveScene().name;
        Debug.Log("�Ō�ɂ����V�[�����F" + StageLoader.LastPlayedStageName);
        // �v���C���[�̈ʒu��ۑ�
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            PlayerPositionStorage.savedPosition = player.transform.position;
        }
        seManager.DoorSE();
        Invoke(nameof(LoadNextScene), 0.2f); // �� 0.2�b��ɃV�[���J��
    }
    private void LoadNextScene()
    {
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
