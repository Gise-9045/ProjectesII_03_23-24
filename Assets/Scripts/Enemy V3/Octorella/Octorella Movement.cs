using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OctorellaMovement : MonoBehaviour
{
    private Enemy enemy;

    private Rigidbody2D rb;

    private PlayerDetection detection;

    [SerializeField] private float cooldownAttack = 1.5f;
    float actualCooldownAttack;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        enemy = GetComponent<Enemy>();
        detection = GetComponentInChildren<PlayerDetection>();
    }

    private void FixedUpdate()
    {
        if (actualCooldownAttack > 0)
        {
            actualCooldownAttack -= Time.deltaTime;

        }

        if(detection.GetPlayerDetection() && actualCooldownAttack < 0)
        {
            //Hace salto y dispara directo al jugador

            rb.AddForce(new Vector2(enemy.GetDirection().x, 0), ForceMode2D.Impulse);

            Vector2 d = rb.velocity;
        }
    }
}
