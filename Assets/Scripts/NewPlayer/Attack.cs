using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Attack : MonoBehaviour
{

    [SerializeField] Animator anim;
    [SerializeField] InputActionReference attack;
    [SerializeField] AudioClip screamClip;
    [SerializeField] AudioSource scream;

    private Enemy enemy;
    private Bullet bullet;
    private void Start()
    {
        scream.clip = screamClip;
    }
    private void Update()
    {
        anim.SetBool("isAttacking", attack.action.ReadValue<float>() > 0);
        if (anim.GetBool("isAttacking"))
        {
            scream.Play();
        }
        
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
