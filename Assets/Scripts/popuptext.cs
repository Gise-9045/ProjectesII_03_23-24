using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class popuptext : MonoBehaviour
{
    [SerializeField] GameObject dialogue;
    [SerializeField] GameObject textBubble;
   
    void Start()
    {
        dialogue.SetActive(false);
        textBubble.SetActive(false);

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            dialogue.SetActive(true);
            textBubble.SetActive(true);
        }
    }

    
}
