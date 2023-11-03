using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    private int health = 3;
    private int maxHealh = 3;

    public int GetHealth()
    {
        return health;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        Debug.Log(health);

        if (health < 0)
        {
            Debug.Log("MUERTO");
            health = 0;
        }
    }
}
