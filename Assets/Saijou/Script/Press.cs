using Unity.VisualScripting;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField] private float moveDistance = 10f;//�ړ�����
    [SerializeField] private float moveSpeed = 5f;     //�ړ��X�s�[�h
    private bool isMoveUp = false;                    //������Ɉړ����邩
    private float initialY;                           //Y�����ʒu
    private float initialX;                           //X�����ʒu

    private PlayerHealth playerHealth;
    void Start()
    {
        initialY = transform.localPosition.y;
        initialX = transform.localPosition.x;

    }

    void Update()
    {
       // if (playerHealth.isDead) return;

        if (gameObject.tag == "UpDown")
        {
          UpDownPress();
        }
        if (gameObject.tag == "LeftRight")
        {
          ReftLightPress();
        }
    }


    void UpDownPress()
    {
        float newY = transform.localPosition.y;
        if (isMoveUp)
        {
            newY += moveSpeed * Time.deltaTime;//��ɓ�����
            if (newY >= initialY + moveDistance)
            {
                isMoveUp = false;
            }
        }
        else
        {
            newY -= moveSpeed * Time.deltaTime;//���ɓ�����
            if (newY <= initialY - moveDistance)
            {
                isMoveUp = true;
            }
        }
        //�V�����ʒu��ݒ�
        transform.localPosition = new Vector3(transform.localPosition.x, newY, transform.localPosition.z);
    }
    void ReftLightPress()
    {
        float newX = transform.localPosition.x;

            if (isMoveUp)
            {
                newX += moveSpeed * Time.deltaTime;//��ɓ�����
                if (newX >= initialX + moveDistance)
                {
                    isMoveUp = false;
                }
            }
            else
            {
                newX -= moveSpeed * Time.deltaTime;//���ɓ�����
                if (newX <= initialX - moveDistance)
                {
                    isMoveUp = true;
                }
            }
            //�V�����ʒu��ݒ�
            transform.localPosition = new Vector3(newX, transform.localPosition.y, transform.localPosition.z);
        
    }

    private void OnTriggerEnter(Collider other)
    {
        playerHealth = other.GetComponent<PlayerHealth>();
        
        if (other.CompareTag("Player"))
        {
            playerHealth.TakeDamage(100);
        }
    }
}
