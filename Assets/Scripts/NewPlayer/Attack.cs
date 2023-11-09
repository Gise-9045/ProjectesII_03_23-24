using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Attack : MonoBehaviour
{

    [SerializeField] Animator anim;

    private GroundEnemyDamage groundEnemy;
    private FlyingEnemyDamage flyingEnemy;
    private Bullet bullet;

    public void StartAttack(float attack)
    {
        anim.SetBool("isAttacking", attack > 0);
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
        if(collision.tag == "Lever")
        {
            collision.GetComponent<leverActivation>().Toggle();
        }
    }
}
