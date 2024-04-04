using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroolantBullet : MonoBehaviour
{
    Rigidbody2D rb;


    void Start()
    {
        
        rb = GetComponent<Rigidbody2D>();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.gameObject.GetComponent<Player>().TakeDamage();
            //collision.GetComponent<PlayerStats>().TakeDamage(1, 20.0f, Mathf.Sign(collision.gameObject.transform.position.x - gameObject.transform.position.x));
            Destroy(gameObject);
        }

        if (collision.tag == "Ground")
        {
            Destroy(gameObject);
        }
    }
}
