using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private InputActionReference attack;

    public float isAttacking;

    [SerializeField] private Collider2D weapon;
    public GroundEnemy groundEnemy;


    private void Update()
    {
        isAttacking = attack.action.ReadValue<float>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Enemy")
        {
            collision.GetComponent<EnemyDamage>().TakeDamage(1, 20.0f);
        }

        //if (collision.tag == "FlyingEnemy")
        //{
        //    collision.GetComponent<FlyingEnemy>().TakeDamage(1, 20.0f);

        //}
    }
}
