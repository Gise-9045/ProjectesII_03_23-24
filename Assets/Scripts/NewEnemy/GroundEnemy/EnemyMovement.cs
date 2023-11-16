using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GroundEnemy : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform player;

    [SerializeField] private GroundEnemyDamage groundEnemyDamage;


    private Transform currentPoint;

    //VARIABLES
    [SerializeField] private float speed;

    public int direction = 1;
    public int health;
    public bool followPlayer;



    private void Start()
    {
        currentPoint = GetComponent<Transform>();
    }

    private void FixedUpdate()
    {

        if(groundEnemyDamage.GetKnockback())
        {
            rb.velocity = new Vector2(-Mathf.Sign(player.position.x - currentPoint.position.x) * groundEnemyDamage.GetKnockbackVel(), rb.velocity.y);
        }
        else if(followPlayer)
        {
            rb.velocity = new Vector2(Mathf.Sign(player.position.x - currentPoint.position.x) * speed, rb.velocity.y);

            direction = (int)Mathf.Sign(player.position.x - currentPoint.position.x);

        }
        else 
        {
            rb.velocity = new Vector2(direction * speed, rb.velocity.y);

        }

        Direction();

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


    public void Direction()
    {
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x = direction;
        gameObject.transform.localScale = currentScale;
    }
}
