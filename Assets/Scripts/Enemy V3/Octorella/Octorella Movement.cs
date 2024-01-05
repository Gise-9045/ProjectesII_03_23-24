using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OctorellaMovement : MonoBehaviour
{
    private Enemy enemy;

    private Transform tr;
    private Transform player;

    private Rigidbody2D rb;

    private PlayerDetection detection;

    private GroundDetection ground;

    [SerializeField] Animator animator;


    [SerializeField] private GameObject bullet;

    [SerializeField] private float cooldownJump;
    float actualCooldownJump;

    [SerializeField] private float cooldownShoot;
    float actualCooldownShoot;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        enemy = GetComponent<Enemy>();
        detection = GetComponentInChildren<PlayerDetection>();
        ground = GetComponentInChildren<GroundDetection>();

        player = GameObject.FindWithTag("Player").GetComponent<Transform>();
        tr = GetComponent<Transform>();


        actualCooldownJump = 0f;
        actualCooldownShoot = 0f;
    }

    private void FixedUpdate()
    {
        enemy.SetDirection(new Vector2((int)Mathf.Sign(player.position.x - tr.position.x), 1));

        if (actualCooldownJump > 0 && ground.OnGround())
        {
            actualCooldownJump -= Time.deltaTime;
            animator.SetBool("Open", false);
        }

        if (actualCooldownShoot > 0 && !ground.OnGround())
        {
            actualCooldownShoot -= Time.deltaTime;
        }

        if (detection.GetPlayerDetection() && actualCooldownJump <= 0 && ground.OnGround())
        {
            actualCooldownJump = cooldownJump;

            rb.gravityScale = 9.81f;


            rb.AddForce(new Vector2(0, 400), ForceMode2D.Impulse);
        }


        if (rb.velocity.y < 0 && actualCooldownShoot <= 0 && !ground.OnGround())
        {
            rb.gravityScale = 0.5f;
            actualCooldownShoot = cooldownShoot;

            animator.SetBool("Open", true);


            //Si jugador está bastante fuera del rango no dispara
            if (Mathf.Abs(player.position.x - tr.position.x) < 10)
            {
                Vector2 bulletPos = new Vector2(tr.position.x + (enemy.GetDirection().x * 1), tr.position.y + 0.4f);
                Instantiate(bullet, bulletPos, Quaternion.identity);
            }
        }
    }
}
