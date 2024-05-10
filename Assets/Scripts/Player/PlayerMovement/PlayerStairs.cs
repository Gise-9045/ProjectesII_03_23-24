using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStairs : MonoBehaviour
{
    bool onStairs;
    private InputController controller;
    private AudioManager audioManager;
    [SerializeField] private float climbSoundDelay;
    private float actualClimbSoundDelay;
    private Transform tr;
    private Vector2 stairsPos;
    private Rigidbody2D rb;
    private bool usingStairs;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        controller = GetComponent<InputController>();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        tr = GetComponentInChildren<Transform>();
        onStairs = false;
        
    }


    public void Stairs()
    {
        //ARREGLAR
        if (actualClimbSoundDelay < 0 && onStairs && (controller.GetMovement().y < 0 || controller.GetMovement().y > 0))
        {
            audioManager.PlaySFX(audioManager.stairsClimb);
            actualClimbSoundDelay = climbSoundDelay;
        }
        else
        {
            actualClimbSoundDelay -= Time.deltaTime;
        }


        if ((controller.GetMovement().y < 0  || controller.GetMovement().y > 0) && controller.GetMovement().x == 0)
        {
            tr.position = new Vector2((float)(tr.position.x + 0.05 * (stairsPos.x - tr.position.x)), tr.position.y);
        }

        if (controller.GetMovement().y > 0)
        {
            rb.gravityScale = 0f;
            rb.velocity = new Vector2(rb.velocity.x, 5);
        }
        else if(controller.GetMovement().y < 0)
        {
            rb.gravityScale = 0f;
            rb.velocity = new Vector2(rb.velocity.x, -5);
        }
        else
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
        }
    }

    public bool GetOnStairs()
    {
        return onStairs;
    }

    public bool GetUsingStairs()
    {
        return usingStairs;
    }


    public void SetUsingStairs(bool set)
    {
        usingStairs = set;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Ladder")
        {
            onStairs = true;

            stairsPos = collision.transform.position;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Ladder")
        {
            audioManager.StopSFX(audioManager.stairsClimb);
            onStairs = false;
            usingStairs = false;
        }
    }
}
