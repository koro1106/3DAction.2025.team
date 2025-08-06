using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerHealth : MonoBehaviour
{
    public int maxHP = 100;
    public int currentHP = 100;

    [Header("HPに応じたスプライト（マテリアル）")]
    public Material sprite100;
    public Material sprite80;
    public Material sprite60;
    public Material sprite40;
    public Material sprite20;

    [Header("壊れたときのプレハブ")]
    public GameObject brokenPrefab;

    public GameObject child;
    // Renderer に変更
    private Renderer matRenderer;

    private bool isDead = false;

    void Start()
    {
        // 子オブジェクトの Renderer を探す
        matRenderer = child.GetComponentInChildren<Renderer>();
        if (matRenderer == null)
        {
            Debug.LogError("Renderer が見つかりません！Visual 子オブジェクトを確認してください。");
        }
        UpdateSprite();
    }

    void Update()
    {
        // スペースキーで5ダメージテスト
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            TakeDamage(5);
        }
    }

    public void TakeDamage(int amount)
    {
        if (isDead) return;

        currentHP -= amount;
        currentHP = Mathf.Clamp(currentHP, 0, maxHP);
        UpdateSprite();

        if (currentHP <= 0)
        {
            Die();
        }
    }

    void UpdateSprite()
    {
        if (matRenderer == null) return;

        if (currentHP > 80 && sprite100 != null)
            matRenderer.material = sprite100;
        else if (currentHP > 60 && sprite80 != null)
            matRenderer.material = sprite80;
        else if (currentHP > 40 && sprite60 != null)
            matRenderer.material = sprite60;
        else if (currentHP > 20 && sprite40 != null)
            matRenderer.material = sprite40;
        else if (sprite20 != null)
            matRenderer.material = sprite20;
    }

    void Die()
    {
        isDead = true;

        if (brokenPrefab != null)
        {
            GameObject broken = Instantiate(brokenPrefab, transform.position, transform.rotation);
            foreach (Rigidbody rb in broken.GetComponentsInChildren<Rigidbody>())
            {
                Vector3 force = Random.onUnitSphere * Random.Range(2f, 5f);
                rb.AddForce(force, ForceMode.Impulse);
            }
        }

        Destroy(gameObject);
    }
}
