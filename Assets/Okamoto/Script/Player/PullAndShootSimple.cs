//using UnityEngine;
//using UnityEngine.InputSystem;
//public class PullAndShootSimple : MonoBehaviour
//{
//    public float maxPullDistance = 0.06f; // 6cm = 0.06m
//    public LineRenderer arrowLine;
//    public GameObject landingMarker;
//    private Camera mainCamera;
//    private Vector3 dragStart;
//    private bool isDragging = false;
//    void Start()
//    {
//        mainCamera = Camera.main;
//        arrowLine.enabled = false;
//        if (landingMarker != null) landingMarker.SetActive(false);
//    }
//    void Update()
//    {
//        if (Mouse.current.leftButton.wasPressedThisFrame)
//        {
//            dragStart = GetMouseWorldPosition();
//            isDragging = true;
//            arrowLine.enabled = true;
//        }
//        if (Mouse.current.leftButton.isPressed && isDragging)
//        {
//            Vector3 dragCurrent = GetMouseWorldPosition();
//            Vector3 pullVec = dragStart - dragCurrent;
//            pullVec.z = 0;
//            // 制限
//            if (pullVec.magnitude > maxPullDistance)
//                pullVec = pullVec.normalized * maxPullDistance;
//            // 矢印表示
//            ShowArrow(pullVec);
//            // 対称点に予測点を表示
//            if (landingMarker != null)
//            {
//                landingMarker.SetActive(true);
//                landingMarker.transform.position = transform.position + pullVec;
//            }
//        }
//        if (Mouse.current.leftButton.wasReleasedThisFrame && isDragging)
//        {
//            Vector3 dragEnd = GetMouseWorldPosition();
//            Vector3 pullVec = dragStart - dragEnd;
//            pullVec.z = 0;
//            if (pullVec.magnitude > maxPullDistance)
//                pullVec = pullVec.normalized * maxPullDistance;
//            // プレイヤーを移動（飛距離＝引っ張り長さ）
//            transform.position += pullVec;
//            // リセット
//            arrowLine.enabled = false;
//            if (landingMarker != null) landingMarker.SetActive(false);
//            isDragging = false;
//        }
//        // Z座標固定
//        var pos = transform.position;
//        pos.z = 0;
//        transform.position = pos;
//    }
//    Vector3 GetMouseWorldPosition()
//    {
//        Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
//        Plane plane = new Plane(Vector3.forward, Vector3.zero); // XY平面
//        if (plane.Raycast(ray, out float distance))
//            return ray.GetPoint(distance);
//        return Vector3.zero;
//    }
//    void ShowArrow(Vector3 dir)
//    {
//        if (arrowLine == null) return;
//        arrowLine.positionCount = 2;
//        arrowLine.SetPosition(0, transform.position);
//        arrowLine.SetPosition(1, transform.position - dir);
//        float powerRate = dir.magnitude / maxPullDistance;
//        if (powerRate < 0.33f)
//            arrowLine.material.color = Color.green;
//        else if (powerRate < 0.66f)
//            arrowLine.material.color = Color.yellow;
//        else
//            arrowLine.material.color = Color.red;
//    }
//}
using UnityEngine;
using UnityEngine.InputSystem;
[RequireComponent(typeof(Rigidbody))]
public class PullShootParabolicFall : MonoBehaviour
{
    public float maxPullDistance = 0.06f;
    public float jumpPower = 5f; // 上方向の倍率
    public LineRenderer arrowLine;
    public GameObject landingMarker;
    private Rigidbody rb;
    private Camera mainCamera;
    private Vector3 dragStart;
    private bool isDragging;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.isKinematic = true;
        mainCamera = Camera.main;
        arrowLine.enabled = false;
        if (landingMarker != null) landingMarker.SetActive(false);
    }
    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            dragStart = GetMouseWorldPosition();
            isDragging = true;
            arrowLine.enabled = true;
        }
        if (Mouse.current.leftButton.isPressed && isDragging)
        {
            Vector3 curr = GetMouseWorldPosition();
            Vector3 pull = dragStart - curr;
            pull.z = 0;
            if (pull.magnitude > maxPullDistance)
                pull = pull.normalized * maxPullDistance;
            ShowArrow(pull);
            if (landingMarker) landingMarker.transform.position = transform.position + pull;
        }
        if (Mouse.current.leftButton.wasReleasedThisFrame && isDragging)
        {
            Vector3 release = GetMouseWorldPosition();
            Vector3 pull = dragStart - release;
            pull.z = 0;
            if (pull.magnitude > maxPullDistance)
                pull = pull.normalized * maxPullDistance;
            Launch(pull);
            isDragging = false;
            arrowLine.enabled = false;
            if (landingMarker) landingMarker.SetActive(false);
        }
    }
    void Launch(Vector3 pull)
    {
        rb.isKinematic = false;
        rb.useGravity = true;
        // 引っ張った方向に水平方向の速度、上向きにジャンプ力を追加
        Vector3 velocity = pull.normalized * (pull.magnitude / maxPullDistance) * jumpPower;
        velocity.y = Mathf.Abs(velocity.magnitude); // 上向き成分（最大高さを作る）
        rb.velocity = velocity;
    }
    Vector3 GetMouseWorldPosition()
    {
        Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (new Plane(Vector3.forward, 0).Raycast(ray, out float d))
            return ray.GetPoint(d);
        return Vector3.zero;
    }
    void ShowArrow(Vector3 dir)
    {
        arrowLine.positionCount = 2;
        arrowLine.SetPosition(0, transform.position);
        arrowLine.SetPosition(1, transform.position + dir);
        float rate = dir.magnitude / maxPullDistance;
        arrowLine.material.color = rate < 0.33f ? Color.green :
                                   rate < 0.66f ? Color.yellow : Color.red;
    }
    void OnCollisionEnter(Collision col)
    {
        Debug.Log("Hit: " + col.gameObject.name);
    }
}




