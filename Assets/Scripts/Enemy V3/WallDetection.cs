using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallDetection : MonoBehaviour
{
    RaycastHit2D rcGround;

    [SerializeField] private Transform scale;
    [SerializeField] private float distanceY;
    [SerializeField] private float distanceX;

    [SerializeField] private Enemy enemy;


    private void Update()
    {
        //Arreglar para que sea ajustable por el programador
        rcGround = Physics2D.Raycast(new Vector2(gameObject.transform.position.x + enemy.GetDirection() * distanceX, gameObject.transform.position.y - (scale.localScale.y / 2) - 0.1f), Vector2.down, distanceY);

        if (rcGround.collider != null)
        {
            Debug.DrawRay(new Vector2(gameObject.transform.position.x + enemy.GetDirection() * distanceX, gameObject.transform.position.y - (scale.localScale.y / 2) - 0.1f), Vector2.down * distanceY, Color.green);
        }


        if (rcGround.collider == null)
        {
            Debug.DrawRay(new Vector2(gameObject.transform.position.x + enemy.GetDirection() * distanceX, gameObject.transform.position.y - (scale.localScale.y / 2) - 0.1f), Vector2.down * distanceY, Color.red);
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
}
