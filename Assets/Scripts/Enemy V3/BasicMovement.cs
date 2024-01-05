using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMovement : MonoBehaviour
{
    private Enemy enemy;
    private PlayerDetection detection;

    private Rigidbody2D rb;
    private Transform tr;
    private Transform player;
    [SerializeField] private AudioSource slimeMoving;
    [SerializeField] private AudioClip movingClip;
    [SerializeField, Range(0f, 3f)] private float volumeAudio = 0.2f;

    private void Start()
    {
        
        rb = GetComponent<Rigidbody2D>();
        tr = GetComponent<Transform>();
        enemy = GetComponent<Enemy>();
        detection = GetComponentInChildren<PlayerDetection>();


        player = GameObject.FindWithTag("Player").GetComponent<Transform>();

    }


    private void FixedUpdate()
    {
        slimeMoving.volume = volumeAudio;
        if (enemy.GetKnockback())
        {
            rb.velocity = new Vector2(-Mathf.Sign(player.position.x - tr.position.x) * enemy.GetKnockbackVel(), rb.velocity.y);
        }
        else
        {
            if (detection.GetPlayerDetection())
            {
                slimeMoving.clip = movingClip;
                slimeMoving.Play();
                enemy.SetDirection(new Vector2((int)Mathf.Sign(player.position.x - tr.position.x), 1));
            }

            rb.velocity = new Vector2(enemy.GetDirection().x * enemy.GetSpeed(), rb.velocity.y);
        }
    }

}
