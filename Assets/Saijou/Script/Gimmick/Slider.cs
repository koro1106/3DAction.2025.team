using UnityEngine;

public class Slider : MonoBehaviour
{
    public Transform player;
    public float attackRangeX = 10f; //攻撃判定距離
    public float speed = 5f;         //攻撃スピード

    private Rigidbody rb;
    private Collider col;
    private bool isSlide = false;
    private float slideDirection = 0f;//スライダーの方向
    private PlayerHealth playerHealth;

    private bool isMoveUp = false;    // 上下移動時の方向管理
    public bool moveVertical = false; // trueなら上下移動、falseなら左右移動//上方向に移動するか
    private Vector3 initialPos;
    public float moveDistance = 3f;   // 移動できる距離
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
        initialPos = transform.position;
        //タグで移動方向判定
        if (gameObject.CompareTag("UpDown"))
        {
            moveVertical = true;
            isMoveUp = true; // 上方向スタート
        }
        else if (gameObject.CompareTag("LeftRight"))
        {
            moveVertical = false;
            slideDirection = 1; // 右方向スタート
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
                //最初の方向を記録
                slideDirection = Mathf.Sign(player.position.x - transform.position.x);

                //もしPlayerと同じ位置になったら（0）
                if (slideDirection == 0) slideDirection = 1;
            }
            else
            {

            }
           
        }
        if (xDistance > attackRangeX)
        {
            isSlide = false;
            rb.velocity = Vector2.zero;//止まる
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
                // 上下移動
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
                // 左右移動
                float newX = pos.x + slideDirection * speed * Time.deltaTime;

                // 移動範囲チェック（optional）
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
            col.enabled = false;//playerと接触したら通過する
        }
    }
}
