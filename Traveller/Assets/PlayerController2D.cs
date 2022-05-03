using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController2D : MonoBehaviour
{
    Animator animator;
    Rigidbody2D rb2d;
    SpriteRenderer spriteRenderer;

    bool isGrounded;

    [SerializeField]Transform groundCheck;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if(Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground")))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }

        if(Input.GetKey("d") || Input.GetKey("right"))
        {
            rb2d.velocity = new Vector2(4, rb2d.velocity.y);
            if(isGrounded)
               animator.Play("PlayerRunAnim");
            spriteRenderer.flipX = false;
        }
        else if(Input.GetKey("q") || Input.GetKey("left"))
        {
            rb2d.velocity = new Vector2(-4, rb2d.velocity.y);
            if(isGrounded)
               animator.Play("PlayerRunAnim");
            spriteRenderer.flipX = true;
        }
        else
        {
            if(isGrounded)
               animator.Play("Idle_Player");
            rb2d.velocity = new Vector2(0, rb2d.velocity.y);
        }

        if (Input.GetKey("space") && isGrounded)
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, 4.5f);
            animator.Play("PlayerJumpAnim");
        }
    }
}
