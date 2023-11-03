using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    int health = 3;
    int maxHealh = 3;

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
