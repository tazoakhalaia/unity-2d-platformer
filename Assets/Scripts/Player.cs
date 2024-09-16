using UnityEngine;

public class Player : MonoBehaviour
{

    public float speed = 5;
    public float jumpHight = 9;
    private bool isGrounded = false;
    private Vector3 rotation;
    private Rigidbody2D rb;
    private Animator anim;
    private CoinManager m;
    public GameObject panel;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        rb.freezeRotation = true;
        rotation = transform.eulerAngles;
        m = FindFirstObjectByType<CoinManager>();
    }

    void Update()
    {
        float moveX = Input.GetAxis("Horizontal");

        if(moveX != 0)
        {
            anim.SetBool("isRunning", true);
        }else
        {
            anim.SetBool("isRunning", false);
        }

        if(moveX < 0)
        {
            transform.eulerAngles = rotation - new Vector3(0, 180, 0);
            transform.Translate(Vector2.right * speed * -moveX * Time.deltaTime);
        }

        if (moveX > 0)
        {
            transform.eulerAngles = rotation;
            transform.Translate(Vector2.right * speed * moveX * Time.deltaTime);
        }


        if (isGrounded == false)
        {
            anim.SetBool("isJumping", true);
        }else
        {
            anim.SetBool("isJumping", false);
        }

        if (Input.GetKey(KeyCode.Space) && isGrounded){
            rb.AddForce(Vector2.up * jumpHight, ForceMode2D.Impulse);
            isGrounded = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
       if(collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Coin")
        {
            m.AddMoney();
            Destroy(collision.gameObject);
        }

        if(collision.gameObject.tag == "Spike")
        {
            panel.SetActive(true);
            Destroy(gameObject);
        }
    }

}
