using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverLogic : MonoBehaviour
{
    private InputController controller;
    private Animator anim;

    [SerializeField] private bool isActivated;
    private bool isTouching;


    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        isActivated = false;
        controller = GameObject.FindWithTag("Player").GetComponent<InputController>();
    }

    void Update()
    {
        if(isTouching)
        {
            if(controller.GetPowerUpKey() && isActivated)
            {
                isActivated = false;
                anim.SetBool("isActivate", false);
            }
            else if(controller.GetPowerUpKey() && !isActivated)
            {
                isActivated = true;
                anim.SetBool("isActivate", true);
            }
        }
    }

    public bool GetIsEnabled()
    {
        return isActivated;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            isTouching = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            isTouching = false;
        }
    }
}
