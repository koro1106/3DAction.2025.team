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

            //最初の方向を記録
            slideDirection = Mathf.Sign(player.position.x - transform.position.x);

            //もしPlayerと同じ位置になったら（0）
            if (slideDirection == 0)
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
            col.enabled = false;//playerと接触したら通過する
        }
    }
}
