using UnityEngine;
public class FollowTarget : MonoBehaviour
{
    [Header("�Ǐ]�Ώ�")]
    public Transform target; // �v���C���[�Ȃ�
    [Header("�Ǐ]�ݒ�")]
    [Tooltip("�^�[�Q�b�g�Ƃ̃I�t�Z�b�g�i�ʒu����j")]
    public Vector3 offset = new Vector3(0, 2f, -5f);
    [Tooltip("�Ǐ]�X�s�[�h")]
    public float followSpeed = 5f;
    [Tooltip("�^�[�Q�b�g�Ƃ̋��������͈͓̔��Ȃ瓮���Ȃ�")]
    public float followRange = 0.1f;
    [Header("�Ǐ]�͈͐����i�I�v�V�����j")]
    public bool useClamp = false;
    public Vector2 minPosition; // x, y �̍ŏ�
    public Vector2 maxPosition; // x, y �̍ő�
    void LateUpdate()
    {
        if (target == null) return;
        // �ڕW�ʒu���v�Z
        Vector3 targetPos = target.position + offset;
        // ���݈ʒu�ƖڕW�ʒu�̋����𑪂�
        float distance = Vector3.Distance(transform.position, targetPos);
        // ��苗���ȏ㗣��Ă�����Ǐ]
        if (distance > followRange)
        {
            transform.position = Vector3.Lerp(
                transform.position,
                targetPos,
                followSpeed * Time.deltaTime
            );
        }
        // �͈͐������L���Ȃ�Clamp
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