using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallDetection : MonoBehaviour
{
    private Enemy enemy;
    private bool detection;

    private void Start()
    {
        enemy = GetComponent<Enemy>();
    }

    public bool Detection()
    {
        return detection;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground") || collision.CompareTag("Lever") || collision.CompareTag("Enemy"))
        {
            detection = true;

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground") || collision.CompareTag("Lever") || collision.CompareTag("Enemy"))
        {
            detection = false;

        }
    }
}
