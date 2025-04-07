//using UnityEditor.Animations;
//using UnityEngine;

//public class PlayerMove_KYH : MonoBehaviour
//{
//    public float moveSpeed = 5f; // 이동 속도
//    public float jumpForce = 5f; // 점프 힘
//    private Rigidbody2D rb;
//    private SpriteRenderer sr;
//    private AnimatorController ac;
//    private Animator anim;
//    private bool isGrounded;


//    [SerializeField] private float moveInput;

//    void Start()
//    {
//        rb = GetComponent<Rigidbody2D>();
//        sr = GetComponent<SpriteRenderer>();
//        ac = GetComponent<AnimatorController>();
//        anim = GetComponent<Animator>();
//    }

//    void FixedUpdate()
//    {
//        Move();
//        //Jump();
//    }

//    private void Update()
//    {
//        Jump();
//    }

//    void Move()
//    {
//        moveInput = Input.GetAxis("Horizontal");
//        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);
//        if (moveInput > 0.2f)
//        {
//            sr.flipX = false;
//            anim.SetInteger("isRun",1);
//            anim.SetInteger("State", 1);
            
//        }
//        else if (moveInput < -0.2f)
//        {
//            sr.flipX = true;
//            anim.SetInteger("isRun",1);
//            anim.SetInteger("State", 1);
//        }
//        else
//        {
//            anim.SetInteger("isRun",0);
//            anim.SetInteger("State", 0);
//        }
//    }

//    void Jump()
//    {
//        if (isGrounded && Input.GetButtonDown("Jump") ||
//            isGrounded && Input.GetKeyDown(KeyCode.W) ||
//            isGrounded && Input.GetKeyDown(KeyCode.UpArrow))
            
//            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
//    }
    

//    private void OnCollisionEnter2D(Collision2D collision)
//    {
//        if (collision.gameObject.CompareTag("Ground"))
//        {
//            isGrounded = true;
//            anim.SetInteger("isJump", 0);
//        }
//    }

//    private void OnCollisionExit2D(Collision2D collision)
//    {
//        if (collision.gameObject.CompareTag("Ground"))
//        {
//            isGrounded = false;
//            anim.SetInteger("isJump", 1);
//        }
//    }
//}
