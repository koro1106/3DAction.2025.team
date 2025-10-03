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
    [SerializeField] private LayerMask groundLayer; // �n�ʃ��C���[
    [SerializeField] private float fallDeathHeight = 4f; // �������̍����������l
    private Rigidbody rb;
    private Camera mainCamera;
    private Vector3 dragStart;
    private bool isDragging;
    private int remainingPulls = 2; // �� �ő�2��ɏC��
    private bool positionRestored = false;
    // ��������p
    private float highestY; // �ړ��J�n���Ƃ̍ō�Y���W���L�^
    private bool isFallingCheck; // �����`�F�b�N���t���O
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
        // �V�[���J�ڂňʒu����
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
        // �󒆂̂Ƃ��͍ō�Y���L�^
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
        // �V�����ړ��J�n �� �ō��_�����Z�b�g
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
            // ���n���ɗ���������
            if (isFallingCheck)
            {
                float fallDistance = highestY - transform.position.y;
                if (fallDistance >= fallDeathHeight && !health.isDead)
                {
                    Debug.Log("�������I�i����: " + fallDistance + "�j");
                    health.Die();
                }
                else
                {
                    Debug.Log("���S�ɒ��n�i����: " + fallDistance + "�j");
                }
                isFallingCheck = false;
            }
            // �n�ʂɒ�������܂��ړ��ł���悤��
            remainingPulls = 2;
        }
    }
}