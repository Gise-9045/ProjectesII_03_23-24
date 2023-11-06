using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{

    private Rigidbody2D _physics;

    [SerializeField] private float jumpForce;

    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;


    private void Update()
    {
        _physics = GetComponent<Rigidbody2D>();
    }

    public void Jump_player()
    {

        _physics.AddForce((Vector2.up) * Physics2D.gravity.y * (fallMultiplier + 1) * Time.deltaTime); 

        if(_physics.velocity.y > 0 )
        {
            _physics.AddForce((Vector2.up) * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime);
        }
    }
}
