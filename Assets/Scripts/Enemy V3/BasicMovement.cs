using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMovement : MonoBehaviour
{
    [SerializeField] private Enemy enemy;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    private void FixedUpdate()
    {
        rb.velocity = new Vector2(enemy.GetDirection() * enemy.GetSpeed(), rb.velocity.y);
    }
}
