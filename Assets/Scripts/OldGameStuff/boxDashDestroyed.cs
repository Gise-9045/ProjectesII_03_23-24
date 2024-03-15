using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boxDashDestroyed : MonoBehaviour
{
    // Start is called before the first frame update
    public float impactSpeedThreshold = 5f; // Speed threshold for destruction
    [SerializeField] private AudioClip breakingClip;
    [SerializeField] private AudioSource breakingSource;
    [SerializeField, Range(0f, 3f)] private float volumeAudio = 0.2f;

    private void FixedUpdate()
    {
        breakingSource.volume = volumeAudio;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            float playerSpeed = collision.relativeVelocity.magnitude;

            if (playerSpeed >= impactSpeedThreshold)
            {
                breakingSource.clip = breakingClip;
                breakingSource.Play();
                Destroy(gameObject); // Destroy the box if collided at or above the threshold speed
            }
        }
    }
}
