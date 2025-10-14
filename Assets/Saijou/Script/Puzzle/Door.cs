using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    public float distance = 3f;//ドア開けれる範囲
    public string sceneToLoad;//移動先のシーン名
    private bool isPlayerNear = false;//Playerがドア近づいているか
    private bool isDoorOpne = false;//ドアが開いたか
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
        // シーン名を保存
        StageLoader.LastPlayedStageName = SceneManager.GetActiveScene().name;
        Debug.Log("最後にいたシーン名：" + StageLoader.LastPlayedStageName);
        // プレイヤーの位置を保存
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            PlayerPositionStorage.savedPosition = player.transform.position;
        }
        seManager.DoorSE();
        Invoke(nameof(LoadNextScene), 0.2f); // ← 0.2秒後にシーン遷移
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
