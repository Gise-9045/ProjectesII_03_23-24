using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetection : MonoBehaviour
{
    [SerializeField] private GroundEnemy groundEnemy;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            groundEnemy.followPlayer = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        groundEnemy.followPlayer = false;

    }
}
