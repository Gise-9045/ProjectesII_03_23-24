using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem; 

public class PlayerController : MonoBehaviour
{

    [SerializeField] private InputActionReference move;

    private Vector2 movementInput;
    private Rigidbody2D rb;
    private Vector2 oldMovementInput; 

    [SerializeField] private float currenSpeed; 
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
        Debug.Log(move.action.ReadValue<Vector2>());
        movementInput = move.action.ReadValue<Vector2>();  
    }
    private void FixedUpdate()
    {
        if(movementInput.magnitude > 0 && currenSpeed >= 0)
        {
            oldMovementInput = movementInput;
            currenSpeed += acceleration * maxSpeed * Time.deltaTime; 
        }
        else
        {
            currenSpeed -= deacceleration * maxSpeed * Time.deltaTime;
        }
        currenSpeed = Mathf.Clamp(currenSpeed, 0, maxSpeed);
        rb.velocity = oldMovementInput * currenSpeed; 
    }
}
