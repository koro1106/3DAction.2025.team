using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerHealth : MonoBehaviour
{
    public int maxHP = 100;
    public int currentHP = 100;
    [Header("HP�ɉ������X�v���C�g")]
    public Sprite sprite100;
    public Sprite sprite80;
    public Sprite sprite60;
    public Sprite sprite40;
    public Sprite sprite20;
    [Header("�j��G�t�F�N�g")]
    public GameObject brokenPrefab;  // ��ꂽ�p�[�c�v���n�u
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
            Debug.LogWarning("SpriteRenderer �� null �ł��B");//�����Ȃ񂩌����Ă�
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
            Debug.LogWarning("�ǂ� Sprite ���ݒ肳��Ă��܂���I");
    }
    void Die()
    {
        isDead = true;
        // �o���o���̃v���n�u���o��
        if (brokenPrefab != null)
        {
            GameObject broken = Instantiate(brokenPrefab, transform.position, transform.rotation);
            // �e�p�[�c�ɔ����I�ȗ͂�������i�I�v�V�����j
            foreach (Rigidbody rb in broken.GetComponentsInChildren<Rigidbody>())
            {
                Vector3 force = Random.onUnitSphere * Random.Range(2f, 5f);
                rb.AddForce(force, ForceMode.Impulse);
            }
        }
        Destroy(gameObject); // �������g�i�v���C���[�j��j��
    }
}