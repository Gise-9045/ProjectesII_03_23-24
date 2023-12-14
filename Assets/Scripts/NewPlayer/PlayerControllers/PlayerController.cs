using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public enum PlayerStates { NONE, MOVING, DASH , ATTACK, AIR, DEAD};
    [SerializeField] private PlayerStates currentState;

    [HideInInspector]
    public PlayerMovementController playerMovementController;
    //public PlayerDashController playerDashController; 
    public PlayerJumpController playerJumpController;
    public PlayerAttackController playerAttackControlller;
    public PlayerStats playerStats; 
    //public PlayerRespawn playerRespawn;

    public PlayerInput playerInput; 

    private Animator anim;
    private Rigidbody2D rb2d;

    public bool canMove;
    public bool canJump;

    private float deathCooldownTime = 0.5f;
    public float deathEndTime = 0;

    //[SerializeField] public bool _canDash { get; private set; }


    private void Awake()
    {
        //_canDash = false;
        AllGetComponents(); 
    }
    private void AllGetComponents()
    {
        playerMovementController = GetComponent<PlayerMovementController>();
        //playerRespawn = GetComponent<PlayerRespawn>();
        //playerStats = GetComponent<PlayerStats>();
        //playerDashController = GetComponent<PlayerDashController>();
        playerJumpController = GetComponent<PlayerJumpController>();
        playerAttackControlller = GetComponent<PlayerAttackController>();

        playerInput = GetComponent<PlayerInput>();

        anim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();

    }
    
    void FixedUpdate()
    {
        StatesFunction();
        AnimateCharacter(); 
    }

    public void SetPlayerMoveDirection(Vector2 direction)
    {
        playerMovementController.SetDirection(direction);
    }

    private void StatesFunction() //LA FUNCION DE UTILIZA PARA CHEQUEAR COLISIONES Y ACTUALIZAR BOOLEANOS
    {
        switch (currentState)
        {
            case PlayerStates.NONE:
            case PlayerStates.MOVING:
                playerJumpController.UpdateGroundCheck(); 
                CheckMovementStates();
                //movementController.FloorMovement();
                //movementController.CheckJumping();
                //movementController.CheckSlope();
                //movementController.ApplyForces();
                break;
            case PlayerStates.AIR:
                playerJumpController.UpdateGroundCheck();
                CheckMovementStates();
                //movementController.AirMovement();
                //movementController.CheckJumping();
                //movementController.CheckSlope();
                //movementController.ApplyForces();
                break;
            case PlayerStates.DASH:
                //playerDashController.Dash(); 
                //playerDashController.DashTimer();
                // movementController.CheckGrounded();
                break;
            case PlayerStates.DEAD:

                playerStats.Die();

                //deathEndTime = Time.fixedDeltaTime + deathCooldownTime;

                //if(Time.fixedDeltaTime <= deathEndTime)
                //{
                //    ChangeState(PlayerStates.NONE);
                //} 

                ChangeState(PlayerStates.NONE);

                break;
            case PlayerStates.ATTACK:
                break;
        }
    }

    private void CheckMovementStates()
    {
        // cada frame esta modificando a falso o a verdadera la variable isJumping, no se queda en uno hasta que no detecta el suelo 

        bool isJumping = playerJumpController.lastIsOnGround;

        if (!isJumping)
        {
            //si esta en el suelo
            if (playerMovementController.currentSpeed != 0)
            {
                ChangeState(PlayerStates.MOVING);
            }
            else
            {
                ChangeState(PlayerStates.NONE);
            }

        }

        if (isJumping)
        {
            //si esta en el aire
            ChangeState(PlayerStates.AIR);
        }

        Debug.Log(isJumping + " " + currentState);
    }

    private void AnimateCharacter()
    {
        if (currentState == PlayerStates.MOVING)
        {
            anim.SetBool("isMoving", true);
            //anim.SetBool("isJumping", false);
        }
        if (currentState == PlayerStates.ATTACK)
        {
            anim.SetBool("isAttacking", true);
            //anim.SetBool("isMoving", false);
            anim.SetBool("isJumping", false);
        }
        if (currentState == PlayerStates.NONE)
        {
            anim.SetBool("isMoving", false);
            //anim.SetBool("isJumping", false);
            anim.SetBool("isAttacking", false);
        }
        if (currentState == PlayerStates.AIR)
        {
            //anim.SetBool("isJumping", true);
            anim.SetBool("isMoving", false);
            anim.SetBool("isAttacking", false);
        }
    }
    
    public void ChangeState(PlayerStates _nextState)
    {
        switch (_nextState)
        {
            case PlayerStates.NONE:
                playerInput.canMove = true;
                playerInput.canJump = true;
                break;
            case PlayerStates.MOVING:
                break;
            case PlayerStates.AIR:
                break;
            case PlayerStates.DASH:
                break;
            case PlayerStates.ATTACK:
                break;
            case PlayerStates.DEAD:
                break;
            default:
                break;
        }

        currentState = _nextState;
    }
}
