using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Move : MonoBehaviour
{
    [SerializeField] private Vector2 positionPlayer;

    private Vector2 oldPosition = Vector2.zero; 

    [SerializeField] private Rigidbody2D physics;
    [SerializeField] private InputActionReference move;

    [Space]
    [Header("Velocity")]

    [SerializeField] private float currentSpeed;
    [SerializeField] private float acceleraiton; 
    [SerializeField] private float deacceleraiton; 
    [SerializeField] private float maxSpeed;

    public void Walk(InputActionReference movePlayer) //solo se hace UNA VEZ!!
    {
        Debug.Log("dentro de la funcion");
        Debug.Log(positionPlayer.magnitude); 
        Debug.Log(move.action.ReadValue<Vector2>());

        if(move.action.ReadValue<Vector2>().magnitude > 0 && currentSpeed >= 0)
        {
            oldPosition = move.action.ReadValue<Vector2>();
            currentSpeed += acceleraiton * maxSpeed * Time.fixedDeltaTime;
        }
        else
        {
            currentSpeed -= deacceleraiton * maxSpeed * Time.fixedDeltaTime;
        }

        currentSpeed = Mathf.Clamp(currentSpeed, 0, maxSpeed); 
        physics.velocity = new Vector2(oldPosition.x * currentSpeed, physics.velocity.y);
    }
}
