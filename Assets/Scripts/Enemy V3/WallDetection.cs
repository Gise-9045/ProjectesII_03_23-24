using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallDetection : MonoBehaviour
{
    RaycastHit2D rcGround;

    [SerializeField] private Transform parent;
    [SerializeField] private float distanceX;
    [SerializeField] private float distanceY;

    [SerializeField] private Enemy enemy;

    private void Start()
    {
        parent = GetComponentInParent<Transform>();
    }

    private void Update()
    {
        //Arreglar para que sea ajustable por el programador
        rcGround = Physics2D.Raycast(new Vector2(parent.transform.position.x + (enemy.GetDirection() * distanceX), parent.transform.position.y + distanceY) , Vector2.down);

        if (rcGround.collider != null)
        {
            Debug.DrawRay(new Vector2(parent.transform.position.x + (enemy.GetDirection() * distanceX), parent.transform.position.y + distanceY), Vector2.down, Color.green);
        }


        if (rcGround.collider == null)
        {
            Debug.DrawRay(new Vector2(parent.transform.position.x + (enemy.GetDirection() * distanceX), parent.transform.position.y + distanceY), Vector2.down, Color.red);
            
            enemy.SetDirection(enemy.GetDirection() * -1);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            enemy.SetDirection(enemy.GetDirection() * -1);
        }
    }
    private void OnDrawGizmos()
    {
        parent = GetComponentInParent<Transform>();

        Debug.DrawRay(new Vector2(parent.transform.position.x + (1 * distanceX), parent.transform.position.y + distanceY), Vector2.down, Color.blue);
    }
}
