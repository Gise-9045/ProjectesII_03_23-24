using Cinemachine.Utility;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
//using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class PlayerMovement : MonoBehaviour
{
    private Player player;

    private Vector2 stairsPos;

    private Rigidbody2D rb;
    private Transform tr;

    [SerializeField] private float jumpForce;
    [SerializeField] private float jumpTimeCounter;
    private float actualJumpTimeCounter;


    private PlayerGroundDetection ground;

    bool isJumping;

    private float coyoteTime;
    private float actualCoyoteTime;
    private int doubleJump = 0;
    [SerializeField] private bool canDoubleJump = false;
    private float slide;

    bool onStairs;
    bool usingStairs = false;

    bool dashing = false;
    [SerializeField] float dashVelocity;
    float actualDashTimer;
    [SerializeField] float dashTimer;
    [SerializeField] bool canDash;
    [SerializeField] float dashCooldown;
    [SerializeField] private ParticleSystem dashTrail;
    [SerializeField] private ParticleSystem deathParticles;
    float actualDashCooldown = 0;

    [SerializeField]
    private ParticleSystem walkParticles;
    [SerializeField]
    private ParticleSystem jumpParticles;

    private Animator animator;

    private bool oldDead;

    void Start()
    {
        
        player = GetComponent<Player>();
        rb = GetComponent<Rigidbody2D>();
        ground = GetComponentInChildren<PlayerGroundDetection>();
        tr = GetComponentInChildren<Transform>();
        isJumping = false;
        onStairs = false;
        coyoteTime = 0.3f;
        animator = GetComponent<Animator>();

        ground.OnGroundTouchdown += jumpParticles.Play;
        ground.OnGroundTouchdown += walkParticles.Play;

        ground.OnLeaveGround += walkParticles.Stop;

        oldDead = false;
    }

    void Update()
    {
        if (player.GetDead())
        {
            animator.SetBool("Stairs", false);
            animator.SetBool("Dash", false);
            animator.SetBool("Walk", false);
            animator.SetBool("Grounded", true);


            animator.SetBool("Death", true);

            if(player.GetDead() && player.GetDead() != oldDead)
            {
                deathParticles.Play();
            }

            rb.gravityScale = 0f;
            rb.velocity = Vector2.zero;

            oldDead = player.GetDead();

            return;
        }
        else
        {
            animator.SetBool("Death", false);
            oldDead = player.GetDead();
        }

        animator.SetFloat("FallVelocity", rb.velocity.y);
        animator.SetBool("Grounded", ground.OnGround() || onStairs);
        animator.SetBool("Stairs", onStairs && (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S)));
        animator.SetBool("Dash", dashing);
        animator.SetFloat("DashVelocity", rb.velocity.x);

        animator.SetBool("Walk", Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow));

        Walk();
        DashCheck();

        if (onStairs)
        {
            if (usingStairs)
            {
                Stairs();
                rb.gravityScale = 0f;
            }
            else
            {
                usingStairs = Input.GetKeyDown(KeyCode.W) || rb.velocity.y <= 0.0f;
            }

        }
        else if (canDash && !dashing && Input.GetKeyDown(KeyCode.LeftShift) && actualDashCooldown <= 0)
        {
            Dash();
        }
        else if(!dashing)
        {
            rb.gravityScale = 9.81f;
        }

        CheckJump();
    }

    void Walk()
    {

        if(slide > 0)
        {
            slide -= Time.deltaTime;
        }
        else if(slide < 0)
        {
            slide = 0;
        }


        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            slide = 0.1f;
            player.SetDirection(new Vector2(-1, player.GetDirection().y));

            rb.velocity = new Vector2(player.GetDirection().x * player.GetSpeed(), rb.velocity.y);

        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            slide = 0.1f;
            player.SetDirection(new Vector2(1, player.GetDirection().y));

            rb.velocity = new Vector2(player.GetDirection().x * player.GetSpeed(), rb.velocity.y);

        }
        else
        {
            rb.velocity = new Vector2(player.GetDirection().x * (player.GetSpeed() * slide), rb.velocity.y);

        }
    }
    void Jump()
    {
        isJumping = true;
        actualJumpTimeCounter = jumpTimeCounter;
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        jumpParticles.Play();
    }
    void CheckJump()
    {
        if (ground.OnGround())
        {
            actualCoyoteTime = coyoteTime;
            doubleJump = 0;
        }
        else
        {
            actualCoyoteTime -= Time.deltaTime;
        }

        if (actualCoyoteTime > 0 && Input.GetKeyDown(KeyCode.Space) && !isJumping)
        {
            Jump();
            actualCoyoteTime = 0f;
        }
        else if (canDoubleJump && doubleJump < 1 && Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
            doubleJump++; // Incrementa el contador de saltos después de un doble salto
        }

        if (Input.GetKey(KeyCode.Space) && isJumping)
        {
            if (actualJumpTimeCounter > 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                rb.gravityScale = 0.0f;
                actualJumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                rb.gravityScale = 9.81f;
                isJumping = false;
            }
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            isJumping = false;
            actualCoyoteTime = 0f;
            rb.gravityScale = 9.81f;
        }
    }

    void DashCheck()
    {
        if (actualDashCooldown > 0)
        {
            actualDashCooldown -= Time.deltaTime;
        }

        if (actualDashTimer > 0)
        {
            dashing = true;
            actualDashCooldown = 0.5f;
            rb.velocity = new Vector2(player.GetDirection().x * dashVelocity, 0);
            rb.gravityScale = 0f;
            actualDashTimer -= Time.deltaTime;
            
        }
        else
        {
            dashing = false;
           
        }
    }

    
    public void SetDoubleJump(bool condition)
    {
        canDoubleJump = condition;
    } 
    public void SetDash(bool condition)
    {
        canDash = condition;
    }

    void Stairs()
    {
        if((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)) || Input.GetKey(KeyCode.Space) && !(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow)))
        {
            tr.position = new Vector2((float)(tr.position.x + 0.05 * (stairsPos.x - tr.position.x)), tr.position.y);
        }

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            rb.gravityScale = 0f;
            rb.velocity = new Vector2(rb.velocity.x, 5);
        }
        else if(Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) 
        {
            rb.gravityScale = 0f;
            rb.velocity = new Vector2(rb.velocity.x, -5);
        }
        else
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Ladder")
        {
            onStairs = true;

            stairsPos = collision.transform.position;
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Ladder")
        {
            onStairs = false;
            usingStairs = false;
        }
    }
    private void Dash()
    {
        actualDashTimer = dashTimer;
        rb.gravityScale = 0f;
        dashTrail.Play();

    }
}
