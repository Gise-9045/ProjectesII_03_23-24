using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Tilemaps;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    public Animator animator;

    [SerializeField] private PlayerController controller;
    [SerializeField] private PlayerJump controllerJump;
    [SerializeField] private PlayerAttack controllerAttack;

    [SerializeField] private GameObject model;

    private bool facingRight; 

    // Start is called before the first frame update
    void Awake()
    {
        controller = GetComponent<PlayerController>();
        controllerJump = GetComponent<PlayerJump>();
    }

    void FixedUpdate()
    {
        animator.SetBool("Moving", controller.isMoving);

        if (controller.movementInput.x < 0 && facingRight)
        {
            Flip();
        }
        else if (controller.movementInput.x > 0 && !facingRight)
        {
            Flip();
        }

        animator.SetBool("Attack", controllerAttack.isAttacking == 1.0f);
    }

    private void Flip ()
    {
        Vector2 currentScale = model.transform.localScale;
        currentScale.x *= -1; 

        model.transform.localScale = currentScale;

        facingRight = !facingRight; 
    }
}
