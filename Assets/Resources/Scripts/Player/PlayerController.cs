using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public int jumpPower;
    public Transform attackTransform;
    private int moveSpeed;
    public float attackRadius;
    public int attackPower;
    Animator animator;
    Rigidbody2D rigid;
    SpriteRenderer sprite;
    Transform trans;
    public Image img;
    void Start()
    {
        animator = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        trans = GetComponent<Transform>();
        moveSpeed = 2;
        attackRadius = 0.5f;
        attackPower = 2;
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
        /**
        if (Input.GetMouseButtonDown(0))
        {
            if (Time.time >= nextAttackTime)
            {
                Attack();
            }
        }
        **/
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
    
    void Attack()
    {
        animator.SetTrigger("onAttack");

        Collider2D[] enemies = Physics2D.OverlapCircleAll(attackTransform.position, attackRadius, LayerMask.GetMask("Monster"));

        if (enemies != null)
        {
            foreach (Collider2D enemy in enemies)
            {
                //enemy.GetComponent<MonsterController>().TakeDamage(attackPower);
            }

            //nextAttackTime = Time.time + attackReload;
        }
    }
    /**
    public override void TakeDamage(int damage)
    {
        if (animator.GetBool("isDeath"))
            return;

        base.TakeDamage(damage);
        img.fillAmount = health / maxHealth;

        animator.SetTrigger("onHit");
    }
    public override void Die()
    {
        animator.SetBool("isDeath", true);
        this.enabled = false;
        GameManager.instance.GameOver(false);
    }
    **/
}