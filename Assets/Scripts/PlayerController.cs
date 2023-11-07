using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
//using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class PlayerController : MonoBehaviour
{
    public bool isMoving; 

    [SerializeField] private InputActionReference move;

    public Vector2 movementInput;
    private Rigidbody2D rb;
    private Vector2 oldMovementInput = Vector2.zero;

    public float currentSpeed;
    [SerializeField] private float acceleration;
    [SerializeField] private float deacceleration;
    [SerializeField] private float maxSpeed;

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        movementInput = move.action.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        if (movementInput.magnitude > 0 && currentSpeed >= 0)
        {
            oldMovementInput = movementInput;
            currentSpeed += acceleration * maxSpeed * Time.fixedDeltaTime;
            isMoving = true;
        }
        else
        {
            currentSpeed -= deacceleration * maxSpeed * Time.fixedDeltaTime;
            isMoving = true;
        }

        if(currentSpeed <= 0 )
        {
            isMoving = false; 
        }
        currentSpeed = Mathf.Clamp(currentSpeed, 0, maxSpeed);
        rb.velocity = new Vector2(oldMovementInput.x * currentSpeed,rb.velocity.y);
    }
}
