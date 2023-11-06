using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{

    [SerializeField] private Rigidbody2D physics;
    [SerializeField] private float jumpForce;

    public void Jump_player()
    {
        physics.velocity = new Vector2(physics.velocity.x, 0);
        physics.velocity += Vector2.up * jumpForce; 
    }

}
