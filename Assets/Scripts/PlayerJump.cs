using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerJump : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private InputActionReference jumps;
    [SerializeField] private float jumpPower;

    public Transform groundCheck;
    public LayerMask groundLayer;
    
    private bool isGrounded; 
    private bool isJumping;
    private Rigidbody2D rb; 

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();   
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics2D.OverlapCapsule(groundCheck.position, new Vector2(1.01f, 0.3f), CapsuleDirection2D.Horizontal, 0, groundLayer);

        Debug.Log(Input.GetButtonDown("Jump"));
        //isJumping = jumps.action.ReadValue<bool>(); 
    }

    private void FixedUpdate()
    {
        if (Input.GetButtonDown("Jump") == true && !isGrounded)
        {
            Debug.Log("salto");
            rb.velocity = new Vector2(rb.velocity.x, jumpPower);
        }
    }
}
