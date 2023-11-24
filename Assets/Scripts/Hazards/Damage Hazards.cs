using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class DamageHazards : MonoBehaviour
{
    private GameObject player;
    private Tilemap tileMap;

    private void Start()
    {
        tileMap = GetComponent<Tilemap>();
        player = GameObject.FindWithTag("Player");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector3Int cellPosition = tileMap.WorldToCell(transform.position);
        tileMap.GetTile(cellPosition);

        Debug.Log(tileMap.GetTile(cellPosition));

        if (collision.collider.tag == "Player")
        {
            player.GetComponent<PlayerStats>().TakeDamage(1, 20.0f, Mathf.Sign(tileMap.transform.position.x - gameObject.transform.position.x));
        }
    }
}
