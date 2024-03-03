using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class DamageHazards : MonoBehaviour
{
    private GameObject player;
    [SerializeField] private Player playerStats; 

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.collider.tag == "Player" && playerStats.canDie)
        {
            player.GetComponent<PlayerStats>().TakeDamage(1, 20.0f, Mathf.Sign(player.transform.position.x - gameObject.transform.position.x));
        }
    }
}
