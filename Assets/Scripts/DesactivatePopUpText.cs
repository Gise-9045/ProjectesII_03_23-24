using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesactivatePopUpText : MonoBehaviour
{
    [SerializeField] GameObject dialogue;
    [SerializeField] GameObject textBubble;

    [SerializeField] GameObject activationText;

   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            dialogue.SetActive(false);
            textBubble.SetActive(false);
            activationText.SetActive(false);
            this.gameObject.SetActive(false);
        }
    }
}
