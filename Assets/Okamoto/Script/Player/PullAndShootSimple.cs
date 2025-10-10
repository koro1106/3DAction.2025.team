using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PullShootParabolicFall : MonoBehaviour
{
    public float maxPullDistance = 0.06f;
    public float jumpPower = 5f;
    public float minPullThreshold = 0.005f;

    [Header("UI Elements")]
    public GameObject arrowImage; // ← SpriteRenderer付き矢印画像
    public GameObject landingMarker;

    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float fallDeathHeight = 4f;

    private Rigidbody rb;
    private Camera mainCamera;
    private Vector3 dragStart;
    private bool isDragging;
    private int remainingPulls = 2;
    private bool positionRestored = false;

    private float highestY;
    private bool isFallingCheck;
    private PlayerHealth health;
    [SerializeField] private SEManager seManager;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.isKinematic = true;
        mainCamera = Camera.main;

        if (arrowImage != null)
            arrowImage.SetActive(false);

        if (landingMarker != null)
            landingMarker.SetActive(false);

        health = GetComponent<PlayerHealth>();
    }

    void Update()
    {
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
            if (arrowImage != null) arrowImage.SetActive(true);
        }

        if (Mouse.current.leftButton.isPressed && isDragging)
        {
            Vector3 curr = GetMouseWorldPosition();
            Vector3 pull = dragStart - curr;
            pull.z = 0;

            if (pull.magnitude > maxPullDistance)
                pull = pull.normalized * maxPullDistance;

            ShowArrowImage(pull);

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
            if (arrowImage != null) arrowImage.SetActive(false);
            if (landingMarker) landingMarker.SetActive(false);
        }

        if (!rb.isKinematic && rb.useGravity)
        {
            if (transform.position.y > highestY)
            {
                highestY = transform.position.y;
            }
        }
    }

    void ShowArrowImage(Vector3 dir)
    {
        if (arrowImage == null) return;

        //Cubeの右上角をワールド座標で計算
        Vector3 cubeSize = transform.localScale;
        Vector3 cornerOffset = new Vector3(cubeSize.x / 3f, cubeSize.y / 3f, 0f);
        Vector3 cornerPosition = transform.position + cornerOffset;

        //矢印の根本（ピボット位置）を角に配置
        arrowImage.transform.position = cornerPosition;

        //引っ張り方向の逆向き（発射方向）に回転
        float angle = Mathf.Atan2(-dir.y, -dir.x) * Mathf.Rad2Deg + 90f;
        arrowImage.transform.rotation = Quaternion.Euler(0, 0, angle);

        // 長さスケール調整（Y方向に伸ばす）
        float scaleRate = dir.magnitude / maxPullDistance;
        arrowImage.transform.localScale = new Vector3(1f, scaleRate, 1f);
    }


    void Launch(Vector3 pull)
    {
        rb.isKinematic = false;
        rb.useGravity = true;
        Vector3 velocity = pull.normalized * (pull.magnitude / maxPullDistance) * jumpPower;
        velocity.y = Mathf.Max(velocity.y, jumpPower * 0.5f);
        rb.velocity = velocity;
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

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Ground"))
        {
            seManager.PlayerGraundSE();

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

            remainingPulls = 2;
        }
    }
}
