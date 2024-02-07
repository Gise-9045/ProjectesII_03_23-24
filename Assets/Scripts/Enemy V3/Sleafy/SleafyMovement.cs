using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SleafyMovement : MonoBehaviour
{
    private Enemy enemy;
    private GroundDetection groundDetection;
    private WallDetection wallDetection;


    private Rigidbody2D rb;
    private Transform tr;
    private PlayerDetection detection;
    private Transform player;

    [SerializeField] Animator animator;

    bool persue;
    Vector2 startPos;

    [SerializeField] private float cooldownAppear;
    float actualCooldownAppear;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        tr = GetComponent<Transform>();
        enemy = GetComponent<Enemy>();
        groundDetection = GetComponentInChildren<GroundDetection>();
        wallDetection = GetComponentInChildren<WallDetection>();

        detection = GetComponentInChildren<PlayerDetection>();

        player = GameObject.FindWithTag("Player").GetComponent<Transform>();

        persue = false;

        startPos = tr.position;

        actualCooldownAppear = 0f;
    }

    private void FixedUpdate()
    {
        //enemy.SetDirection(new Vector2((int)Mathf.Sign(player.position.x - tr.position.x), 1));

        if(actualCooldownAppear > 0)
        {
            actualCooldownAppear -= Time.deltaTime;
        }


        if (enemy.GetKnockback())
        {
            rb.velocity = new Vector2(-Mathf.Sign(player.position.x - tr.position.x) * enemy.GetKnockbackVel(), rb.velocity.y);
            animator.SetBool("Enabled", false);
        }
        else
        {
            if (detection.GetPlayerDetection() && actualCooldownAppear <= 0)
            {
                animator.SetBool("Enabled", true);
                persue = true;
            }

            if (persue)
            {
                rb.velocity = new Vector2(enemy.GetDirection().x * enemy.GetSpeed(), rb.velocity.y);
            }
            else
            {
                rb.velocity = new Vector2(0, rb.velocity.y);
            }
        }

        if (!groundDetection.OnGround() || wallDetection.Detection())
        {
            animator.SetBool("Enabled", false);
            persue = false;
            rb.velocity = new Vector2(0, 0);
        }
    }


    void RestartEnemy()
    {
        tr.position = startPos;
        actualCooldownAppear = cooldownAppear;
    }
}
