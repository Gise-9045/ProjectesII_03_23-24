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
    public Action onLeaveGround;
    public Action onJump;

    //public Action OnJumpApex;
    public Action onTouchGround;
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

    private void Awake()
    {
        _physics = GetComponent<Rigidbody2D>();
        onLeaveGround += BeforeTouchGround;  // metodo que ctive la animacion de saltar del suelo 
        onJump += WhileJumping; //Metodo que active la animacion de salto en el aire 
        onTouchGround += StartJump; // metodo que ctive la animacion de llegar al suelo 

        jumpCount = 0;
        maxJump = 1;
        jumpAudio = gameObject.AddComponent<AudioSource>(); // Add an AudioSource component
        jumpAudio.clip = jumpClip;
    }

    private void FixedUpdate()
    {
        jumpAudio.volume = volumeAudio;
        //UpdateGroundCheck();
        UpdateGravity();
    }


    public void Jump_player()
    {
        if (Physics2D.OverlapCapsule(_physics.position, groundCheck.localScale, CapsuleDirection2D.Horizontal, 0, waterLayer))
        {
            jumpAudio.clip = swimClip;
            ControlOnWater(); 
        }
        else
        {
            jumpAudio.clip = jumpClip;
            if (jumpCount > maxJump)
            { return; }
            PlayJumpAudio();
            _physics.velocity = new Vector2(_physics.velocity.x, 0);
            _physics.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

            jumpCount++; 
        }

        Debug.Log(jumpCount); 
    }

    private void ControlOnWater()
    {
        jumpAudio.Play();
        _physics.velocity = new Vector2(_physics.velocity.x, 0);
        _physics.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    public bool UpdateGroundCheck()
    {
        isOnGround = Physics2D.OverlapCapsule(_physics.position,groundCheck.localScale, CapsuleDirection2D.Horizontal, 0, groundLayer);

        return isOnGround; 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Ground"))
        {
            isOnGround = true; 
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            isOnGround = false;
        }
    }

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

    //NO SE PUEDEN PONER EN UN UPDATE

    private void StartJump()
    {
        Debug.Log("1");
        isOnGround = false;
        //onTouchGround?.Invoke();
        CancelInvoke("onTouchGround");
    }

    private void WhileJumping()
    {
        Debug.Log("2");
        //onJump?.Invoke();
        CancelInvoke("onJump"); 
    }

    private void BeforeTouchGround()
    {
        Debug.Log("3");
        isOnGround = true;
        // onLeaveGround?.Invoke();

        CancelInvoke("onLeaveGround"); 
    }
    private void PlayJumpAudio()
    {
        if (jumpAudio != null)
        {
            jumpAudio.Play();
        }
    }
}
