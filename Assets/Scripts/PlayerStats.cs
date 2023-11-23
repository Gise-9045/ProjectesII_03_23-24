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

    private void Update()
    {
        if(knockback && Time.timeScale == 1)
        {
            StartCoroutine(KnockBack());
        }
    }

    public void TakeDamage(int damage, float k, float direction)
    {

        health -= damage;

        knockbackVel = k;
        knockbackVel *= direction;


        knockback = true;
        CinemachineShake.Instance.ShakeCamera(5f, 0.125f);
        HitStop.Instance.StopTime(0f, 0.25f);




        if (health < 0)
        {
            health = 0;
            Time.timeScale = 1;
            SceneManager.LoadScene("MainMenu");
        }
    }


    private IEnumerator KnockBack()
    {
        yield return new WaitForSecondsRealtime(0.1f);
        knockback = false;
    }
}
