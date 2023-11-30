using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Jump : MonoBehaviour
{
    [Header("Physics")]
    private Rigidbody2D _physics;

    [Header("Ground check")]
    public bool isOnGround;
    private bool lastIsOnGround;
    public Action onLeaveGround;
    public Action OnJump;
    public Action OnJumpApex;
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
       
    }

    private void Update()
    {
        UpdateGroundCheck();
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
        OnJump?.Invoke();
    }

    private void ControlOnWater()
    {
        _physics.velocity = new Vector2(_physics.velocity.x, 0);
        _physics.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    private void UpdateGroundCheck()
    {
        isOnGround = Physics2D.OverlapCircle((Vector2)transform.position + bottomOffset, collisionRadius, groundLayer);

        if (isOnGround && !lastIsOnGround)
            onTouchGround?.Invoke();
        else if (!isOnGround && lastIsOnGround)
            onLeaveGround?.Invoke();

        lastIsOnGround = isOnGround;
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
}
