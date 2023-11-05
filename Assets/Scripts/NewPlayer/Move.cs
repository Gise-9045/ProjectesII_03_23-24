using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public void Walk(Vector2 positionPlayer, bool canMove, bool wallJumped , Rigidbody2D physics , float speed ,float wallJumpLerp) 
    {
        if (!canMove)
            return; 
        if(!wallJumped)
        {
            physics.velocity = new Vector2(positionPlayer.x * speed, physics.velocity.y); 
        }
        else
        {
            physics.velocity = Vector2.Lerp(physics.velocity, (new Vector2(positionPlayer.x * speed, physics.velocity.y)), wallJumpLerp * Time.deltaTime); 
        }
    }
}
