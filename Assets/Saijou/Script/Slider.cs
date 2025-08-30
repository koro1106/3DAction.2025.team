using UnityEngine;

public class Slider : MonoBehaviour
{
    public Transform player;
    public float attackRangeX = 10f; //�U�����苗��
    public float speed = 5f;         //�U���X�s�[�h

    private Rigidbody rb;
    private Collider col;
    private bool isSlide = false;
    private float slideDirection = 0f;//�X���C�_�[�̕���
    private PlayerHealth playerHealth;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
    }

    void Update()
    {
        float xDistance = Mathf.Abs(transform.position.x - player.position.x);

        if(!isSlide && xDistance <= attackRangeX)
        {
            isSlide = true;

            //�ŏ��̕������L�^
            slideDirection = Mathf.Sign(player.position.x - transform.position.x);

            //����Player�Ɠ����ʒu�ɂȂ�����i0�j
            if (slideDirection == 0)
            {

            }
        }
        if (xDistance > attackRangeX)
        {
            isSlide = false;
            rb.velocity = Vector2.zero;//�~�܂�
        }
    }

    private void FixedUpdate()
    {
        if (isSlide)
        {
           transform.position += new Vector3(slideDirection, 0, 0) * speed * Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerHealth = other.GetComponent<PlayerHealth>();
            playerHealth.TakeDamage(1);
            col.enabled = false;//player�ƐڐG������ʉ߂���
        }
    }
}
