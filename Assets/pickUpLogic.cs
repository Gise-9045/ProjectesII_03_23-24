using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickUpLogic : MonoBehaviour
{
    private PlayerMovement colorCheck;
    private GameObject player;
    private bool amPicked;
    bool beingCarried;
    private float storedMass;
    private Vector2 offset;

    void Start()
    {
        colorCheck = FindObjectOfType<PlayerMovement>();
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            if (colorCheck.canPickUp && colorCheck.controller.GetPowerUpKey())
            {
                if (!amPicked)
                {
                    PickUp();
                }
                else
                {
                    Drop();
                }
            }
        }
    }

    void Update()
    {
        if (amPicked)
        {
            // Solo actualizar la posici�n mientras se lleva el objeto
            transform.position = new Vector2(player.transform.position.x,player.transform.position.y+1f);
        }
        else
        {
            Drop();
        }
    }

    private void PickUp()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        storedMass = GetComponent<Rigidbody2D>().mass;
        GetComponent<Rigidbody2D>().mass = 0f;
        GetComponent<Collider2D>().isTrigger = true;
        amPicked = false;
        beingCarried = true;
        offset = transform.position - player.transform.position;
    }

    private void Drop()
    {
        if (beingCarried && colorCheck.controller.GetPowerUpKey()) // Solo suelta si se presiona el bot�n de soltar
        {
            transform.position = new Vector2(transform.position.x + 1.25f * transform.localScale.x, transform.position.y);
            player = null;
            amPicked = false;
            GetComponent<Collider2D>().isTrigger = false;
            GetComponent<Rigidbody2D>().mass = storedMass;
            storedMass = 0;
        }
    }
}
