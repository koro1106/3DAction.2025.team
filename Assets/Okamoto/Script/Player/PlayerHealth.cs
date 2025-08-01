using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerHealth : MonoBehaviour
{
    public int maxHP = 100;
    public int currentHP = 100;
    [Header("HPに応じたスプライト")]
    public Sprite sprite100;
    public Sprite sprite80;
    public Sprite sprite60;
    public Sprite sprite40;
    public Sprite sprite20;
    [Header("破壊エフェクト")]
    public GameObject brokenPrefab;  // 壊れたパーツプレハブ
    private SpriteRenderer spriteRenderer;
    private bool isDead = false;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        UpdateSprite();
    }
    void Update()
    {
       if(Keyboard.current.spaceKey.wasPressedThisFrame)
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
        if (spriteRenderer == null)
        {
            Debug.LogWarning("SpriteRenderer が null です。");//ここなんか言われてる
            return;
        }
        Debug.Log($"HP: {currentHP}");
        if (currentHP > 80 && sprite100 != null)
            spriteRenderer.sprite = sprite100;
        else if (currentHP > 60 && sprite80 != null)
            spriteRenderer.sprite = sprite80;
        else if (currentHP > 40 && sprite60 != null)
            spriteRenderer.sprite = sprite60;
        else if (currentHP > 20 && sprite40 != null)
            spriteRenderer.sprite = sprite40;
        else if (sprite20 != null)
            spriteRenderer.sprite = sprite20;
        else
            Debug.LogWarning("どの Sprite も設定されていません！");
    }
    void Die()
    {
        isDead = true;
        // バラバラのプレハブを出す
        if (brokenPrefab != null)
        {
            GameObject broken = Instantiate(brokenPrefab, transform.position, transform.rotation);
            // 各パーツに爆発的な力を加える（オプション）
            foreach (Rigidbody rb in broken.GetComponentsInChildren<Rigidbody>())
            {
                Vector3 force = Random.onUnitSphere * Random.Range(2f, 5f);
                rb.AddForce(force, ForceMode.Impulse);
            }
        }
        Destroy(gameObject); // 自分自身（プレイヤー）を破壊
    }
}