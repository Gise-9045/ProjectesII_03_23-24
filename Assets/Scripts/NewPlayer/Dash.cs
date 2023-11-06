using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Dash : MonoBehaviour
{
    [SerializeField] private float _secondTime = .15f;
    [SerializeField] private float _dashSpeed = 20f;

    private Rigidbody2D _physics;
    private Jump _jump;
    private Collisions _collision;

    private void Awake() {
        _physics = GetComponent<Rigidbody2D>();
        _jump = GetComponent<Jump>();
        _collision = GetComponent<Collisions>();
    }

    public void Dash_player()
    {
        //movimiento camara 
        _physics.velocity = Vector2.zero;
        Vector2 dir = new Vector2(_physics.position.x, _physics.position.y); 
        _physics.velocity += dir.normalized * _dashSpeed ;
        StartCoroutine(DashWait()); 
    }


    IEnumerator DashWait()
    {
        //ghostTrail 

        //PARTICULAS 
        
        yield return new WaitForSeconds(_secondTime*2);
    }


    IEnumerator GroundDash()
    {
        if(_collision.onGround)
        {
            // Dash en el suelo    
        }
        yield return new WaitForSeconds(_secondTime);
    }
}
