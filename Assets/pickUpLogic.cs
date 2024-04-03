using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickUpLogic : MonoBehaviour
{
    private PlayerMovement colorCheck;
    private GameObject player;
    private bool amPicked;
    private float storedMass;
    private Vector2 offset;

    void Start()
    {
        colorCheck = FindObjectOfType<PlayerMovement>();
        //offset = transform.position - player.transform.position;
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
            transform.position = (Vector2)player.transform.position + offset;
            if (colorCheck.controller.GetPowerUpKey())
            {
                Drop();
            }
        }
    }

    private void PickUp()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        storedMass = GetComponent<Rigidbody2D>().mass;
        GetComponent<Rigidbody2D>().mass = 0f;
        GetComponent<Collider2D>().isTrigger = true;
        amPicked = true;
        offset = transform.position - player.transform.position;
    }

    private void Drop()
    {
        transform.position = new Vector2(transform.position.x + 1.25f * transform.localScale.x, transform.position.y);
        player = null;
        amPicked = false;
        GetComponent<Collider2D>().isTrigger = false;
        GetComponent<Rigidbody2D>().mass = storedMass;
        storedMass = 0;
    }
}
