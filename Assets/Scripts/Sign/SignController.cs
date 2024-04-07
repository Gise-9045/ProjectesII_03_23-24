using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignController : MonoBehaviour
{
    private Collider2D col;
    private InputController controller;

    [SerializeField] private GameObject interactionArrow;
    [SerializeField] private GameObject text;

    private bool isTextActive;

    void Start()
    {
        col = GetComponent<Collider2D>();
        controller = GameObject.FindWithTag("Player").GetComponent<InputController>();
        isTextActive = false;
        text.SetActive(false);
    }

    void Update()
    {
        if(controller.GetPowerUpKey() && interactionArrow.activeSelf && !isTextActive)
        {
            text.SetActive(true);
            isTextActive = true;
            Time.timeScale = 0.0f;
        }
        else if(controller.GetPowerUpKey() && isTextActive)
        {
            text.SetActive(false);
            isTextActive = false;
            Time.timeScale = 1.0f;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            interactionArrow.SetActive(true);
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            interactionArrow.SetActive(false);
        }
    }
}
