using Unity.VisualScripting;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField] private float moveDistance = 10f;//移動距離
    [SerializeField] private float moveSpeed = 5f;     //移動スピード
    private bool isMoveUp = false;                    //上方向に移動するか
    private float initialY;                           //Y初期位置
    private float initialX;                           //X初期位置

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
            newY += moveSpeed * Time.deltaTime;//上に動かす
            if (newY >= initialY + moveDistance)
            {
                isMoveUp = false;
            }
        }
        else
        {
            newY -= moveSpeed * Time.deltaTime;//下に動かす
            if (newY <= initialY - moveDistance)
            {
                isMoveUp = true;
            }
        }
        //新しい位置を設定
        transform.localPosition = new Vector3(transform.localPosition.x, newY, transform.localPosition.z);
    }
    void ReftLightPress()
    {
        float newX = transform.localPosition.x;

            if (isMoveUp)
            {
                newX += moveSpeed * Time.deltaTime;//上に動かす
                if (newX >= initialX + moveDistance)
                {
                    isMoveUp = false;
                }
            }
            else
            {
                newX -= moveSpeed * Time.deltaTime;//下に動かす
                if (newX <= initialX - moveDistance)
                {
                    isMoveUp = true;
                }
            }
            //新しい位置を設定
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
