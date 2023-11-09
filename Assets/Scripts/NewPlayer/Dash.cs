using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;


public class Dash : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _physics;
    [SerializeField] private Move playermove;
    public bool canDash = true;
    private bool isDashing;
    private float dashtime = 0.2f;
    private float dashLimit = 3f;
    private float timesIDash = 0;
    private float dashingCooldown = 2f;
    
    [SerializeField] private TrailRenderer tr;

    private void FixedUpdate()
    {
        if ((Time.timeScale>=1f))
        {
            if (isDashing)
            {
                return;
            }
        }
        
        
    }
    private IEnumerator Dashing()
    {
        if(Time.timeScale>=1f)
        {
            canDash = false;
            isDashing = true;
            float originalGravity = _physics.gravityScale;
            
            if(_physics.gravityScale!=00.2f) {
                playermove.maxSpeed = 50f;
                playermove.currentSpeed = 50f;
                _physics.velocity = new Vector2(50, 5f);
                _physics.velocity.Normalize();
                timesIDash++;
                if(timesIDash >= dashLimit)
                {
                    canDash = false;
                    isDashing = false;
                }
            }
            
            tr.emitting = true;
            yield return new WaitForSeconds(dashtime);
            tr.emitting = false;
            playermove.maxSpeed = 10;
            _physics.gravityScale = originalGravity;
            isDashing = false;
            yield return new WaitForSeconds(dashingCooldown);
            canDash = true;
        }
    }
    public void PlayerDashing()
    {
        StartCoroutine(Dashing());
    }
}
