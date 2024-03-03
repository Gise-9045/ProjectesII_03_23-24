using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Feedback_Key : MonoBehaviour
{
    [SerializeField] private Animator animKey;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") && collision.GetComponent<KeySaver>().keys.Count == 0)
        {
                animKey.SetBool("TouchDoor", true);  
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            animKey.SetBool("TouchDoor", false);
        }
    }
}
