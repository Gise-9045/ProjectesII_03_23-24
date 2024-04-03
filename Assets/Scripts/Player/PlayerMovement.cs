using Cinemachine.Utility;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
//using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class PlayerMovement : MonoBehaviour
{
    private Player player;

    private Vector2 stairsPos;

    private Rigidbody2D rb;
    private Transform tr;
    private GameObject carriedBox;
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
    private bool isPicked = false;
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
    [SerializeField] private float walkSoundDelay = 1.0f;
    [SerializeField] private float ClimbSoundDelay = 1.0f;
    private bool isPlayingSound = false;
    private Coroutine soundCoroutine;
    private bool isPlayingJumpSound = false;

    private float storedMass;

    public InputController controller;


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

        ground.OnGroundTouchdown += jumpParticles.Play;
        ground.OnGroundTouchdown += walkParticles.Play;

        ground.OnLeaveGround += walkParticles.Stop;

        oldDead = false;
    }
    


    void FixedUpdate()
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
        animator.SetBool("Grounded", ground.OnGround() || onStairs);
        animator.SetBool("Stairs", onStairs && controller.GetMovement().y != 0);
        animator.SetBool("Dash", dashing);
        animator.SetFloat("DashVelocity", rb.velocity.x);

        animator.SetBool("Walk", controller.GetMovement().x != 0);

        Walk();
        DashCheck();
        
        #region Movement
        if (onStairs)
        {
            if (usingStairs)
            {
                Stairs();
                rb.gravityScale = 0f;
            }
            else
            {
                usingStairs = controller.GetMovement().y > 0 || rb.velocity.y <= 0.0f;
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
      
        CheckJump();
#endregion

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


        if (controller.GetMovement().x < 0 || controller.GetMovement().x > 0)
        {
            slide = 0.1f;
            player.SetDirection(new Vector2(controller.GetMovement().x, player.GetDirection().y));

            rb.velocity = new Vector2(player.GetDirection().x * player.GetSpeed(), rb.velocity.y);

            if (!isPlayingSound && ground.OnGround())
            {
                soundCoroutine = StartCoroutine(PlaySoundRepeatedly());
            }

            isWalking = true;
        }
        else
        {
            rb.velocity = new Vector2(player.GetDirection().x * (player.GetSpeed() * slide), rb.velocity.y);
        }

        if(controller.GetMovement().x < 0 || controller.GetMovement().x > 0)
        {
            if (soundCoroutine != null)
            {
                StopCoroutine(soundCoroutine);
            }
            isPlayingSound = false;

            isWalking = false;
        }


    }

    IEnumerator PlaySoundRepeatedly()
    {
        isPlayingSound = true;

        while (true)
        {
            if(ground.OnGround())
                audioManager.PlaySFX(audioManager.walk);
            yield return new WaitForSeconds(walkSoundDelay);
        }
    }

    IEnumerator PlaySoundRepeatedlyStairs()
    {
        isPlayingSound = true;

        while (true)
        {
            if (ground.OnGround())
                audioManager.PlaySFX(audioManager.stairsClimb);
            yield return new WaitForSeconds(walkSoundDelay);
        }
    }

    void Jump()
    {
        isJumping = true;
        actualJumpTimeCounter = jumpTimeCounter;
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        jumpParticles.Play();

        audioManager.PlaySFX(audioManager.jump);
    }

    void CheckJump()
    {
        //Debug.Log(controller.GetJumpKeyTap());

        if (ground.OnGround())
        {
            actualCoyoteTime = coyoteTime;
            doubleJump = 0;
        }
        else
        {
            actualCoyoteTime -= Time.deltaTime;
        }


        if (actualCoyoteTime > 0 && controller.GetJumpKeyTap())
        {
            Jump();
            actualCoyoteTime = 0f;
            oldJump = true;
        }
        else if (canDoubleJump && doubleJump < 1 && controller.GetJumpKeyTap() && !ground.OnGround())
        {
            Jump();
            doubleJump++; // Incrementa el contador de saltos después de un doble salto
            oldJump = true;
        }

        if (controller.GetJumpkeyHold() && isJumping)
        {
            oldJump = true;

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


        if (!controller.GetJumpkeyHold() && oldJump)
        {
            oldJump = false;
            isJumping = false;
            actualCoyoteTime = 0f;
            rb.gravityScale = 9.81f;
        }

        isPlayingJumpSound = true;
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
    public void SetPickUp(bool condition)
    {
        canPickUp = condition;
    }

    void Stairs()
    {
        if((controller.GetMovement().y < 0  || controller.GetMovement().y > 0) && controller.GetMovement().x == 0)
        {
            tr.position = new Vector2((float)(tr.position.x + 0.05 * (stairsPos.x - tr.position.x)), tr.position.y);
        }

        if (controller.GetMovement().y > 0)
        {
            rb.gravityScale = 0f;
            rb.velocity = new Vector2(rb.velocity.x, 5);
            if (!isPlayingSound && ground.OnGround())
            {
                soundCoroutine = StartCoroutine(PlaySoundRepeatedlyStairs());
            }
        }
        else if(controller.GetMovement().y < 0)
        {
            rb.gravityScale = 0f;
            rb.velocity = new Vector2(rb.velocity.x, -5);
            if (!isPlayingSound && ground.OnGround())
            {
                soundCoroutine = StartCoroutine(PlaySoundRepeatedlyStairs());
            }

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
            audioManager.StopSFX(audioManager.stairsClimb);
            StopCoroutine(PlaySoundRepeatedlyStairs());
            onStairs = false;
            usingStairs = false;
        }
    }
   
    private void Dash()
    {
        actualDashTimer = dashTimer;
        rb.gravityScale = 0f;
        audioManager.PlaySFX(audioManager.dash);
        dashTrail.Play();

    }
   

}
