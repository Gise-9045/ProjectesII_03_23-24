using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour
{
    private int health = 3;
    private int maxHealh = 3;

    public bool knockback = false;
    public float knockbackVel;

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

        HitParticles.Instance.DisablePlayer();
        HitParticles.Instance.EnablePlayer(gameObject.transform.position.x, gameObject.transform.position.y);

        knockback = true;
        CinemachineShake.Instance.ShakeCamera(5f, 0.5f);
        HitStop.Instance.StopTime(0f, 0.5f);




        if (health <= 0)
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
