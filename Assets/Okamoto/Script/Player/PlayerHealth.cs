using UnityEngine;

using UnityEngine.InputSystem;

public class PlayerHealth : MonoBehaviour

{

    public int maxHP = 100;

    public int currentHP = 100;

    private MeshRenderer mr;

    [SerializeField] private MeshRenderer m;

    [SerializeField] private PlayerDestoroy playerDestoroy;

    [Header("HPに応じたスプライト（マテリアル）")]

    public Material sprite100;

    public Material sprite80;

    public Material sprite60;

    public Material sprite40;

    public Material sprite20;

    [Header("壊れたときのプレハブ")]

    public GameObject brokenPrefab;

    public GameObject child;

    private Renderer matRenderer;

    public bool isDead = false;

    // 破片関連の設定

    public GameObject fragmentPrefab;

    public int fragmentCount = 10;

    public float force = 500f;

    [SerializeField] private SEManager seManager;
    void Start()

    {

        if (child == null && transform.childCount > 0)

        {

            child = transform.GetChild(0).gameObject;

        }

        matRenderer = child != null ? child.GetComponentInChildren<Renderer>() : null;

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

        seManager.PlayerDamageSE();// SE

        currentHP = Mathf.Clamp(currentHP, 0, maxHP);

        UpdateSprite();

        if (currentHP <= 0)
        {
            Die();
        }

    }

    public void UpdateSprite()

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

    public void Die()

    {

        if (isDead) return;

        isDead = true;

        Debug.Log("死んだ");

        // 破片を飛ばす演出

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

        if (playerDestoroy != null)

        {
            seManager.PlayerDestroySE();
            Invoke(nameof(LoadNextScene), 0.2f); // ← 0.2秒後にシーン遷移
           

        }

        else

        {

            Debug.LogWarning("playerDestoroy がアサインされていません。");

        }

        mr = GetComponent<MeshRenderer>();

        if (mr != null) mr.enabled = false;

        if (m != null) m.enabled = false;

    }

    private void LoadNextScene()
    {
        StartCoroutine(playerDestoroy.LoadSelectionScene());
    }

}
