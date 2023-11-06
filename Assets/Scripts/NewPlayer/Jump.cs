using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    public void Jump_player(Vector2 dir, Rigidbody2D physics, float jumpForce)
    {
        physics.velocity = new Vector2(physics.velocity.x, 0);
        physics.velocity += dir * jumpForce; 

    }

}
