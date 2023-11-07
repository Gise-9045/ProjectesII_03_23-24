using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GroundEnemy : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;


    private Transform currentPoint;

    //VARIABLES
    [SerializeField] private float speed;

    public int direction = 1;
    public int health = 3;
    bool knockback = false;
    float knockbackVel;


    private void Start()
    {
        currentPoint = GetComponent<Transform>();
    }

    private void FixedUpdate()
    {
        if(knockback)
        {
            rb.velocity = new Vector2(-direction * knockbackVel, rb.velocity.y);
        }
        else 
        {
            rb.velocity = new Vector2(direction * speed, rb.velocity.y);

        }


        //HEALTH
        if (health <= 0)
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

        knockback = false;
    }

    public void TakeDamage(int damage, float k)
    {
        //health -= damage;

        //Vector 2D de la fuerza y el como le actua al objeto
        //rb.AddForce(new Vector2(knockback, 0f));

        knockbackVel = k;
        StartCoroutine(KnockBack());
    }

    private IEnumerator KnockBack()
    {
        knockback = true;
        yield return new WaitForSeconds(0.1f);
        knockback = false;
    }
}
