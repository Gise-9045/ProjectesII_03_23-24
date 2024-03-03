using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveBlovk : MonoBehaviour
{
    [SerializeField] private Transform respawn;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PuzzleBox"))
        {
            MoveBlovk(collision.transform);
           
        }
    }
    void MoveBlovk(Transform collision)
    {
      
        collision.position = respawn.position;
        
    }
}
