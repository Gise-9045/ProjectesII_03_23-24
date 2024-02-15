using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class KeySaver : MonoBehaviour
{
    public List<Transform> keys = new List<Transform>(); // Lista para almacenar las llaves
    public Transform player;
    public float distanceBetweenKeys = 2.0f;
    public float smoothness = 5.0f; // Suavidad del movimiento de la llave
    private Quaternion playerPreviousRotation; // Rotación anterior del jugador

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Key"))
        {
            collision.transform.localScale = new Vector2(0.7f, 0.7f);
            collision.gameObject.GetComponent<Collider2D>().enabled = false;
            keys.Add(collision.transform);

            // Gira la llave hacia el lado opuesto al que mira el jugador
        }
    }


    void Update()
    {
     

        // Movemos las llaves hacia el jugador con un espacio de 2 unidades entre ellas
        foreach (Transform key in keys)
        {
            if (player.position.x > 0)
            {
                
            }
            if (player.position.x < 0)
            {
             
            }

            Vector3 targetPosition = player.position + Vector3.right * distanceBetweenKeys * keys.IndexOf(key) + new Vector3(1,0,0);
            key.position = Vector3.Lerp(key.position, targetPosition, Time.deltaTime * smoothness);
        }
    }

    
}
