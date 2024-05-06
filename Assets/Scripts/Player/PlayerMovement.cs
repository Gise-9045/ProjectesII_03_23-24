using Cinemachine.Utility;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
//using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class PlayerMovement : MonoBehaviour
{
    //SE UTILIZARÁ PROXIMAMENTE
    public enum PlayerStates { WAIT, JUMP, WALK, DASH, STAIRS}






    private Player player;

    private Vector2 stairsPos;

    private Rigidbody2D rb;
    private Transform tr;
    [SerializeField] private float jumpForce;
    [SerializeField] private float jumpTimeCounter;
    private float actualJumpTimeCounter;


    private PlayerGroundDetection ground;

    public bool isJumping;
    public bool isWalking;

    private bool oldJump = false;

    private float coyoteTime;
    private float actualCoyoteTime;
    private int doubleJump = 0;
    [SerializeField] private bool canDoubleJump = false;
    [SerializeField] public bool canPickUp = false;
    private float slide;

    bool onStairs;
    bool usingStairs = false;

    bool dashing = false;
    [SerializeField] float dashVelocity;
    float actualDashTimer;
    [SerializeField] float dashTimer;
    [SerializeField] bool canDash;
    [SerializeField] float dashCooldown;

    private AudioManager audioManager;


    [Header("----- Particles -----")]
    [SerializeField] private ParticleSystem dashTrail;
    [SerializeField] private ParticleSystem deathParticles;
    [SerializeField] private ParticleSystem walkParticles;
    [SerializeField] private ParticleSystem jumpParticles;

    float actualDashCooldown = 0;

    private Animator animator;

    private bool oldDead;

    [Header("----- Sound -----")]
    [SerializeField] private float walkSoundDelay;
    private float actualWalkSoundDelay;

    [SerializeField] private bool fallToGroundSound;

    [SerializeField] private float climbSoundDelay;
    private float actualClimbSoundDelay;

    private bool isPlayingSound = false;
    private bool isPlayingJumpSound = false;
    private Coroutine soundCoroutine;

    private float storedMass;

    private InputController controller;






    [SerializeField] private PlayerJump playerJump;
    [SerializeField] private PlayerWalk playerWalk;
    [SerializeField] private PlayerStairs playerStairs;


    void Start()
    {
        player = GetComponent<Player>();
        controller = GetComponent<InputController>();
        rb = GetComponent<Rigidbody2D>();
        ground = GetComponentInChildren<PlayerGroundDetection>();
        tr = GetComponentInChildren<Transform>();
        isJumping = false;
        onStairs = false;
        coyoteTime = 0.3f;
        animator = GetComponent<Animator>();

        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();

        //ground.OnGroundTouchdown += jumpParticles.Play;
        ground.OnGroundTouchdown += walkParticles.Play;

        ground.OnLeaveGround += walkParticles.Stop;

        oldDead = false;
    }
    


    void Update()
    {
        //NOTA PARA ADRI POR ADRI. ESTO ES UNA GUARRERÍA ARREGLALO CUANTO ANTES
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
        else if(player.GetStop())
        {
            animator.SetBool("Stairs", false);
            animator.SetBool("Dash", false);
            animator.SetBool("Walk", false);
            animator.SetBool("Grounded", true);

            animator.SetBool("StartPose", true);

            return;
        }
        else
        {
            animator.SetBool("Death", false);
            animator.SetBool("StartPose", false);
            oldDead = player.GetDead();
        }

        animator.SetFloat("FallVelocity", rb.velocity.y);
        animator.SetBool("Grounded", ground.OnGround() || playerStairs.GetOnStairs());
        animator.SetBool("Stairs", playerStairs.GetOnStairs() && controller.GetMovement().y != 0);
        animator.SetBool("Dash", dashing);
        animator.SetFloat("DashVelocity", rb.velocity.x);

        animator.SetBool("Walk", controller.GetMovement().x != 0);

        playerWalk.Walk();
        //Walk();
        DashCheck();
        
        if (playerStairs.GetOnStairs())
        {
            if (playerStairs.GetUsingStairs())
            {
                playerStairs.Stairs();
                //Stairs();
                rb.gravityScale = 0f;
            }
            else
            {
                playerStairs.SetUsingStairs(controller.GetMovement().y > 0 || rb.velocity.y <= 0.0f);
            }

        }
        else if (canDash && !dashing && controller.GetPowerUpKey() && actualDashCooldown <= 0)
        {
            Dash();
        }
        else if (!dashing)
        {
            rb.gravityScale = 9.81f;
        }

        playerJump.CheckJump();

        //CheckJump();
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



    public void SetDash(bool condition)
    {
        canDash = condition;
    }
    public void SetPickUp(bool condition)
    {
        canPickUp = condition;
    }
   
    private void Dash()
    {
        actualDashTimer = dashTimer;
        rb.gravityScale = 0f;
        audioManager.PlaySFX(audioManager.dash);
        dashTrail.Play();
    }
}
