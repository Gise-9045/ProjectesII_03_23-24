using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MauriceJrMovement : MonoBehaviour
{
    private Enemy enemy;

    private Rigidbody2D rb;
    private Transform player;
    private Transform tr;
    private WallDetection wallDetection;

    private float cooldownJump;
    private float actualCooldownJump;

    private int jumpHeight;
    private int jumpWidth;

    private GroundDetection ground;

    private void Start()
    {
        cooldownJump = 0.5f;
        rb = GetComponent<Rigidbody2D>();
        ground = GetComponentInChildren<GroundDetection>();
        enemy = GetComponent<Enemy>();
        wallDetection = GetComponentInChildren<WallDetection>();

        tr = GetComponent<Transform>();
        player = GameObject.FindWithTag("Player").GetComponent<Transform>();

        actualCooldownJump = 0.5f;
    }

    private void FixedUpdate()
    {
        if (actualCooldownJump > 0 && ground.OnGround())
        {
            actualCooldownJump -= Time.deltaTime;
            //CinemachineShake.Instance.ShakeCamera(5f, 0.1f);
        }

        if (ground.OnGround() && actualCooldownJump < 0)
        {
            actualCooldownJump = cooldownJump;

            if (enemy.GetLife() >= enemy.GetMaxLife() / 2)
            {
                cooldownJump = 0.5f;
                jumpHeight = 350;
                jumpWidth = 100;
                NormalMovement();
            }
            else if(enemy.GetLife() >= enemy.GetMaxLife() * 0.25 && enemy.GetLife() < enemy.GetMaxLife() / 2)
            {
                cooldownJump = 0.5f;
                jumpHeight = 400;
                PersuePlayer();
            }
            else
            {
                cooldownJump = 0.1f;
                jumpHeight = 500;
                PersuePlayer();
            }
        }

        if (wallDetection.GetWallDetection())
        {
            enemy.SetDirection(new Vector2(enemy.GetDirection().x * -1, 1));
        }

    }

    void NormalMovement()
    {
        rb.AddForce(new Vector2(enemy.GetDirection().x * jumpWidth, jumpHeight), ForceMode2D.Impulse);
    }

    void PersuePlayer()
    {
        float distanceFromPlayer = player.position.x - tr.position.x;

        rb.AddForce(new Vector2(distanceFromPlayer * 10, jumpHeight), ForceMode2D.Impulse);
    }
}
