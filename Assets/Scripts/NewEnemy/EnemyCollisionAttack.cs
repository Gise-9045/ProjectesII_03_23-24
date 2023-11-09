using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollisionAttack : MonoBehaviour
{
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private Transform player;

    [SerializeField] private Transform currentPoint;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Player")
        {
            playerStats.TakeDamage(1, 20.0f, Mathf.Sign(player.position.x - currentPoint.position.x));
        }
    }
}
