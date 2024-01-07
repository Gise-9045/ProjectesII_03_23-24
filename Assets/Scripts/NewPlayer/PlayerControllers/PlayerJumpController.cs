using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class PlayerJumpController : MonoBehaviour
{
    [Header("Physics")]
    private Rigidbody2D _physics;

    [Header("Ground check")]
    public bool isOnGround;
    public bool lastIsOnGround;
    public bool canDoubleJump; 
    public bool isJumping; 
    public bool isSwimming;

    public float collisionRadius = 0.25f;
    public Vector2 bottomOffset;
    public LayerMask groundLayer;

    [Space]
    [Header("Jump Variables")]
    [SerializeField] private float jumpForce;
    public Transform groundCheck;
    public LayerMask waterLayer;
    public bool inWater;
   // public Action isJumping;
    [SerializeField] private float gravityOnFall;
    [SerializeField] private float gravityOnJump;

    public int jumpCount;
    public  int maxJump;
    [Header("Audio")]
    [SerializeField, Range(0f, 1f)] private float volumeAudio = 0.2f;
    [SerializeField] private AudioClip jumpClip;
    [SerializeField] private AudioClip swimClip;
    [SerializeField] private AudioSource jumpAudio;


    private PlayerGroundDetection groundDetection;

    private void Awake()
    {
        _physics = GetComponent<Rigidbody2D>();

        jumpCount = 0;
        maxJump = 1;
        jumpAudio = gameObject.AddComponent<AudioSource>(); // Add an AudioSource component
        jumpAudio.clip = jumpClip;

        groundDetection = GetComponentInChildren<PlayerGroundDetection>();
    }

    private void FixedUpdate()
    {
        jumpAudio.volume = volumeAudio;
        //UpdateGroundCheck();
        UpdateGravity();


        if(groundDetection.OnGround())
        {
            isOnGround = true;
        }
        else
        {
            isOnGround = false;
        }
    }


    public void Jump_player()
    {
        if (Physics2D.OverlapCapsule(_physics.position, groundCheck.localScale, CapsuleDirection2D.Horizontal, 0, waterLayer))
        {
            jumpAudio.clip = swimClip;
            ControlOnWater();
        }
        else if (isOnGround)
        {
            jumpAudio.clip = jumpClip;

            PlayJumpAudio();
            _physics.velocity = new Vector2(_physics.velocity.x, 0);
            _physics.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isOnGround = false;
        }
        else
            return;

        Debug.Log(isOnGround + "JUMP");
    }

    public void Jump_player_PowerUp()
    {
        if (Physics2D.OverlapCapsule(_physics.position, groundCheck.localScale, CapsuleDirection2D.Horizontal, 0, waterLayer))
        {
            jumpAudio.clip = swimClip;
            ControlOnWater();
        }
        if(isOnGround)
        {
            jumpAudio.clip = jumpClip;

            PlayJumpAudio();
            _physics.velocity = new Vector2(_physics.velocity.x, 0);
            _physics.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            canDoubleJump = true;
            isOnGround = false;
        }
        else if(canDoubleJump)
        {
            jumpAudio.clip = jumpClip;

            PlayJumpAudio();
            _physics.velocity = new Vector2(_physics.velocity.x, 0);
            _physics.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            canDoubleJump = false;
        }
        else
            return; 
    }

    private void ControlOnWater()
    {
        jumpAudio.Play();
        _physics.velocity = new Vector2(_physics.velocity.x, 0);
        _physics.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if(collision.CompareTag("Ground") || collision.CompareTag("Enemy"))
    //    {
    //        isOnGround = true; 
    //    }
    //    else 
    //        isOnGround = false;
    //}


    private void UpdateGravity()
    {
        if (_physics.velocity.y >= 0f)
        {
            _physics.gravityScale = gravityOnJump;
        }
        else
        {
            _physics.gravityScale = gravityOnFall;
        }
    }
   
    private void PlayJumpAudio()
    {
        if (jumpAudio != null)
        {
            jumpAudio.Play();
        }
    }
}
