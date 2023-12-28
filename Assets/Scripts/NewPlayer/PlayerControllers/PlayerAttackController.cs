using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttackController : MonoBehaviour
{

    [SerializeField] Animator anim;
    [SerializeField] private ParticleSystem attackParticles; 

    private Enemy enemy;
    private Bullet bullet;
    private void Update()
    {
        // NewInputManger._instance._playerAttackInput.action.ReadValue<float>() > 0); 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if(collision.tag == "Enemy")
        {
            collision.GetComponent<Enemy>().TakeDamage(1, true, 20.0f);
        }
    }

    public void Attack()
    {
        attackParticles.Play();
        anim.SetBool("isAttacking",true);
    }

}
