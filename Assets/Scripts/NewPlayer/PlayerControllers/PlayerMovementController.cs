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

    public Vector2 currentScale;
    private int sidePlayer;
    [SerializeField] private GameObject model;
    [SerializeField] private GameObject particleAttackModel;
    bool facingRight;

    //[SerializeField] private PlayerStats playerStats;

    private void Awake()
    {
        physics = GetComponent<Rigidbody2D>();
        controller = GetComponent<PlayerController>();
    }

    private void FixedUpdate() 
    {
       

        if (currentSpeed < 0)
        {
            controller.ChangeState(PlayerController.PlayerStates.NONE);
        }

        //else   
        //controller.ChangeState(PlayerController.PlayerStates.MOVING);

        if (_direction.magnitude > 0 && currentSpeed >= 0)
        {
            oldPosition = _direction;
            currentSpeed += acceleraiton * maxSpeed * Time.deltaTime;
        }
        else
        {
            currentSpeed -= deacceleraiton * maxSpeed * Time.deltaTime;
        }

        Walk();

        if (_direction.x > 0 && facingRight)
        {
            sidePlayer = 1;
            Flip();
        }
        if (_direction.x < 0 && !facingRight)
        {
            sidePlayer = -1;
            Flip();
        }

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
        _direction = direction;

       
    }

    private void Flip()
    {
        currentScale = model.transform.localScale;
        currentScale.x *= -1;

        model.transform.localScale = currentScale;
        particleAttackModel.transform.localScale = currentScale;

        facingRight = !facingRight;
    }
}
