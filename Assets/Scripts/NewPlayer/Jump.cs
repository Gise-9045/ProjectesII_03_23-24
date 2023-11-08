using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Jump : MonoBehaviour
{

    private Rigidbody2D _physics;
    private Collisions _collisions;

    [SerializeField] private float jumpForce ;

    [Space]
    [Header("Jump Variables")]
    public Transform groundCheck;
    public LayerMask waterLayer;
    public bool inWater;
    public Action isJumping;
    [SerializeField] private float massScale = 10f;
    [SerializeField] private float fallingMassScale = 10f;

    float oldVelocity;

    private void Update()
    {
        _physics = GetComponent<Rigidbody2D>();
       
    }
    
    public void Jump_player(int jumpCount , int maxJump)
    {
        if (Physics2D.OverlapCapsule(_physics.position, groundCheck.localScale, CapsuleDirection2D.Horizontal, 0, waterLayer))
        {
            inWater = true;
        }
        else
        {
            inWater = false;
        }
        if (jumpCount < maxJump && inWater == false)
        {
            isJumping();
            Debug.Log(jumpCount + " | " + maxJump);
            _physics.velocity = new Vector2(_physics.velocity.x, 0);
            _physics.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        } 
        if(inWater == true)
        {
            _physics.velocity = new Vector2(_physics.velocity.x, 0);
            _physics.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
        
       
        if (_physics.velocity.y >= 0f )
        {
            Vector2 resistanceForce = -_physics.velocity.normalized * airResistance;
            _physics.AddForce(resistanceForce);
        }

        if (_physics.velocity.y >= 0f)
        {
            _physics.mass = gravityScale;
        }
        else
        {
            _physics.mass = fallingGravityScale;
        }
        oldVelocity = _physics.velocity.y;

        
    }
}
