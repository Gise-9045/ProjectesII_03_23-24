using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetection : MonoBehaviour
{
    private bool isDetectingPlayer;

    private void Start()
    {
        isDetectingPlayer = false;
    }

    public bool GetPlayerDetection()
    {
        return isDetectingPlayer;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            isDetectingPlayer = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            isDetectingPlayer = false;

        }
    }
}
