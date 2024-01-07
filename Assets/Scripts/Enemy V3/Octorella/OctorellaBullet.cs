using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OctorellaBullet : MonoBehaviour
{
    private Rigidbody2D rb;
    private Transform player;

    private Transform tr;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player").GetComponent<Transform>();
        tr = GetComponent<Transform>();

        float distanceFromPlayer = player.position.x - tr.position.x;

        rb.AddForce(new Vector2(distanceFromPlayer * 3, 0), ForceMode2D.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<PlayerStats>().TakeDamage(1, 20.0f, Mathf.Sign(collision.gameObject.transform.position.x - tr.position.x));
            Destroy(gameObject);
        }

        if (collision.tag == "Ground")
        {
            Destroy(gameObject);
        }
    }
}
