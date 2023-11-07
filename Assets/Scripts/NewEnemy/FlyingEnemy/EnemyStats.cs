using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FlyingEnemy : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;

    [SerializeField] private Transform pointA;
    [SerializeField] private Transform pointB;

    private Transform currentPoint;

    //VARIABLES
    [SerializeField] private float speed;
    [SerializeField] private float acceleration;

    float movement;
    int direction = 0;
    public int health = 3;


    private void Start()
    {
        currentPoint = GetComponent<Transform>();
    }



    private void FixedUpdate()
    {
        if (currentPoint.position.y + 2.0f >= Mathf.Round(pointA.position.y))
        {

            direction = 0;
        }
        else if (currentPoint.position.y - 2.0f <= Mathf.Round(pointB.position.y))
        {

            direction = 1;
        }



        if(direction == 0)
        {
            //movement = Mathf.Lerp(currentPoint.position.y, pointB.position.y, (((currentPoint.position.y - pointB.position.y) / speed) * Time.deltaTime) * acceleration);

            movement = Mathf.Lerp(currentPoint.position.y, pointB.position.y, speed);

        }
        else
        {
            //movement = Mathf.Lerp(currentPoint.position.y, pointA.position.y, (((pointA.position.y - currentPoint.position.y) / speed) * Time.deltaTime) * acceleration);

            movement = Mathf.Lerp(currentPoint.position.y, pointA.position.y, speed);

        }


        Move();
        //Flip();

        //HEALTH
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    void Flip()
    {
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x = direction;
        gameObject.transform.localScale = currentScale;

    }

    private void Move()
    {
        //rb.velocity = new Vector2(rb.velocity.x, movement);
        rb.position = new Vector2(rb.position.x, movement);
    }

    public void TakeDamage(int damage, Vector2 knockback)
    {
        health -= damage;
        rb.AddForce(knockback, ForceMode2D.Impulse);
    }


    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(pointA.transform.position, 0.5f);
        Gizmos.DrawWireSphere(pointB.transform.position, 0.5f);

        Gizmos.DrawLine(pointA.transform.position, pointB.transform.position);
    }
}
