using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementController : MonoBehaviour
{
    private Vector2 _direction;
    private Vector2 oldPosition = Vector2.zero; 

    [SerializeField] private Rigidbody2D physics;
    [SerializeField] private Animator anim; 

    [Space]
    [Header("Velocity")]

    [SerializeField] public float currentSpeed;
    [SerializeField] private float acceleraiton; 
    [SerializeField] private float deacceleraiton; 
    [SerializeField] public float maxSpeed;

    [SerializeField] private PlayerStats playerStats;

    private void Update() 
    {
        Walk();
    }

    private void Walk() 
    {
        if(playerStats.knockback)
        {
            physics.velocity = new Vector2(playerStats.knockbackVel, physics.velocity.y);
        }
        else
        {
            if (_direction.magnitude > 0 && currentSpeed >= 0)
            {
                anim.SetBool("isMoving", true);
                oldPosition = _direction;
                currentSpeed += acceleraiton * maxSpeed * Time.fixedDeltaTime;
            }
            else
            {
                anim.SetBool("isMoving", false);
                currentSpeed -= deacceleraiton * maxSpeed * Time.fixedDeltaTime;
            }

            currentSpeed = Mathf.Clamp(currentSpeed, 0, maxSpeed);
            physics.velocity = new Vector2(oldPosition.x * currentSpeed, physics.velocity.y);
        }
    }


    public void SetDirection(Vector2 direction)
    {
        _direction = direction;
    }

    
}
