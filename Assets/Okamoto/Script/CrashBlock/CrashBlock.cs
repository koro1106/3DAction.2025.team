using UnityEngine;
public class CrashBlock : MonoBehaviour
{
    [Header("破片関連")]
    public GameObject fragmentPrefab; // 破片のプレハブ
    public int fragmentCount = 10;    // 破片の数
    public float force = 300f;        // 飛ばす力
    [Header("ダメージ量")]
    public int damageToPlayer = 1;    // プレイヤーに与えるダメージ
    private bool isBroken = false;
    [SerializeField] private SEManager seManager; // SE
    void OnCollisionEnter(Collision collision)
    {
       
        if (isBroken) return;
        if (collision.gameObject.CompareTag("Player"))
        {
            seManager.CrushBlockSE(); // SE
            // プレイヤーHPを減らす
            PlayerHealth player = collision.gameObject.GetComponent<PlayerHealth>();
            if (player != null)
            {
                player.TakeDamage(damageToPlayer);
               
            }
            // 自分を破壊
            BreakBlock();
        }
    }
    void BreakBlock()
    {
        isBroken = true;
        // 破片を生成して飛ばす
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
        // 元のブロックを削除
        Destroy(gameObject);
    }
}
