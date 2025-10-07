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

    private bool isMoveUp = false;    // �㉺�ړ����̕����Ǘ�
    public bool moveVertical = false; // true�Ȃ�㉺�ړ��Afalse�Ȃ獶�E�ړ�//������Ɉړ����邩
    private Vector3 initialPos;
    public float moveDistance = 3f;   // �ړ��ł��鋗��
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
        initialPos = transform.position;
        //�^�O�ňړ���������
        if (gameObject.CompareTag("UpDown"))
        {
            moveVertical = true;
            isMoveUp = true; // ������X�^�[�g
        }
        else if (gameObject.CompareTag("LeftRight"))
        {
            moveVertical = false;
            slideDirection = 1; // �E�����X�^�[�g
        }
    }

    void Update()
    {
        float xDistance = Mathf.Abs(transform.position.x - player.position.x);

        if(!isSlide && xDistance <= attackRangeX)
        {
            isSlide = true;

            if(!isMoveUp)
            {
                //�ŏ��̕������L�^
                slideDirection = Mathf.Sign(player.position.x - transform.position.x);

                //����Player�Ɠ����ʒu�ɂȂ�����i0�j
                if (slideDirection == 0) slideDirection = 1;
            }
            else
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
        //if (isSlide)
        //{
        //   transform.position += new Vector3(slideDirection, 0, 0) * speed * Time.deltaTime;
        //}
        if (isSlide)
        {
            Vector3 pos = transform.position;

            if (moveVertical)
            {
                // �㉺�ړ�
                float newY = pos.y + (isMoveUp ? speed : -speed) * Time.deltaTime;

                if (newY >= initialPos.y + moveDistance)
                {
                    newY = initialPos.y + moveDistance;
                    isMoveUp = false;
                }
                else if (newY <= initialPos.y - moveDistance)
                {
                    newY = initialPos.y - moveDistance;
                    isMoveUp = true;
                }

                transform.position = new Vector3(pos.x, newY, pos.z);
            }
            else
            {
                // ���E�ړ�
                float newX = pos.x + slideDirection * speed * Time.deltaTime;

                // �ړ��͈̓`�F�b�N�ioptional�j
                if (newX >= initialPos.x + moveDistance)
                {
                    newX = initialPos.x + moveDistance;
                    slideDirection = -1;
                }
                else if (newX <= initialPos.x - moveDistance)
                {
                    newX = initialPos.x - moveDistance;
                    slideDirection = 1;
                }

                transform.position = new Vector3(newX, pos.y, pos.z);
            }
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
