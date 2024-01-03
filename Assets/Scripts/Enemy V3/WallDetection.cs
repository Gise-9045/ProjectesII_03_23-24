using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallDetection : MonoBehaviour
{
    [SerializeField] private Enemy enemy;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground") || collision.CompareTag("Lever") || collision.CompareTag("Enemy"))
        {
            enemy.SetDirection(new Vector2(enemy.GetDirection().x * -1, 1));

        }
    }



}
