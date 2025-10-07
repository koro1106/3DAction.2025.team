using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform target;          // �v���C���[
    public Vector3 offset = new Vector3(0, 10f, 0f); // �J�����̈ʒu�I�t�Z�b�g
    public float smoothSpeed = 5f;    // �J�����̒Ǐ]���x

    void LateUpdate()
    {
        if (target == null) return;

        //��]�Œ�
        transform.rotation = Quaternion.identity;

        //Vector3 desiredPosition = target.position + offset;
        //Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        //transform.position = smoothedPosition;
        Vector3 desiredPosition = new Vector3(target.position.x + offset.x, transform.position.y, transform.position.z);
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        transform.position = smoothedPosition;

        // ��Ƀv���C���[������
        // transform.LookAt(target);
    }
}