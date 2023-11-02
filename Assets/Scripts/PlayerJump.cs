    using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerJump : MonoBehaviour
{
    [SerializeField] private InputActionReference jumps;
    [SerializeField] private float jumpPower;

    public Transform groundCheck;
    public LayerMask groundLayer;
    private Rigidbody2D rb;

    [SerializeField] int maxJumps = 2;
    [SerializeField] int jumpsRemeaning; 



    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        GroundCheck(); 
    }

    #region collision
    private bool GroundCheck()
    {
        if (Physics2D.OverlapCapsule(groundCheck.position, groundCheck.localScale, CapsuleDirection2D.Horizontal, 0, groundLayer))
        {
            jumpsRemeaning = maxJumps;
            return true;

        }
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
                jumpsRemeaning--;
            }
            else if (context.canceled)
            {
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.1f);
                jumpsRemeaning--;
            }
        }


    }
    #endregion jump



}