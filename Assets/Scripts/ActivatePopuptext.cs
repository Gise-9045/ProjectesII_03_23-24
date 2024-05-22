using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ActivatePopuptext : MonoBehaviour
{
    [SerializeField] GameObject dialogue;
    [SerializeField] GameObject textBubble;
   
    void Awake()
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
