using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollideWithPlayer : MonoBehaviour
{
    [SerializeField] private PlayerStats playerStats;
    private Transform player;
    private Transform tr;

    [SerializeField] private int substrackLife;
    [SerializeField] private int knockback;

    private void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Transform>();
        tr = GetComponent<Transform>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            playerStats.TakeDamage(1, 20.0f, Mathf.Sign(player.position.x - tr.position.x));
        }
    }
}
