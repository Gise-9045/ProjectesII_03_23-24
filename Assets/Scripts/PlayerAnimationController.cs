using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    public Animator animator;

    [SerializeField] private PlayerController controller;
    [SerializeField] private PlayerJump controllerJump;

    [SerializeField] private GameObject model;

    private bool facingRight; 

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

        }
        if (controller.movementInput.x < 0 && facingRight)
        {
            Flip();
        }
        else if (controller.movementInput.x > 0 && !facingRight)
        {
            Flip();
        }
    }

    private void Flip ()
    {
        Vector2 currentScale = model.transform.localScale;
        currentScale.x *= -1; 

        model.transform.localScale = currentScale;

        facingRight = !facingRight; 
    }
}
