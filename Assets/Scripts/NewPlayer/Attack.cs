using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Attack : MonoBehaviour
{

    [SerializeField] Animator anim;
    [SerializeField] InputActionReference attack;

    private Enemy enemy;
    private FlyingEnemyDamage flyingEnemy;
    private Bullet bullet;
    private void Update()
    {
        anim.SetBool("isAttacking", attack > 0);
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if(collision.tag == "Enemy")
        {
            collision.GetComponent<Enemy>().TakeDamage(1, true, 20.0f);
        }

        if(collision.tag == "Lever")
        {
            collision.GetComponent<leverActivation>().Toggle();
        }
    }
}
