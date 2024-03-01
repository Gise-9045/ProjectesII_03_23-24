using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundDetection : MonoBehaviour
{
    private bool onGround;

    private BoxCollider2D boxCol;
    private Rigidbody2D playerRb;

    public Action OnGroundTouchdown;
    public Action OnLeaveGround;


    void Start()
    {
        boxCol = GetComponentInParent<BoxCollider2D>();
        playerRb = GetComponentInParent<Rigidbody2D>();
    }


    public bool OnGround()
    {
        return onGround;
    }

    private void Update()
    {
        bool previous = onGround;

        Vector2 p = (Vector2)this.transform.position - Vector2.up * (boxCol.size.y / 2.0f) + boxCol.offset - Vector2.up * boxCol.edgeRadius;
        Vector2 size = new Vector2(boxCol.size.x, boxCol.edgeRadius * 2.0f);
        Collider2D[] colliders = Physics2D.OverlapBoxAll(p, size, 0.0f);

        onGround = false;
        foreach(Collider2D collider in colliders)
        {
            if(collider.CompareTag("Ground") || collider.CompareTag("ColorChange")|| collider.CompareTag("PuzzleBox") || collider.CompareTag("Ladder"))
            {
                onGround = playerRb.velocity.y < 1.0f;
                break;
            }
        }

        if(onGround && !previous)
        {
            if (OnGroundTouchdown != null)
                OnGroundTouchdown.Invoke();
        }
        else if(!onGround && previous)
        {
            if (OnLeaveGround != null)
                OnLeaveGround.Invoke();
        }
    }
}
