using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int life;
    private int direction;
    [SerializeField] int speed;

    private bool knockback;
    private float knockbackVel;

    public bool GetKnockback()
    {
        return knockback;
    }

    public float GetKnockbackVel()
    {
        return knockbackVel;
    }



    private void Start()
    {
        direction = 1;

    }

    public void SetLife(int l)
    {
        life = l;
    }
    public int GetLife()
    {
        return life;
    }

    public void SetSpeed(int s)
    {
        speed = s;
    }

    public int GetSpeed()
    {
        return speed;
    }

    public void SetDirection(int d)
    {
        //-1 izquierda 1 derecha
        direction = d;
    }
    public int GetDirection()
    {
        return direction;
    }


    public void TakeDamage(int subtractLife, bool knock, float knockVel)
    {
        life -= subtractLife;

        knockback = knock;
        knockbackVel = knockVel;

        HitParticles.Instance.DisableEnemy();
        HitParticles.Instance.EnableEnemy(gameObject.transform.position.x, gameObject.transform.position.y);

        if (knockback)
        {
            StartCoroutine(KnockBack());
        }

        if (life <= 0 )
        {
            CinemachineShake.Instance.ShakeCamera(5f, 0.5f);
            HitStop.Instance.StopTime(0.15f, 0.5f);
            Destroy(gameObject);
        }
    }

    private IEnumerator KnockBack()
    {
        yield return new WaitForSecondsRealtime(0.1f);
        knockback = false;
    }



    private void Update()
    {
        //EnemyDirection
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x = direction;
        gameObject.transform.localScale = currentScale;
    }
}
