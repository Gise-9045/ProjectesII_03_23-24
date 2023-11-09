using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemyAttack : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    Quaternion rotation = Quaternion.Euler(0, 0, 0);
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            Instantiate(bullet, new Vector2(gameObject.transform.position.x + 1, gameObject.transform.position.y), rotation);
        }
    }
}
