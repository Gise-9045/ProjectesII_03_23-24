using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Jump : MonoBehaviour
{

    private Rigidbody2D _physics;

    [SerializeField] private float jumpForce ;

    [Space]
    [Header("Jump Variables")]
    public bool inWater;
    public Action isJumping;
    [SerializeField] private float massScale = 10f;
    [SerializeField] private float fallingMassScale = 10f;

    float oldVelocity;

    private void Update()
    {
        _physics = GetComponent<Rigidbody2D>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("water"))
        {
            inWater = true;
        }
        else {
            inWater = false;
        }
    }
    public void Jump_player(int jumpCount , int maxJump)
    {
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
            _physics.mass = massScale;

        }
        if (oldVelocity <= _physics.velocity.y )
        {
            _physics.mass = fallingMassScale;
        }
        oldVelocity = _physics.velocity.y;

        
    }
}
