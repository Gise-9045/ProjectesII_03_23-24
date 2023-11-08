using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{

    private Rigidbody2D _physics;
    private Collisions _collisions;

    [SerializeField] private float jumpForce ;

    [Space]
    [Header("Jump Variables")]
    [SerializeField] private float gravityScale;
    [SerializeField] private float fallingGravityScale;
    [SerializeField] private float airResistance = 5.0f; // Ajusta este valor según sea necesario

    float oldVelocity;

    private void Update()
    {
        _physics = GetComponent<Rigidbody2D>();
        _collisions = GetComponent<Collisions>();   
    }

    public void Jump_player(int jumpCount, int maxJump)
    {
        if (jumpCount != maxJump)
        {
            _physics.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            jumpCount++;
        }

        // Aplicar resistencia al aire
        if (!_collisions.onGround)
        {
            Vector2 resistanceForce = -_physics.velocity.normalized * airResistance;
            _physics.AddForce(resistanceForce);
        }

        if (_physics.velocity.y >= 0f)
        {
            _physics.mass = gravityScale;
        }
        else
        {
            _physics.mass = fallingGravityScale;
        }
        oldVelocity = _physics.velocity.y;

        Debug.Log(jumpCount);
    }
}
