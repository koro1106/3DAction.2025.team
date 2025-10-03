using UnityEngine;
using UnityEngine.InputSystem;
[RequireComponent(typeof(Rigidbody))]
public class PullShootParabolicFall : MonoBehaviour
{
    public float maxPullDistance = 0.06f;
    public float jumpPower = 5f;
    public float minPullThreshold = 0.005f;
    public LineRenderer arrowLine;
    public GameObject landingMarker;
    [SerializeField] private LayerMask groundLayer; // 地面レイヤー
    [SerializeField] private float fallDeathHeight = 4f; // 落下死の高さしきい値
    private Rigidbody rb;
    private Camera mainCamera;
    private Vector3 dragStart;
    private bool isDragging;
    private int remainingPulls = 2; // ← 最大2回に修正
    private bool positionRestored = false;
    // 落下判定用
    private float highestY; // 移動開始ごとの最高Y座標を記録
    private bool isFallingCheck; // 落下チェック中フラグ
    private PlayerHealth health;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.isKinematic = true;
        mainCamera = Camera.main;
        arrowLine.enabled = false;
        if (landingMarker != null) landingMarker.SetActive(false);
        health = GetComponent<PlayerHealth>();
    }
    void Update()
    {
        // シーン遷移で位置復元
        if (!positionRestored && PlayerPositionStorage.savedPosition != Vector3.zero)
        {
            transform.position = PlayerPositionStorage.savedPosition;
            positionRestored = true;
        }
        if (remainingPulls <= 0) return;
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
            if (pull.magnitude >= minPullThreshold)
            {
                if (pull.magnitude > maxPullDistance)
                    pull = pull.normalized * maxPullDistance;
                Launch(pull);
                remainingPulls--;
            }
            isDragging = false;
            arrowLine.enabled = false;
            if (landingMarker) landingMarker.SetActive(false);
        }
        // 空中のときは最高Yを記録
        if (!rb.isKinematic && rb.useGravity)
        {
            if (transform.position.y > highestY)
            {
                highestY = transform.position.y;
            }
        }
    }
    void Launch(Vector3 pull)
    {
        rb.isKinematic = false;
        rb.useGravity = true;
        Vector3 velocity = pull.normalized * (pull.magnitude / maxPullDistance) * jumpPower;
        velocity.y = Mathf.Max(velocity.y, jumpPower * 0.5f);
        rb.velocity = velocity;
        // 新しい移動開始 → 最高点をリセット
        highestY = transform.position.y;
        isFallingCheck = true;
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
    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Ground"))
        {
            // 着地時に落下死判定
            if (isFallingCheck)
            {
                float fallDistance = highestY - transform.position.y;
                if (fallDistance >= fallDeathHeight && !health.isDead)
                {
                    Debug.Log("落下死！（距離: " + fallDistance + "）");
                    health.Die();
                }
                else
                {
                    Debug.Log("安全に着地（距離: " + fallDistance + "）");
                }
                isFallingCheck = false;
            }
            // 地面に着いたらまた移動できるように
            remainingPulls = 2;
        }
    }
}