using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonDetection : MonoBehaviour
{
    BoxCollider2D col;

    private bool buttonPush;

    void Start()
    {
        buttonPush = false;
        col = GetComponent<BoxCollider2D>();    
    }

    public bool GetButtonPush()
    {
        return buttonPush;
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player" || other.tag == "PuzzleBox")
        {
            buttonPush = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "Player" || other.tag == "PuzzleBox")
        {
            buttonPush = false;
        }
    }
}
