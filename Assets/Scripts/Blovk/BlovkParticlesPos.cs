using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlovkParticlesPos : MonoBehaviour
{
    private Transform tr;
    [SerializeField] private Transform particleTr;
    private Rigidbody2D rb;
    [SerializeField] private ParticleSystem boxParticles;
    SpriteRenderer BlovkRenderer;

    private bool onGround;

    void Start()
    {
        tr = GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
        BlovkRenderer = GetComponentInChildren<SpriteRenderer>();

        onGround = true;
    }

    void Update()
    {
        if(rb.velocity.x > 1 && onGround)
        {
            particleTr.position = new Vector2(tr.position.x - 0.5f, tr.position.y -0.5f*BlovkRenderer.size.y);

            if(!boxParticles.isPlaying)
            {
                boxParticles.Play();
            }

        }
        else if (rb.velocity.x < -1 && onGround)
        {
            particleTr.position = new Vector2(tr.position.x + 0.5f, tr.position.y -0.5f * BlovkRenderer.size.y);

            if(!boxParticles.isPlaying)
            {
                boxParticles.Play();
            }
        }
        else
        {
            boxParticles.Stop();


        }

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            onGround = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            onGround = false;
        }
    }
}
