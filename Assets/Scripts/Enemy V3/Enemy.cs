using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private int life;
    private int direction;
    [SerializeField] int speed;

    public void AddLife(int l)
    {
        life += l;
    }
    public int GetLife()
    {
        return life;
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
}
