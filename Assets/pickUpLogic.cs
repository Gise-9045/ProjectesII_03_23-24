using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class pickUpLogic : MonoBehaviour
{
    private ColorReceiver colorCheck;
    private GameObject player;
    private bool amPicked;
    private float storedMass;
    // Start is called before the first frame update
    void Start()
    {
        colorCheck = FindObjectOfType<ColorReceiver>();  
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player")){
            player = GameObject.FindGameObjectWithTag("Player");
            storedMass = GetComponent<Rigidbody2D>().mass;
            GetComponent<Rigidbody2D>().mass = 0f;
            GetComponent<Collider2D>().isTrigger = true;
            amPicked = true;
            PickUp(player, amPicked);
        }
    }
    // Update is called once per frame
    void Update()
    {
        PickUp(player, amPicked);
    }
    private void PickUp(GameObject player, bool isCarrying)
    {
        if (isCarrying )
        {
            transform.position = new Vector2(player.transform.position.x, player.transform.position.y + 1);
        }
        else if (player != null && !isCarrying)
        {
            transform.position = new Vector2(transform.position.x + 1.25f * transform.localScale.x, transform.position.y);
            player = null;
            GetComponent<Collider2D>().isTrigger = false;
            GetComponent<Rigidbody2D>().mass = storedMass;
            storedMass = 0;

        }
    }
}
