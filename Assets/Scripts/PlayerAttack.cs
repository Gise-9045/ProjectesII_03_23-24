using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    public float isAttacking;

    [SerializeField] private Collider2D weapon;
    public GroundEnemy groundEnemy;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.tag == "Enemy");

        if(collision.tag == "Enemy")
        {
            groundEnemy.TakeDamage(1, 20.0f);
        }
    }

    void Start()
    {

    }



    void Update()
    { 

    }

}
