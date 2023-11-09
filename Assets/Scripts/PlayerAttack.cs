using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private InputActionReference attack;

    public float isAttacking;

    //[SerializeField] private Collider2D weapon;
    private GroundEnemyDamage groundEnemy;
    private FlyingEnemyDamage flyingEnemy;
    private Bullet bullet;


    private void Update()
    {
        isAttacking = attack.action.ReadValue<float>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if(collision.tag == "GroundEnemy")
        {
            collision.GetComponent<GroundEnemyDamage>().TakeDamage(1, 20.0f);
        }

        if (collision.tag == "FlyingEnemy")
        {
            collision.GetComponent<FlyingEnemyDamage>().TakeDamage(1, 20.0f);

        }
    }
}
