using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform player;
    [SerializeField] private PlayerStats playerStats;

    Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //rb.velocity = new Vector2(3, rb.velocity.y);
        bullet.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            playerStats.TakeDamage(1, 20.0f, Mathf.Sign(player.position.x - bullet.transform.position.x));
            Destroy(bullet);
        }

        if (collision.tag == "Ground")
        {
            Destroy(bullet);
        }
    }
}
