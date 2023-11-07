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

    bool isFalling; 

    private void Update()
    {
        _physics = GetComponent<Rigidbody2D>();
    }

    public void Jump_player()
    {
        _physics.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

        if (_physics.velocity.y >= 0 && !isFalling)
        {
            _physics.mass = massScale;
            Debug.Log(_physics.mass);
        }
        if(oldVelocity <= _physics.velocity.y)
        {
            _physics.mass = fallingMassScale;
            Debug.Log(_physics.mass);
            isFalling = false; 
        }

        oldVelocity = _physics.velocity.y; 
         
    }
}
