using UnityEngine;
public class FollowTarget : MonoBehaviour
{
    [Header("追従対象")]
    public Transform target; // プレイヤーなど
    [Header("追従設定")]
    [Tooltip("ターゲットとのオフセット（位置ずれ）")]
    public Vector3 offset = new Vector3(0, 2f, -5f);
    [Tooltip("追従スピード")]
    public float followSpeed = 5f;
    [Tooltip("ターゲットとの距離がこの範囲内なら動かない")]
    public float followRange = 0.1f;
    [Header("追従範囲制限（オプション）")]
    public bool useClamp = false;
    public Vector2 minPosition; // x, y の最小
    public Vector2 maxPosition; // x, y の最大
    void LateUpdate()
    {
        if (target == null) return;
        // 目標位置を計算
        Vector3 targetPos = target.position + offset;
        // 現在位置と目標位置の距離を測る
        float distance = Vector3.Distance(transform.position, targetPos);
        // 一定距離以上離れていたら追従
        if (distance > followRange)
        {
            transform.position = Vector3.Lerp(
                transform.position,
                targetPos,
                followSpeed * Time.deltaTime
            );
        }
        // 範囲制限が有効ならClamp
        if (useClamp)
        {
            transform.position = new Vector3(
                Mathf.Clamp(transform.position.x, minPosition.x, maxPosition.x),
                Mathf.Clamp(transform.position.y, minPosition.y, maxPosition.y),
                transform.position.z
            );
        }
    }
}