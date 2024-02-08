using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeySaver : MonoBehaviour
{
    public List<Transform> keys = new List<Transform>(); // Lista para almacenar las llaves
    public Transform player;
    public float distanceBetweenKeys = 2.0f;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Key"))
        {
            collision.transform.localScale = new Vector2(0.5f, 0.5f);
            keys.Add(collision.transform);
            
            //collision.gameObject.SetActive(false); // Desactivamos la llave en la escena
        }
    }

    void Update()
    {
        // Movemos las llaves hacia el jugador con un espacio de 2 unidades entre ellas
        Vector2 nextPosition = new Vector2(player.position.x - 1f, player.position.y) ;
        foreach (Transform key in keys)
        {
            key.position = nextPosition;
            nextPosition.x += distanceBetweenKeys;
        }
    }
}