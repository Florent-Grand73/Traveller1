using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController2D : MonoBehaviour
{
    Animator animator;
    Rigidbody2D rb2d;
    SpriteRenderer spriteRenderer;

    bool isGrounded;

    
    public float jumpforce = 4.5f;
    
    public float moveSpeed = 4f;

    [SerializeField]Transform groundCheck;
    //Wallstuff
    public Transform wallGrabPoint;
    private bool canGrab, isGrabbing;
    public LayerMask whatisGround;
    private float gravityStore;
    public float wallJumpTime = .2f;
    private float wallJumpCounter;

    //end wall stuff


    // Attack stuff
   
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;
    //end
    // Start is called before the first frame update
    void Start()
    {
      
        

        animator = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        //walljump
        gravityStore = rb2d.gravityScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Attack();
        }
        if (wallJumpCounter <= 0)
        {
            //flip direction
            if (rb2d.velocity.x > 0)
            {
                transform.localScale = Vector3.one;
            }
            else if (rb2d.velocity.x < 0)
            {
                transform.localScale = new Vector3(-1, 1, 1f);
            }
            //handle wall jump
            canGrab = Physics2D.OverlapCircle(wallGrabPoint.position, .2f, whatisGround);

            isGrabbing = false;
            if (canGrab && !isGrounded)
            {
                if ((transform.localScale.x == 1f && Input.GetAxisRaw("Horizontal") > 0) || (transform.localScale.x == -1f && Input.GetAxisRaw("Horizontal") < 0))
                {
                    isGrabbing = true;
                }
            }

            if (isGrabbing)
            {
                rb2d.gravityScale = 0f;
                rb2d.velocity = Vector2.zero;

                if (Input.GetButtonDown("Jump"))
                {
                    wallJumpCounter = wallJumpTime;

                    rb2d.velocity = new Vector2(-Input.GetAxisRaw("Horizontal") * moveSpeed, jumpforce);
                    rb2d.gravityScale = gravityStore;
                    isGrabbing = false;
                }
            }
            else
            {
                rb2d.gravityScale = gravityStore;
            }

            




            if (Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground")))
            {
                isGrounded = true;
            }
            else
            {
                isGrounded = false;
            }

            if (Input.GetKey("d") || Input.GetKey("right"))
            {
                rb2d.velocity = new Vector2(moveSpeed, rb2d.velocity.y);
                if (isGrounded)
                    animator.Play("PlayerRunAnim");
                //spriteRenderer.flipX = false;
            }
            else if (Input.GetKey("q") || Input.GetKey("left"))
            {
                rb2d.velocity = new Vector2(-moveSpeed, rb2d.velocity.y);
                if (isGrounded)
                    animator.Play("PlayerRunAnim");
                // spriteRenderer.flipX = true;
            }
            else
            {
                if (isGrounded && !Input.GetKeyDown(KeyCode.A))
                    animator.Play("Idle_Player");
                rb2d.velocity = new Vector2(0, rb2d.velocity.y);
            }

            if (Input.GetKey("space") && isGrounded)
            {
                rb2d.velocity = new Vector2(rb2d.velocity.x, jumpforce);
                animator.Play("PlayerJumpAnim");
            }

        }
        else
        {
            wallJumpCounter -= Time.deltaTime;
        }
        

    }

    void Attack()
    {
        animator.Play("PlayerAttack1");

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);


        foreach (Collider2D enemy in hitEnemies)
        {
            Debug.Log("We hit" + enemy.name);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    private void FixedUpdate()
    {
        
        

            
        
      


    }
}
