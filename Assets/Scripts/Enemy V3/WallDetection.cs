using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallDetection : MonoBehaviour
{
    RaycastHit2D rcGround;

    private Transform parent;
    [SerializeField] private float distanceX;
    [SerializeField] private float distanceY;

    [SerializeField] private Enemy enemy;

    private void Start()
    {
        parent = GetComponentInParent<Transform>();
    }

    private void FixedUpdate()
    {
        Vector2 pos = new Vector2(parent.transform.position.x + (enemy.GetDirection() * distanceX), parent.transform.position.y + distanceY);
        rcGround = Physics2D.Raycast(pos , Vector2.down, distanceY);

        if (rcGround.collider.tag == "Ground")
        {
            Debug.DrawRay(pos, Vector2.down, Color.green);
        }
        else
        {
            Debug.DrawRay(pos, Vector2.down, Color.red);

            enemy.SetDirection(enemy.GetDirection() * -1);
        }

        Debug.Log(rcGround.collider.tag);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground") || collision.CompareTag("Lever"))
        {
            enemy.SetDirection(enemy.GetDirection() * -1);
        }
    }
    //private void OnDrawGizmos()
    //{
    //    parent = GetComponentInParent<Transform>();

    //    Debug.DrawRay(new Vector2(parent.transform.position.x + (1 * distanceX), parent.transform.position.y + distanceY), Vector2.down, Color.blue);
    //}
}
