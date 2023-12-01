using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public enum PlayerStates { NONE, MOVING, DASH , ATTACK, AIR, DEAD};
    public PlayerStates playerState;

    [HideInInspector]
    public PlayerInput playerInput;
    public PlayerMovementController playerMovementController;
    public PlayerDashController playerDashController; 
    public PlayerJumpController playerJumpController;
    //public Attack playerAttackControlller;
    public PlayerStats playerStats; 
    public PlayerRespawn playerRespawn;

    private Animator anim;

    public Rigidbody2D rb2d; /* {get; private set; }*/

    [SerializeField] public bool _canDash { get; private set; }


    private void Awake()
    {
        _canDash = false;
        AllGetComponents(); 
    }
    private void AllGetComponents()
    {
        playerInput = GetComponent<PlayerInput>();
        playerRespawn = GetComponent<PlayerRespawn>();
        playerStats = GetComponent<PlayerStats>();
        playerMovementController = GetComponent<PlayerMovementController>();
        playerDashController = GetComponent<PlayerDashController>();
        playerJumpController = GetComponent<PlayerJumpController>();
        //playerAttackControlller = GetComponent<Attack>();

        anim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();

    }
    
    void Update()
    {
        StatesFunction();
        AnimateCharacter(); 
    }

    private void StatesFunction()
    {
        switch (playerState)
        {
            case PlayerStates.NONE:
            case PlayerStates.MOVING:
                //movementController.CheckGrounded();
                CheckMovementStates();
                //movementController.FloorMovement();
                //movementController.CheckJumping();
                //movementController.CheckSlope();
                //movementController.ApplyForces();
                break; 
            case PlayerStates.AIR:
                //movementController.CheckGrounded();
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
                //playerRespawn.Respawn();
                break;       
            case PlayerStates.ATTACK:
                //playerAttackControlller.Attack(); 
                break; 
        }
    }

    private void CheckMovementStates()
    {
        if (!playerJumpController.isOnGround)
        {
            //Si esta en el suelo
            if (playerInput._playerMovement != 0)
            {
                ChangeState(PlayerStates.MOVING);
            }
            else
            {
                ChangeState(PlayerStates.NONE);
            }

        }
        else if(playerJumpController.isOnGround)
        {
            //Si esta en el aire
            ChangeState(PlayerStates.AIR);
        }

        Debug.Log(playerJumpController.isOnGround | playerStats);
    }

    private void AnimateCharacter()
    {
        if (playerState == PlayerStates.MOVING)
        {
            anim.SetBool("isMoving", true);
            //anim.SetBool("isJumping", false);
        }
        if (playerState == PlayerStates.ATTACK)
        {
            anim.SetBool("isAttacking", true);
            //anim.SetBool("isMoving", false);
            anim.SetBool("isJumping", false);
        }
        if (playerState == PlayerStates.NONE)
        {
            anim.SetBool("isMoving", false);
            //anim.SetBool("isJumping", false);
            anim.SetBool("isAttacking", false);
        }
        if (playerState == PlayerStates.AIR)
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
                //playerRespawn.Die();
                break;
            default:
                break;
        }

        playerState = _nextState;
    }
}
