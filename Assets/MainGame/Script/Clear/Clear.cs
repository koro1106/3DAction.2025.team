using UnityEngine;
using UnityEngine.SceneManagement;

public class Clear : MonoBehaviour
{
    public GameObject player;  // プレイヤーの参照
    public Vector3 goalPosition;  // ゴールオブジェクトの位置
    public float triggerRange = 2f;  // 通過判定に使用する距離範囲
    private bool hasPlayerPassed = false;  // プレイヤーが通過したかどうか
    [SerializeField] private SEManager seManager;

    void Update()
    {
        if (!hasPlayerPassed)
        {
            // プレイヤーの位置とゴールオブジェクトの位置を比較
            if (player.transform.position.x > goalPosition.x + triggerRange)
            {
                hasPlayerPassed = true;
                seManager.GoalSE();
                Debug.Log("プレイヤーがゴールを通過しました！シーン遷移します。");
                Invoke(nameof(LoadNextScene), 0.2f); // ← 0.2秒後にシーン遷移
            }
        }
    }
    private void LoadNextScene()
    {
        SceneManager.LoadScene("ClearScene");  // シーン遷移
    }
}
