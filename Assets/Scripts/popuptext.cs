using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class popuptext : MonoBehaviour
{
    [SerializeField] GameObject dialogue;
    [SerializeField] GameObject textBubble;
    // Start is called before the first frame update
    void Start()
    {
        dialogue.active = false;
        textBubble.active = false;

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            dialogue.active = true;
            textBubble.active = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
