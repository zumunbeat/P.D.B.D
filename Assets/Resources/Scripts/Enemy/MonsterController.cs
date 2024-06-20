using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
    private float speed;
    private int direct;
    Rigidbody2D rigid;
    public Vector2 minX, maxX, minZ, maxZ;
    public float groundcheckDistance = 0.2f;
    public LayerMask groundMask;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private void Start()
    {
        animator = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        Think();
    }

    private void FixedUpdate()
    {

        rigid.velocity = new Vector2(direct, rigid.velocity.y);

        Vector2 frontVec = new Vector2(rigid.position.x + direct * 0.5f, rigid.position.y);
        RaycastHit2D rayHit = Physics2D.Raycast(frontVec, Vector2.down, 1 , LayerMask.GetMask("ground"));
        if (rayHit.collider == null)
        {
            Turn();
        }
    }

    private void Think()
    {
        //-1 = <- , 0 = idle, 1= -> 
        direct = Random.Range(-1, 2);
        if(direct != 0)
        {
            spriteRenderer.flipX = (direct != 1);
        }
        animator.SetInteger("State", direct);
        float nextThinkTime = Random.Range(2f, 5f);
        Invoke("Think", nextThinkTime);
        
    }
    private void Turn()
    {
       direct *= -1;
       spriteRenderer.flipX = (direct != 1);
       CancelInvoke();
       Invoke("Think", 5);
     
    }

}
