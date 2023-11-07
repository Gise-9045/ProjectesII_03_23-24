using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Dash : MonoBehaviour
{
    [Space]
    [Header("Dash variables")]
    [SerializeField] private float _dashingTime = 1f;
    [SerializeField] private float _dashingCooldown = 1f;
    [SerializeField] private float _dashSpeed = 20f;

    [Header("Tail")]
    [SerializeField] private TrailRenderer _trailRenderer;

    private bool _enabledDash;
    private Rigidbody2D _physics;
    private Jump _jump;
    private Collisions _collision;

    private bool isDashing; // caja destructible mapa

    private void Awake() {
        _physics = GetComponent<Rigidbody2D>();
        _jump = GetComponent<Jump>();
        _collision = GetComponent<Collisions>();
    }

    public void PlayerDashing()
    {
        StartCoroutine(Dashing());
    }

    private IEnumerator Dashing()
    {
        _enabledDash = false;
        isDashing = true;
        float originalGravity = _physics.gravityScale;
        _physics.gravityScale = 0;

        _physics.velocity = new Vector2(transform.localScale.x * _dashSpeed, 0f);


        yield return new WaitForSeconds(_dashingTime);
        _trailRenderer.emitting = false;
        _physics.gravityScale = originalGravity;
        isDashing = false;

        yield return new WaitForSeconds(_dashingCooldown);
        _enabledDash = true; 
    }
 
    //}
    //public void Dash_player()
    //{
    //    //movimiento camara 
    //    _physics.velocity = Vector2.zero;
    //    Vector2 dir = new Vector2(_physics.position.x, _physics.position.y); 
    //    _physics.velocity += dir.normalized * _dashSpeed ;
    //    StartCoroutine(DashWait()); 
    //}

    //IEnumerator DashWait()
    //{
    //    Rigidbody2D aux = _physics; 

    //    //PARTICULAS 
    //    _physics.gravityScale = 0f; 
    //    yield return new WaitForSeconds(_secondTime*2);
    //    _physics.gravityScale = aux.gravityScale; 
    //}


    //IEnumerator GroundDash()
    //{
    //    if(_collision.onGround)
    //    {
    //        // Dash en el suelo    
    //    }
    //    yield return new WaitForSeconds(_secondTime);
    //}
}
