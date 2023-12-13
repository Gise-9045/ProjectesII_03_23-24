using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerJumpController : MonoBehaviour
{
    [Header("Physics")]
    private Rigidbody2D _physics;

    [Header("Ground check")]
    private bool isOnGround;
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

    private void Awake()
    {
        _physics = GetComponent<Rigidbody2D>();
        onLeaveGround += BeforeTouchGround;  // metodo que ctive la animacion de saltar del suelo 
        onJump += WhileJumping; //Metodo que active la animacion de salto en el aire 
        onTouchGround += StartJump; // metodo que ctive la animacion de llegar al suelo 

    }

    private void FixedUpdate()
    {
        //UpdateGroundCheck();
        UpdateGravity();
    }


    public void Jump_player(int jumpCount , int maxJump)
    {
        if (Physics2D.OverlapCapsule(_physics.position, groundCheck.localScale, CapsuleDirection2D.Horizontal, 0, waterLayer))
        {
            ControlOnWater(); 
        }
        else
        {
            if (jumpCount >= maxJump) return;
            _physics.velocity = new Vector2(_physics.velocity.x, 0);
            _physics.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
        onJump?.Invoke();
    }

    private void ControlOnWater()
    {
        _physics.velocity = new Vector2(_physics.velocity.x, 0);
        _physics.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    public void UpdateGroundCheck()
    {
        isOnGround = Physics2D.OverlapCapsule(_physics.position, groundCheck.localScale, CapsuleDirection2D.Horizontal, 0, groundLayer);

        if (!isOnGround && !lastIsOnGround)
        {
            onLeaveGround?.Invoke();
        }
        else if (isOnGround && lastIsOnGround)
        {
            onTouchGround?.Invoke();
        }

        lastIsOnGround = isOnGround;

        Debug.Log(isOnGround);
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
}
