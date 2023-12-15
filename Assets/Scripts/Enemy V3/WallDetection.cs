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

    private bool onGround;
    [SerializeField] private bool detectOnly;

    private void Start()
    {
        parent = GetComponentInParent<Transform>();
    }

    private void FixedUpdate()
    {
        Vector2 pos = new Vector2(parent.transform.position.x + (enemy.GetDirection().x * distanceX), parent.transform.position.y + distanceY);
        rcGround = Physics2D.Raycast(pos , Vector2.down, distanceY);

        if (rcGround.collider != null && rcGround.collider.tag == "Ground")
        {
            Debug.DrawRay(pos, Vector2.down, Color.green);
            onGround = true;
        }
        else
        {
            Debug.DrawRay(pos, Vector2.down, Color.red);

            onGround = false;

            if(!detectOnly)
            {
                enemy.SetDirection(new Vector2(enemy.GetDirection().x * -1, 1));
            }
        }

        //Debug.Log(rcGround.collider.tag);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground") || collision.CompareTag("Lever") || collision.CompareTag("Enemy"))
        {
            enemy.SetDirection(new Vector2(enemy.GetDirection().x * -1, 1));

        }
    }


    public bool OnGround()
    {
        return onGround;
    }
}
