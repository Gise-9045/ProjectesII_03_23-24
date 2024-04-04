using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetBlovkCollider : MonoBehaviour
{
    BoxCollider2D BlovkCollider;
    SpriteRenderer BlovkRenderer;
    ParticleSystem BlovkParticles;

    void Start()
    {
        BlovkCollider = GetComponent<BoxCollider2D>();
        BlovkRenderer = GetComponentInChildren<SpriteRenderer>();
        BlovkParticles = GetComponentInChildren<ParticleSystem>();
       
        BlovkCollider.size = new Vector2 (BlovkRenderer.size.x, BlovkRenderer.size.y);
    }
}
