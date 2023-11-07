using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{

    private Rigidbody2D _physics;

    [SerializeField] private float jumpForce;

    private void Update()
    {
        _physics = GetComponent<Rigidbody2D>();
    }

    public void Jump_player()
    {
        _physics.velocity = new Vector2(_physics.velocity.x, jumpForce); 
    }
}
