using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementController : MonoBehaviour
{
    private Vector2 _direction;
    private Vector2 oldPosition = Vector2.zero; 

    private Rigidbody2D physics;

    private PlayerController controller;

    [Space]
    [Header("Velocity")]

    [SerializeField] public float currentSpeed;
    [SerializeField] private float acceleraiton; 
    [SerializeField] private float deacceleraiton; 
    [SerializeField] public float maxSpeed;

    //[SerializeField] private PlayerStats playerStats;

    private void Awake()
    {
        physics = GetComponent<Rigidbody2D>();
        controller = GetComponent<PlayerController>();
    }

    private void Update() 
    {
        Walk();
       
    }

    private void Walk() 
    {
        //if(playerStats.knockback)
        //{
        //    physics.velocity = new Vector2(playerStats.knockbackVel, physics.velocity.y);
        //}
        //else
        //{

        currentSpeed = Mathf.Clamp(currentSpeed, 0, maxSpeed);
        physics.velocity = new Vector2(oldPosition.x * currentSpeed, physics.velocity.y);


    }


    public void SetDirection(Vector2 direction) // FALTA EL FLIP 
    {
        if (currentSpeed < 0)
        {
            controller.ChangeState(PlayerController.PlayerStates.NONE);
        }

        //else   
        //controller.ChangeState(PlayerController.PlayerStates.MOVING);

        if (direction.magnitude > 0 && currentSpeed >= 0) 
        {
            oldPosition = direction;
            currentSpeed += acceleraiton * maxSpeed * Time.fixedDeltaTime;
        }
        else
        {
            currentSpeed -= deacceleraiton * maxSpeed * Time.fixedDeltaTime;
        }


        //FUNKA 

        if(direction.normalized.x == 1)
        {
            Flip(direction.normalized.x);
        }
        else if(direction.normalized.x == -1) { Flip(direction.normalized.x); }
       
    }

    public Vector2 currentScale;
    [SerializeField] private GameObject model;
    bool facingRight; 
    private void Flip(float a)
    {
        currentScale = model.transform.localScale;
        currentScale.x *= a;

        model.transform.localScale = currentScale;

        facingRight = !facingRight;
    }
}
