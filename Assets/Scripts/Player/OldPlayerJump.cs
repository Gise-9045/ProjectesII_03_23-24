using System;
    using UnityEngine;
using UnityEngine.InputSystem;

public class OldPlayerJump : MonoBehaviour
{
    [SerializeField] private InputActionReference jumps;
    [SerializeField] private float jumpPower;

    public Transform groundCheck;
    public LayerMask groundLayer;
    public LayerMask waterLayer;
    private Rigidbody2D rb;
    public bool isJumping;
    public bool inWater;
    [SerializeField] int maxJumps = 2;
    [SerializeField] int jumpsRemeaning;

    public Action onJump;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        inWater = false;
        GroundCheck(); 
    }

    #region collision
    public bool GroundCheck()
    {
       
        if (Physics2D.OverlapCapsule(groundCheck.position, groundCheck.localScale, CapsuleDirection2D.Horizontal, 0, groundLayer))
        {
            isJumping = false;
            jumpsRemeaning = maxJumps;
            
            return true;

        }
        else if(Physics2D.OverlapCapsule(groundCheck.position, groundCheck.localScale, CapsuleDirection2D.Horizontal, 0, waterLayer))
        {
            inWater = true;
            return true;
        }
        isJumping = true;
        return false;
    }

    #endregion collision

    #region jump
    public void Jump(InputAction.CallbackContext context)
    {
        if (jumpsRemeaning > 0)
        {
            if (context.performed)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpPower);
                if(onJump != null)
                    onJump();
                jumpsRemeaning--;
            }
            else if (context.canceled)
            {
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.3f);
                jumpsRemeaning--;
            }
        }
        else if (inWater)
        {
           
            if (context.performed)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpPower*1f);
                jumpsRemeaning=0;
                GroundCheck();
            }
            else if (context.canceled)
            {
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 1f);
                jumpsRemeaning=0;
                GroundCheck();
            }
            
        }
        

    }
    #endregion jump



}