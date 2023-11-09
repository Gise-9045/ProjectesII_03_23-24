using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour
{

    private int health = 3;
    private int maxHealh = 3;

    public bool knockback = false;
    public float knockbackVel;

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
        knockbackVel *= direction;


        StartCoroutine(HitStop());


        if (health < 0)
        {
            health = 0;
            Time.timeScale = 1;
            SceneManager.LoadScene("MainMenu");


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
