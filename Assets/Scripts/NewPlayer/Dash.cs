using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Dash : MonoBehaviour
{
    [SerializeField] private float _secondTime = .15f;
    [SerializeField] private float _dashSpeed = 20f;

    public void Dash_player(bool hasDashed, Rigidbody2D physics, Vector2 playerPosition , Jump _jump , bool wallJumped, bool isDashing)
    {
        //movimiento camara 
        hasDashed = true; 
        physics.velocity = Vector2.zero;

        Vector2 dir = new Vector2(playerPosition.x, playerPosition.y); 
        physics.velocity += dir.normalized * _dashSpeed ;
        StartCoroutine(DashWait(physics, _jump, wallJumped, isDashing)); 
    }


    IEnumerator DashWait(Rigidbody2D physics, Jump _jumps , bool wallJumped, bool isDashing)
    {
        //ghostTrail 

        //PARTICULAS 
        physics.gravityScale = 0;
        GetComponent<BetterJumping>().enabled = false; 

        wallJumped = true;
        isDashing = true; 

        yield return new WaitForSeconds(_secondTime*2);

        physics.gravityScale = 3;
        GetComponent<BetterJumping>() .enabled = true;
        wallJumped = false; 
        isDashing=false;

    }


    IEnumerator GroundDash(Collisions _collision, bool hasDashed)
    {
        yield return new WaitForSeconds(_secondTime); 
        if(_collision.onGround)
        {
            hasDashed = false; 
        }
    }

    private void RigidbodyDrag(float x, Rigidbody physics)
    {
        physics.drag = x;
    }
}
