using UnityEngine;
using UnityEngine.InputSystem;
public class PullAndShootSimple : MonoBehaviour
{
    public float maxPullDistance = 0.06f; // 6cm = 0.06m
    public LineRenderer arrowLine;
    public GameObject landingMarker;
    private Camera mainCamera;
    private Vector3 dragStart;
    private bool isDragging = false;
    void Start()
    {
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
            Vector3 dragCurrent = GetMouseWorldPosition();
            Vector3 pullVec = dragStart - dragCurrent;
            pullVec.z = 0;
            // ����
            if (pullVec.magnitude > maxPullDistance)
                pullVec = pullVec.normalized * maxPullDistance;
            // ���\��
            ShowArrow(pullVec);
            // �Ώ̓_�ɗ\���_��\��
            if (landingMarker != null)
            {
                landingMarker.SetActive(true);
                landingMarker.transform.position = transform.position + pullVec;
            }
        }
        if (Mouse.current.leftButton.wasReleasedThisFrame && isDragging)
        {
            Vector3 dragEnd = GetMouseWorldPosition();
            Vector3 pullVec = dragStart - dragEnd;
            pullVec.z = 0;
            if (pullVec.magnitude > maxPullDistance)
                pullVec = pullVec.normalized * maxPullDistance;
            // �v���C���[���ړ��i�򋗗����������蒷���j
            transform.position += pullVec;
            // ���Z�b�g
            arrowLine.enabled = false;
            if (landingMarker != null) landingMarker.SetActive(false);
            isDragging = false;
        }
        // Z���W�Œ�
        var pos = transform.position;
        pos.z = 0;
        transform.position = pos;
    }
    Vector3 GetMouseWorldPosition()
    {
        Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
        Plane plane = new Plane(Vector3.forward, Vector3.zero); // XY����
        if (plane.Raycast(ray, out float distance))
            return ray.GetPoint(distance);
        return Vector3.zero;
    }
    void ShowArrow(Vector3 dir)
    {
        if (arrowLine == null) return;
        arrowLine.positionCount = 2;
        arrowLine.SetPosition(0, transform.position);
        arrowLine.SetPosition(1, transform.position - dir);
        float powerRate = dir.magnitude / maxPullDistance;
        if (powerRate < 0.33f)
            arrowLine.material.color = Color.green;
        else if (powerRate < 0.66f)
            arrowLine.material.color = Color.yellow;
        else
            arrowLine.material.color = Color.red;
    }
}
