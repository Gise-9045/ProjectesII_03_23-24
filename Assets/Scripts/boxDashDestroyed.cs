using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boxDashDestroyed : MonoBehaviour
{
    // Start is called before the first frame update
    public float impactSpeedThreshold = 5f; // Speed threshold for destruction

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            float playerSpeed = collision.relativeVelocity.magnitude;

            if (playerSpeed >= impactSpeedThreshold)
            {
                Destroy(gameObject); // Destroy the box if collided at or above the threshold speed
            }
        }
    }
}
