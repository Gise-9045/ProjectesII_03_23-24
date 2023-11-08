using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GroundEnemy : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform player;

    [SerializeField] private EnemyDamage enemyDamage;


    private Transform currentPoint;

    //VARIABLES
    [SerializeField] private float speed;

    public int direction = 1;
    public int health;



    private void Start()
    {
        currentPoint = GetComponent<Transform>();
    }

    private void FixedUpdate()
    {

        if(enemyDamage.GetKnockback())
        {
            rb.velocity = new Vector2(-Mathf.Sign(player.position.x - currentPoint.position.x) * enemyDamage.GetKnockbackVel(), rb.velocity.y);
        }
        else 
        {
            rb.velocity = new Vector2(direction * speed, rb.velocity.y);

        }


        //HEALTH
        if (health <= 0 && Time.timeScale == 1)
        {
            Destroy(gameObject);
        }
    }

    public void Flip()
    {
        direction = -1 * direction;

        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x = direction;
        gameObject.transform.localScale = currentScale;
    }
}
