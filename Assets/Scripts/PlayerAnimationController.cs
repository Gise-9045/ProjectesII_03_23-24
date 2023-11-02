using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    public Animator animator;

    [SerializeField] private PlayerController controller;
    [SerializeField] private PlayerJump controllerJump;

    //[SerializeField] private GameObject model;

    // Start is called before the first frame update
    void Awake()
    {
        controller = GetComponent<PlayerController>();
        controllerJump = GetComponent<PlayerJump>();
    }
    // Update is called once per frame
    void FixedUpdate()
    {
    
        if (controller.isMoving == false)
        {
            animator.SetBool("Moving", false);
        }
        else if (controller.isMoving == true)
        {

            Debug.Log("is True , moving "); 
            animator.SetBool("Moving", true);

           // Flip();

        }
    }

    //private void Flip ()
    //{
    //    if (controller.movementInput.x < 0)
    //    {
    //        model.transform.localScale = new Vector2(1, 1);
    //    }
    //    else if (controller.movementInput.x > 0)
    //    {
    //        model.transform.localScale = new Vector2(-1, 1);
    //    }
    //}
}
