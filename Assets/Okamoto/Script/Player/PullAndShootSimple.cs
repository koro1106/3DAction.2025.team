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

    private Rigidbody rb;
    private Camera mainCamera;
    private Vector3 dragStart;
    private bool isDragging;
    private int remainingPulls = 1;
    private bool positionRestored = false; //位置の復元
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
        //シーン遷移で位置復元
        if (!positionRestored && PlayerPositionStorage.savedPosition != Vector3.zero)
        {
            transform.position = PlayerPositionStorage.savedPosition;
            positionRestored = true;
        }

        if (remainingPulls <= 0) return; // 引っ張り残り0なら無視
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
                remainingPulls--; // 空中でも引っ張ったら減らす
            }
            isDragging = false;
            arrowLine.enabled = false;
            if (landingMarker) landingMarker.SetActive(false);
        }
    }
    void Launch(Vector3 pull)
    {
        rb.isKinematic = false;
        rb.useGravity = true;
        Vector3 velocity = pull.normalized * (pull.magnitude / maxPullDistance) * jumpPower;
        velocity.y = Mathf.Max(velocity.y, jumpPower * 0.5f);
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
    void OnCollisionStay(Collision col)
    {
        foreach (ContactPoint contact in col.contacts)
        {
            // 地面と接触したら引っ張り回数リセット
            if (Vector3.Dot(contact.normal, Vector3.up) > 0.5f)
            {
                remainingPulls = 1;
                break;
            }
        }
    }
}




