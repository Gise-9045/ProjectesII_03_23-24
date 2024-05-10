using Cinemachine.Utility;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.Searcher.SearcherWindow.Alignment;
//using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class PlayerMovement : MonoBehaviour
{
    public enum PlayerStates { IDLE, HANDUP, STOP, DOOR }

    private Player player;

    private Rigidbody2D rb;
    private Transform tr;
    private SpriteRenderer childrenSprite;


    private PlayerGroundDetection ground;


    [SerializeField] public bool canPickUp = false;

    [Header("----- Particles -----")]
    [SerializeField] private ParticleSystem deathParticles1;
    [SerializeField] private ParticleSystem deathParticles2;
    [SerializeField] private ParticleSystem walkParticles;

    private Animator animator;


    private InputController controller;



    [SerializeField] private PlayerJump playerJump;
    [SerializeField] private PlayerWalk playerWalk;
    [SerializeField] private PlayerStairs playerStairs;
    [SerializeField] private PlayerDash playerDash;

    PlayerStates actualState = PlayerStates.IDLE;

    private PlayerPowerUpManager powerUpManager;

    private LvlTransitionWithoutKey doorWithoutKey;
    private Transform doorTr;
    private int doorDirection;

    void Start()
    {
        player = GetComponent<Player>();
        controller = GetComponent<InputController>();
        rb = GetComponent<Rigidbody2D>();
        tr = GetComponent<Transform>();
        ground = GetComponentInChildren<PlayerGroundDetection>();
        animator = GetComponent<Animator>();

        childrenSprite = GetComponentInChildren<SpriteRenderer>();

        //ground.OnGroundTouchdown += jumpParticles.Play;
        ground.OnGroundTouchdown += walkParticles.Play;

        ground.OnLeaveGround += walkParticles.Stop;

        powerUpManager = GetComponent<PlayerPowerUpManager>();
    }



    void Update()
    {
        if (actualState == PlayerStates.STOP)
        {
            animator.SetBool("Stairs", false);
            animator.SetBool("Dash", false);
            animator.SetBool("Walk", false);
            animator.SetBool("Grounded", true);
            rb.velocity = Vector2.zero;

            return;
        }
        else if(actualState == PlayerStates.HANDUP)
        {
            animator.SetBool("Stairs", false);
            animator.SetBool("Dash", false);
            animator.SetBool("Walk", false);
            animator.SetBool("Grounded", true);

            animator.SetBool("StartPose", true);

            return;
        }
        else if(actualState == PlayerStates.DOOR)
        {
            rb.velocity = new Vector2(doorDirection * player.GetSpeed(), rb.velocity.y);

            if(doorDirection == 1 && tr.transform.position.x > doorTr.transform.position.x + 1f)
            {
                actualState = PlayerStates.STOP;
                doorWithoutKey.CloseDoor();
            }
            else if(doorDirection == -1 && tr.transform.position.x < doorTr.transform.position.x - 1f)
            {
                actualState = PlayerStates.STOP;
                doorWithoutKey.CloseDoor();
            }
            return;

        }
        else if(actualState == PlayerStates.IDLE)
        {
            animator.SetBool("Death", false);
            animator.SetBool("StartPose", false);
        }

        animator.SetFloat("FallVelocity", rb.velocity.y);
        animator.SetBool("Grounded", ground.OnGround() || playerStairs.GetOnStairs());
        animator.SetBool("Stairs", playerStairs.GetOnStairs() && controller.GetMovement().y != 0);
        animator.SetBool("Dash", playerDash.GetIsDashing());
        animator.SetFloat("DashVelocity", rb.velocity.x);

        animator.SetBool("Walk", controller.GetMovement().x != 0);

        playerWalk.Walk();

        playerDash.DashCheck();
        
        if (playerStairs.GetOnStairs())
        {
            if (playerStairs.GetUsingStairs())
            {
                playerStairs.Stairs();
                rb.gravityScale = 0f;
            }
            else
            {
                playerStairs.SetUsingStairs(controller.GetMovement().y > 0 || rb.velocity.y <= 0.0f);
            }

        }
        else if (controller.GetPowerUpKey())
        {
            playerDash.Dash();
        }
        else if (!playerDash.GetIsDashing())
        {
            rb.gravityScale = 9.81f;
        }

        playerJump.CheckJump();
    }

    public void SetActualState(PlayerStates state)
    {
        actualState = state;
    }

    public PlayerStates GetActualState()
    {
        return actualState;
    }

    public void Death()
    {

        var main = deathParticles1.main;
        main.startColor = powerUpManager.GetColorRGB();

        var trail = deathParticles1.trails;
        trail.colorOverTrail = powerUpManager.GetColorRGB();
        trail.colorOverLifetime = powerUpManager.GetColorRGB();

        main = deathParticles2.main;
        main.startColor = powerUpManager.GetColorRGB();


        deathParticles1.Play();
        deathParticles2.Play();
        childrenSprite.enabled = false;
            

        rb.gravityScale = 0f;
        rb.velocity = Vector2.zero;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Door")
        {
            doorWithoutKey = collision.gameObject.GetComponent<LvlTransitionWithoutKey>();
            doorTr = collision.gameObject.transform;

            if (doorTr.position.x > tr.position.x)
            {
                doorDirection = 1;
                doorWithoutKey.ShowBlackSquare(0.95f);
            }
            else
            {
                doorDirection = -1;
                doorWithoutKey.ShowBlackSquare(-0.95f);
            }

            actualState = PlayerStates.DOOR;
        }
    }
}
