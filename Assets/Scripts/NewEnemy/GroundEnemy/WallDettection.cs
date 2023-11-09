using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundEnemyWallDetection : MonoBehaviour
{
    RaycastHit2D rcGround;

    [SerializeField] private Collider2D colliderWall;
    [SerializeField] private Transform scale;
    [SerializeField] private float distance;

    [SerializeField] private GroundEnemy groundEnemy;


    private void Update()
    {
        rcGround = Physics2D.Raycast(new Vector2(gameObject.transform.position.x + groundEnemy.direction, gameObject.transform.position.y - (scale.localScale.y/2) - 0.1f), Vector2.down, distance);

        if(rcGround.collider != null)
        {
            Debug.DrawRay(new Vector2(gameObject.transform.position.x + groundEnemy.direction, gameObject.transform.position.y - (scale.localScale.y / 2) - 0.1f), Vector2.down * distance, Color.green);
        }


        if (rcGround.collider == null)
        {
            Debug.DrawRay(new Vector2(gameObject.transform.position.x + groundEnemy.direction, gameObject.transform.position.y - (scale.localScale.y/2) - 0.1f), Vector2.down * distance, Color.red);
            groundEnemy.Flip();


        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            groundEnemy.Flip();
        }
    }
}
