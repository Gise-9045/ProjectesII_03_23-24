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
    public enum PlayerStates { HANDUP, STOP, JUMP, WALK, DASH, STAIRS}






    private Player player;

    private Rigidbody2D rb;
    private SpriteRenderer childrenSprite;


    private PlayerGroundDetection ground;

    public bool isJumping;
    public bool isWalking;

    [SerializeField] public bool canPickUp = false;

    [Header("----- Particles -----")]
    [SerializeField] private ParticleSystem deathParticles1;
    [SerializeField] private ParticleSystem deathParticles2;
    [SerializeField] private ParticleSystem walkParticles;

    private Animator animator;

    private bool oldDead;



    private InputController controller;



    [SerializeField] private PlayerJump playerJump;
    [SerializeField] private PlayerWalk playerWalk;
    [SerializeField] private PlayerStairs playerStairs;
    [SerializeField] private PlayerDash playerDash;

    PlayerStates actualState;

    private PlayerPowerUpManager powerUpManager;

    void Start()
    {
        player = GetComponent<Player>();
        controller = GetComponent<InputController>();
        rb = GetComponent<Rigidbody2D>();
        ground = GetComponentInChildren<PlayerGroundDetection>();
        isJumping = false;
        animator = GetComponent<Animator>();

        actualState = PlayerStates.STOP;

        childrenSprite = GetComponentInChildren<SpriteRenderer>();

        //ground.OnGroundTouchdown += jumpParticles.Play;
        ground.OnGroundTouchdown += walkParticles.Play;

        ground.OnLeaveGround += walkParticles.Stop;

        oldDead = false;
        powerUpManager = GetComponent<PlayerPowerUpManager>();
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


            //animator.SetBool("Death", true);

            if(player.GetDead() && player.GetDead() != oldDead)
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
}
