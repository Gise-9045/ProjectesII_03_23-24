using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlovckGroundCheck : MonoBehaviour
{
    private BoxCollider2D bc;
    private Rigidbody2D boxRb;

    private bool onGround;

    Vector2 p;
    Vector2 size;

    void Start()
    {
        bc = GetComponentInParent<BoxCollider2D>();
        boxRb = GetComponentInParent<Rigidbody2D>();
    }

    void Update()
    {
        p = (Vector2)this.transform.position - Vector2.up * (bc.size.y / 2.0f) + bc.offset - Vector2.up * bc.edgeRadius;
        size = new Vector2(bc.size.x, bc.edgeRadius * 2.0f);
        Collider2D[] colliders = Physics2D.OverlapBoxAll(p, bc.size, 0.0f);

        onGround = false;
        foreach(Collider2D collider in colliders)
        {
            if(collider.CompareTag("Ground"))
            {
                onGround = boxRb.velocity.x < 1.0f;
                break;
            }
        }
    }

    public bool OnGround()
    {
        return onGround;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawCube(p, bc.size);
    }

}
