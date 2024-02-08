using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

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



    void Start()
    {
        player = GetComponent<Player>();
        rb = GetComponent<Rigidbody2D>();
        ground = GetComponentInChildren<PlayerGroundDetection>();
        isJumping = false;
    }

    void Update()
    {
        //if (Input.GetKey(KeyCode.A))
        //{
        //    player.SetDirection(new Vector2(-1, player.GetDirection().y));

        //    rb.velocity = new Vector2(player.GetDirection().x * player.GetSpeed(), rb.velocity.y);

        //}
        //else if (Input.GetKey(KeyCode.D))
        //{
        //    player.SetDirection(new Vector2(1, player.GetDirection().y));

        //    rb.velocity = new Vector2(player.GetDirection().x * player.GetSpeed(), rb.velocity.y);

        //}


        if (ground.OnGround() && Input.GetKeyDown(KeyCode.Space))
        {
            isJumping = true;
            actualJumpTimeCounter = jumpTimeCounter;
            rb.velocity = Vector2.up * jumpForce;

        }
            
        if (Input.GetKey(KeyCode.Space) && isJumping)
        {

            if (actualJumpTimeCounter > 0)
            {
                 rb.velocity = Vector2.up * jumpForce;
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
        }
    }
}
