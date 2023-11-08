using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    Rigidbody2D movement;

    private int health = 3;
    private int maxHealh = 3;

    bool knockback = false;
    float knockbackVel;

    private void Start()
    {
        movement = GetComponent<Rigidbody2D>();
    }
    //EN EL CONTROLLER TIENE QUE COMPROBAR SI knockback == TRUE Y PONER ESTA LINEA

    //ARREGLAR
    //rb.velocity = new Vector2(-direction* knockbackVel, rb.velocity.y);


    public int GetHealth()
    {
        return health;
    }

    public void TakeDamage(int damage, float k, float direction)
    {
        health -= damage;

        knockbackVel = k;

        StartCoroutine(HitStop());


        if (health < 0)
        {
            health = 0;
        }
    }

    private IEnumerator KnockBack()
    {
        knockback = true;
        yield return new WaitForSecondsRealtime(0.1f);
        knockback = false;
    }

    private IEnumerator HitStop()
    {
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(0.25f);
        Time.timeScale = 1;

        StartCoroutine(KnockBack());
    }
}
