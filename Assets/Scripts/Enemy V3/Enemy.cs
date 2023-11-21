using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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


    public void TakeDamage(int life, bool knockback, float knockbackVel)
    {
        this.life -= life;

        this.knockback = knockback;
        this.knockbackVel = knockbackVel;

        if(this.life <= 0)
        {
            //HITSTOP
            CinemachineShake.Instance.ShakeCamera(5f, 0.1f);
            Destroy(gameObject);
        }
    }



    private void Update()
    {
        //EnemyDirection
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x = direction;
        gameObject.transform.localScale = currentScale;
    }
}
