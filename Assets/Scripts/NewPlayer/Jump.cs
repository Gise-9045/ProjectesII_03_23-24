using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Jump : MonoBehaviour
{

    private Rigidbody2D _physics;
 

    [SerializeField] private float jumpForce ;

    [Space]
    [Header("Jump Variables")]
    public Transform groundCheck;
    public LayerMask waterLayer;
    public bool inWater;
   // public Action isJumping;
    [SerializeField] private float gravityOnFall;
    [SerializeField] private float gravityOnJump;

    float oldVelocity;

    private void Awake()
    {
        _physics = GetComponent<Rigidbody2D>();
       
    }

    private void Update()
    {
        if (_physics.velocity.y >= 0f)
        {
            _physics.gravityScale = gravityOnJump;
        }
        else
        {
            _physics.gravityScale = gravityOnFall;
        }
        oldVelocity = _physics.velocity.y;
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
            
            //isJumping();
            Debug.Log(jumpCount + " | " + maxJump);
            _physics.velocity = new Vector2(_physics.velocity.x, 0);
            _physics.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            
        }
    }

    private void ControlOnWater()
    {
        _physics.velocity = new Vector2(_physics.velocity.x, 0);
        _physics.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }
}
