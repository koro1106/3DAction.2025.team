using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform target;          // プレイヤー
    public Vector3 offset = new Vector3(0, 10f, -10f); // カメラの位置オフセット
    public float smoothSpeed = 5f;    // カメラの追従速度

    void LateUpdate()
    {
        if (target == null) return;

        //回転固定
        transform.rotation = Quaternion.identity;
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        transform.position = smoothedPosition;

        // 常にプレイヤーを見る
       // transform.LookAt(target);
    }
}