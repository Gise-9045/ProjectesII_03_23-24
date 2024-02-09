using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class PlayerMovement : MonoBehaviour
{
    private Player player;

    private Vector2 actualMovement;

    private Rigidbody2D rb;

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



    void Start()
    {
        player = GetComponent<Player>();
        rb = GetComponent<Rigidbody2D>();
        ground = GetComponentInChildren<PlayerGroundDetection>();
        isJumping = false;
        onStairs = false;
        coyoteTime = 0.3f;
    }

    void Update()
    {
        Walk();

        if(onStairs)
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
        else
        {
            rb.gravityScale = 9.81f;

        }

        Jump();

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
            slide = 0.3f;
            player.SetDirection(new Vector2(-1, player.GetDirection().y));

            rb.velocity = new Vector2(player.GetDirection().x * player.GetSpeed(), rb.velocity.y);

        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            slide = 0.3f;
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
            actualCoyoteTime = 0f;
            isJumping = true;
            actualJumpTimeCounter = jumpTimeCounter;
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
        else if (canDoubleJump && doubleJump < 1 && Input.GetKeyDown(KeyCode.Space))
        {
            isJumping = true;
            actualJumpTimeCounter = jumpTimeCounter;
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
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
    /* void Jump()
     {
         if (ground.OnGround())
         {
             actualCoyoteTime = coyoteTime;
         }
         else
         {
             actualCoyoteTime -= Time.deltaTime;
         }


         if (actualCoyoteTime > 0 && Input.GetKeyDown(KeyCode.Space) && !isJumping)
         {
             rb.gravityScale = 9.81f;

             actualCoyoteTime = 0f;
             isJumping = true;
             actualJumpTimeCounter = jumpTimeCounter;
             rb.velocity = new Vector2(rb.velocity.x, Vector2.up.y * jumpForce);
         }

         if (Input.GetKey(KeyCode.Space) && isJumping)
         {

             if (actualJumpTimeCounter > 0)
             {
                 rb.velocity = new Vector2(rb.velocity.x, Vector2.up.y * jumpForce);
                 actualJumpTimeCounter -= Time.deltaTime;
             }
             else
             {
                 isJumping = false;

             }
         }

         if (Input.GetKeyUp(KeyCode.Space))
         {
             isJumping = false;
             actualCoyoteTime = 0f;
         }
     }*/
    public void SetDoubleJump(bool condition)
    {
        canDoubleJump = condition;
    }

    void Stairs()
    {
        if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
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
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        onStairs = false;
        usingStairs = false;
    }
}
