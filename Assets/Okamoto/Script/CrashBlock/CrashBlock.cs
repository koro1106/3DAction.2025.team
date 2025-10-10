using UnityEngine;
public class CrashBlock : MonoBehaviour
{
    [Header("�j�Њ֘A")]
    public GameObject fragmentPrefab; // �j�Ђ̃v���n�u
    public int fragmentCount = 10;    // �j�Ђ̐�
    public float force = 300f;        // ��΂���
    [Header("�_���[�W��")]
    public int damageToPlayer = 1;    // �v���C���[�ɗ^����_���[�W
    private bool isBroken = false;
    [SerializeField] private SEManager seManager; // SE
    void OnCollisionEnter(Collision collision)
    {
       
        if (isBroken) return;
        if (collision.gameObject.CompareTag("Player"))
        {
            seManager.CrushBlockSE(); // SE
            // �v���C���[HP�����炷
            PlayerHealth player = collision.gameObject.GetComponent<PlayerHealth>();
            if (player != null)
            {
                player.TakeDamage(damageToPlayer);
               
            }
            // ������j��
            BreakBlock();
        }
    }
    void BreakBlock()
    {
        isBroken = true;
        // �j�Ђ𐶐����Ĕ�΂�
        if (fragmentPrefab != null)
        {
            for (int i = 0; i < fragmentCount; i++)
            {
                Vector3 randomPosition = transform.position + Random.insideUnitSphere * 0.5f;
                GameObject fragment = Instantiate(fragmentPrefab, randomPosition, Random.rotation);
                Rigidbody rb = fragment.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.AddForce(Random.insideUnitSphere * force);
                }
            }
        }
        // ���̃u���b�N���폜
        Destroy(gameObject);
    }
}
