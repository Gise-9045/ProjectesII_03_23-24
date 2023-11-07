using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{

    private Rigidbody2D _physics;

    [SerializeField] private float jumpForce ;

    [Space]
    [Header("Jump Variables")]
    [SerializeField] private float massScale = 10f;
    [SerializeField] private float fallingMassScale = 10f;

    float oldVelocity;

    private void Update()
    {
        _physics = GetComponent<Rigidbody2D>();
    }

    public void Jump_player()
    {
        _physics.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

        if (_physics.velocity.y >= 0f )
        {
            _physics.gravityScale = massScale;
            Debug.Log(_physics.gravityScale);
            Debug.Log(_physics.velocity.y); 
        }
        if(oldVelocity <= _physics.velocity.y)
        {
            _physics.gravityScale = fallingMassScale;
            Debug.Log(_physics.gravityScale);
        }
        oldVelocity = _physics.velocity.y; 
         
    }
}
