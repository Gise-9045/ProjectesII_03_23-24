using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Move : MonoBehaviour
{
    private Vector2 _direction;
    private Vector2 oldPosition = Vector2.zero; 

    [SerializeField] private Rigidbody2D physics;

    [Space]
    [Header("Velocity")]

    [SerializeField] private float currentSpeed;
    [SerializeField] private float acceleraiton; 
    [SerializeField] private float deacceleraiton; 
    [SerializeField] private float maxSpeed;

    private void Update() {
        Walk();
    }

    private void Walk() {
        Debug.Log("dentro de la funcion");
        Debug.Log(_direction);
        
        if(_direction.magnitude > 0 && currentSpeed >= 0)
        {
            oldPosition = _direction;
            currentSpeed += acceleraiton * maxSpeed * Time.fixedDeltaTime;
        }
        else
        {
            currentSpeed -= deacceleraiton * maxSpeed * Time.fixedDeltaTime;
        }
        
        currentSpeed = Mathf.Clamp(currentSpeed, 0, maxSpeed); 
        physics.velocity = new Vector2(oldPosition.x * currentSpeed, physics.velocity.y);
    }

    public void SetDirection(Vector2 direction)
    {
        _direction = direction;
    }

    
}
