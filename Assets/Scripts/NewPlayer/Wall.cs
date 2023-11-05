using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    public void WallJump(bool canMove , Collisions _collision , Jump _jump , bool wallJumped, Rigidbody2D physics, float jumpForce)
    {
        StopCoroutine(DisableMovement(0 , canMove)); 
        StartCoroutine(DisableMovement(.1f , canMove));

        Vector2 wallDir = _collision.onRightWall ? Vector2.left : Vector2.right;

        _jump.Jump_player((Vector2.up / 1.5f + wallDir / 1.5f),physics,jumpForce);

        wallJumped = true; 

    }
    public void WallSlide(/* , int side*/ bool canMove,  Rigidbody2D physics, Collisions _collision, float slideSpeed)
    {
        if(!canMove)
        { return; }

        bool pushingWall = false; 
        
        if((physics.velocity.x > 0 && _collision.onRightWall) || (physics.velocity.x < 0 && _collision.onLeftWall ))
        {
            pushingWall = true;
        }

        float push = pushingWall ? 0 : physics.velocity.x;

        physics.velocity = new Vector2(push, -slideSpeed);
    }

    private IEnumerator DisableMovement(float time, bool canMove)
    {
        canMove = false; 
        yield return new WaitForSeconds(time);
        canMove = true;
    }
}
