using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int life;
    private int direction;
    [SerializeField] int speed;

    private void Start()
    {
        direction = 1;
    }

    public void AddLife(int l)
    {
        life += l;
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

    private void Update()
    {
        //EnemyDirection
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x = direction;
        gameObject.transform.localScale = currentScale;
    }
}
