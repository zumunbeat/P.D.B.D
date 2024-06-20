using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private BattleManager battlemanager;
    public int jumpPower;
    public Transform attackTransform;
    private int moveSpeed;
    public float attackRadius;
    public int attackPower;
    public float attackReload;
    Animator animator;
    Rigidbody2D rigid;
    SpriteRenderer sprite;
    Transform trans;
    public Image img;
    float nextAttackTime;
    RaycastHit2D playerattack;

   void Start()
    {
        animator = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        trans = GetComponent<Transform>();
        moveSpeed = 2;
        attackRadius = 0.5f;
        attackPower = 2;
        attackReload = 0.2f;
        Physics2D.Raycast(rigid.position, Vector2.down, 0.5f, LayerMask.GetMask("enemy"));
    }

    void Update()
    {
        if (Input.GetButton("Horizontal"))
        {
            Move();
        }
        else
        {
            animator.SetInteger("State", 0);
        }

        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }

        if (rigid.velocity.y < 0)
        {
            Landing();
        }
        if (playerattack.collider != null)
        {
            SceneManager.LoadScene("Battle");
        }
    }
    void Move()
    {
        float hInput = Input.GetAxisRaw("Horizontal");

        animator.SetInteger("State", 1);

        if (hInput < 0)
        {
            Vector3 scale = transform.localScale;
            scale.x = -Mathf.Abs(scale.x);
            transform.localScale = scale;
        }
        else if (hInput > 0)
        {
            Vector3 scale = transform.localScale;
            scale.x = Mathf.Abs(scale.x);
            transform.localScale = scale;
        }

        trans.Translate(Vector2.right * hInput * moveSpeed * Time.deltaTime);
    }
    void Jump()
    {
        rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
        animator.SetBool("isJump", true);
    }
    
    void Landing()
    {
        RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, Vector2.down, 1f, LayerMask.GetMask("ground"));
        
        
        Debug.DrawRay(rigid.position, Vector2.down, Color.red);
       
        if (rayHit.collider != null)
        {
            if (rayHit.distance <= 1f)
            {
                animator.SetBool("isJump", false);
            }
        }
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "enemy")
        {
            SceneManager.LoadScene("Battle");
        }
    }
    
}