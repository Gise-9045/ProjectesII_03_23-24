using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundDetection : MonoBehaviour
{
    RaycastHit2D rcGround;
    private Transform parent;

    [SerializeField] private float distanceX;
    [SerializeField] private float distanceY;
    [SerializeField] private float height;

    private bool onGround;

    [SerializeField]
    private BoxCollider2D boxCol;
    [SerializeField]
    private Rigidbody2D playerRb;

    public Action OnGroundTouchdown;
    public Action OnLeaveGround;


    void Start()
    {
        parent = GetComponentInParent<Transform>();
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
            if(collider.CompareTag("Ground") || collider.CompareTag("ColorChange"))
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
