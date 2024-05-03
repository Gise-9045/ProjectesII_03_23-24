using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    private Rigidbody2D rb;
    private InputController controller;

    private bool isJumping;
    private float coyoteTime;
    private float actualCoyoteTime;

    [SerializeField] private float jumpForce;
    [SerializeField] private float jumpTimeCounter;
    private float actualJumpTimeCounter;
    [SerializeField] private bool fallToGroundSound;
    private int doubleJump = 0;
    [SerializeField] private bool canDoubleJump = false;
    private bool oldJump = false;

    private PlayerGroundDetection ground;

    [SerializeField] private ParticleSystem jumpParticles;

    private AudioManager audioManager;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        controller = GetComponent<InputController>();
        ground = GetComponentInChildren<PlayerGroundDetection>();
        ground.OnGroundTouchdown += jumpParticles.Play;
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        coyoteTime = 0.3f;
        isJumping = false;
        
    }

    void Update()
    {
        
    }

    void Jump()
    {
        isJumping = true;
        actualJumpTimeCounter = jumpTimeCounter;
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        jumpParticles.Play();

        audioManager.PlaySFX(audioManager.jump);
    }

    public void CheckJump()
    {
        if (ground.OnGround())
        {
            actualCoyoteTime = coyoteTime;
            doubleJump = 0;
            if(!fallToGroundSound)
            {
                fallToGroundSound = true;

                audioManager.PlaySFX(audioManager.fallToGround);
            }
        }
        else
        {
            actualCoyoteTime -= Time.deltaTime;
            fallToGroundSound = false;
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

        //isPlayingJumpSound = true;
    }

    //CUANDO SE CAMBIE EL COMO FUNCIONAN LOS POWERUPS, ESTO TIENE QUE DESAPARECER
    public void SetDoubleJump(bool condition)
    {
        canDoubleJump = condition;
    }
}
