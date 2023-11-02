    using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerJump : MonoBehaviour
{
    [SerializeField] private InputActionReference jumps;
    [SerializeField] private float jumpPower;

    public Transform groundCheck;
    public LayerMask groundLayer;

    private bool isGrounded;
    private float isJumping; 
    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(isGrounded);
        isGrounded = Physics2D.OverlapCapsule(groundCheck.position, new Vector2(1.7f, -3.75f), CapsuleDirection2D.Horizontal, 0, groundLayer);

        //Debug.Log(isJumping);
        isJumping = jumps.action.ReadValue<float>();
       
    }
    private void FixedUpdate()
    {
        
        if ( isJumping == 1 && isGrounded == false)
        {
            //Debug.Log("salto");
            rb.velocity = new Vector2(rb.velocity.x, jumpPower);
        }
    }
}