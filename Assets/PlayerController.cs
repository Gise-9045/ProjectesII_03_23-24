using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{

    public enum PlayerStates { NONE, MOVING, DASH , ATTACK, AIR, DEAD};
    public PlayerStates playerState;

    private PlayerInput playerInput; 
    private PlayerMovementController _movementController;
    private PlayerRespawn playerRespawn;
    private PlayerDashController playerDashController;
    private PlayerInteractionController playerIntereactionController;
    private PlayerAttackController playerAttackControlller; 

    public PlayerInput _playerInput => playerInput;
    public PlayerMovementController _movementController => movementController;
    public PlayerInteractionController _interactionController => interactionController;
    public PlayerDashController _playerDashController => dashController;
    public PlayerRespawn _playerRespawn => playerRespawn;
    public PlayerAttackController _playerAttackController => playerAttackController;


    private Animator anim;

    public Rigidbody rb { get; private set; };

    [SerializeField] public bool _canDash { get; private set; };


    private void Awake()
    {
        _canDash = false;
        AllGetComponents(); 
    }
    private void AllGetComponents()
    {
        playerInput = GetComponent<PlayerInput>();
        _movementController = GetComponent<PlayerMovementController>();
        playerRespawn = GetComponent<PlayerRespawn>();
        anim = GetComponent<Animator>();
        playerDashController = GetComponent<PlayerDashController>();
        playerIntereactionController = GetComponent<PlayerInteractionController>();
        playerAttackControlller = GetComponent<PlayerAttackController>();   
        rb = GetComponent<Rigidbody>();
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
                _movementController.CheckGrounded();
                CheckMovementStates();
                _movementController.FloorMovement();
                _movementController.CheckJumpin();
                _movementController.CheckSlope();
                _movementController.ApplyForces();
                break; 
            case PlayerStates.AIR:
                _movementController.CheckGrounded();
                CheckMovementStates();
                _movementController.AirMovement();
                _movementController.CheckJumpin();
                _movementController.CheckSlope();
                _movementController.ApplyForces();
                break;
            case PlayerStates.DASH:
                playerDashController.Dash(); 
                playerDashController.DashTimer();
                _movementController.CheckGrounded();
                break; 
            case PlayerStates.DEAD:
                playerRespawn.Respawn();
                break;       
            case PlayerStates.ATTACK:
                playerAttackControlller.Attack(); 
                break; 

        }
    }

    private void AnimateCharacter()
    {
        if (playerState == PlayerStates.MOVING)
        {
            anim.SetBool("isMoving", true);
            anim.SetBool("isJumping", false);
        }
        if (playerState == PlayerStates.ATTACK)
        {
            anim.SetBool("isAttacking", true);
            anim.SetBool("isMoving", false);
            anim.SetBool("isJumping", false);
        }
        if (playerState == PlayerStates.NONE)
        {
            anim.SetBool("isMoving", false);
            anim.SetBool("isJumping", false);
            anim.SetBool("isAttacking", false);
        }
        if (playerState == PlayerStates.AIR)
        {
            anim.SetBool("isJumping", true);
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
                playerRespawn.Die();
                break;
            default:
                break;
        }

        playerState = _nextState;
    }
}
