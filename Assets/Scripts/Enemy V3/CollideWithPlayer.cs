using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollideWithPlayer : MonoBehaviour
{
    private GameObject player;
    private Transform tr;

    [SerializeField] private int substrackLife;
    //[SerializeField] private int knockback;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        tr = GetComponent<Transform>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            player.GetComponent<PlayerStats>().TakeDamage(1, 20.0f, Mathf.Sign(player.transform.position.x - tr.position.x));
        }
    }
}