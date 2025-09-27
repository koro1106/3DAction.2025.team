using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    public float distance = 3f;//ドア開けれる範囲
    public string sceneToLoad;//移動先のシーン名
    private bool isPlayerNear = false;//Playerがドア近づいているか
    private bool isDoorOpne = false;//ドアが開いたか

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

        // プレイヤーの位置を保存
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            PlayerPositionStorage.savedPosition = player.transform.position;
        }
        //シーン移動
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
