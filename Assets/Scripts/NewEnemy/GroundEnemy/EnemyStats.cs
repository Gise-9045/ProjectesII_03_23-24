using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GroundEnemy : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;


    private Transform currentPoint;

    //VARIABLES
    [SerializeField] private float speed;

    public int direction = 1;
    public int health = 3;


    private void Start()
    {
        currentPoint = GetComponent<Transform>();
    }

    private void FixedUpdate()
    {
        Move();

        //HEALTH
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void Flip()
    {
        direction = -1 * direction;

        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x = direction;
        gameObject.transform.localScale = currentScale;
    }

    private void Move()
    {
        rb.velocity = new Vector2(direction * speed, rb.velocity.y);
    }

    public void TakeDamage(int damage, Vector2 knockback)
    {
        health -= damage;
        rb.AddForce(knockback, ForceMode2D.Impulse);
    }



}
