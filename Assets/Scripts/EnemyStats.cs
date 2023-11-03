using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Enemy : MonoBehaviour
{
    public EnemyAttack enemyAttack;

    public int health = 3;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float speed;
    private int direction = 1;

    [SerializeField] public Transform pointA;
    [SerializeField] public Transform pointB;
    [SerializeField] SpriteRenderer sr;

    [SerializeField] private string detectionTag = "Player";


    //Posición actual del Enemigo
    private Transform currentPoint;

    private void Start()
    {
        currentPoint = GetComponent<Transform>();
    }

    private void Update()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
    private void FixedUpdate()
    {
        if (currentPoint.position.x < pointA.position.x)
        {
            direction = 1;

        }
        else if(currentPoint.position.x > pointB.position.x)
        {
            direction = -1;

        }

        Move();
        Flip();

    }

    void Flip()
    {
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


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(detectionTag))
        {
            enemyAttack.StartAttacking();
        }
    }



    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(pointA.transform.position, 0.5f);
        Gizmos.DrawWireSphere(pointB.transform.position, 0.5f);

        Gizmos.DrawLine(pointA.transform.position, pointB.transform.position);
    }
}
