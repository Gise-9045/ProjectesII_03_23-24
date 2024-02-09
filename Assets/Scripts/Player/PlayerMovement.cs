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
    private bool canDoubleJump = false;
    private float slide;



    void Start()
    {
        player = GetComponent<Player>();
        rb = GetComponent<Rigidbody2D>();
        ground = GetComponentInChildren<PlayerGroundDetection>();
        isJumping = false;
        coyoteTime = 0.3f;
    }

    void Update()
    {
        Walk();
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


        if (Input.GetKey(KeyCode.A))
        {
            slide = 0.3f;
            player.SetDirection(new Vector2(-1, player.GetDirection().y));

            rb.velocity = new Vector2(player.GetDirection().x * player.GetSpeed(), rb.velocity.y);

        }
        else if (Input.GetKey(KeyCode.D))
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
    }
    public void SetDoubleJump(bool condition)
    {
        canDoubleJump = condition;
    }
}
