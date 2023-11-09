using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundEnemyDamage : MonoBehaviour
{
    [SerializeField] private GroundEnemy groundEnemy;

    bool knockback = false;
    float knockbackVel;

    public bool GetKnockback()
    {
        return knockback;
    }

    public float GetKnockbackVel()
    {
        return knockbackVel;
    }

    public void TakeDamage(int damage, float k)
    {
        //MIRAR DE PONER MAS BONITO (GETTER, SETTER)
        groundEnemy.health -= damage;


        knockbackVel = k;
        StartCoroutine(HitStop());
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
