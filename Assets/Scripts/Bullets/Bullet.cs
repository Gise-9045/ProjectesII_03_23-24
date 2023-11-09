using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private PlayerStats playerStats;

    Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gameObject.SetActive(true);
    }

    private void Update() 
    {
        rb.velocity = new Vector2(5, rb.velocity.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<PlayerStats>().TakeDamage(1, 20.0f, Mathf.Sign(collision.gameObject.transform.position.x - gameObject.transform.position.x));
            Destroy(gameObject);
        }

        if (collision.tag == "Ground")
        {
            Destroy(gameObject);
        }
    }
}
