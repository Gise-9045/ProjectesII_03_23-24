using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMovement : MonoBehaviour
{
    [SerializeField] private Enemy enemy;
    private Rigidbody2D rb;
    private Transform tr;
    private Transform player;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        tr = GetComponent<Transform>();
        player = GameObject.FindWithTag("Player").GetComponent<Transform>();

    }


    private void FixedUpdate()
    {
        if(enemy.GetKnockback())
        {
            rb.velocity = new Vector2(-Mathf.Sign(player.position.x - tr.position.x) * enemy.GetKnockbackVel(), rb.velocity.y);

        }
        else
        {
            rb.velocity = new Vector2(enemy.GetDirection() * enemy.GetSpeed(), rb.velocity.y);

        }
    }


}
