using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{

    private float playerMovement;
    public float _playerMovement => playerMovement;
    //Variable que recibira el valor de player Movement y
    //la haremos publica para que asi no pueda editarse desde fuera

    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
       // NewInputManger._instance._playerMoveInput.action.performed+= 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #region Actions

    private void MoveAction(InputAction.CallbackContext context)
    {
        playerMovement = NewInputManger._instance._playerMoveInput.action.ReadValue<float>();
    }
    private void JumpAction(InputAction.CallbackContext context)
    {
        //switch (playerController.playerState)
        //{
        //    case PlayerController.PlayerStates.WALL_SLIDE:
        //        playerController._wallJumpController.WallJump();
        //        break;

        //    case PlayerController.PlayerStates.NONE:
        //    case PlayerController.PlayerStates.MOVING:
        //        playerController._movementController.JumpInputPressed();
        //        break;
        //    case PlayerController.PlayerStates.AIR:
        //        playerController._wallJumpController.CheckWallJumpInAir();
        //        break;
        //}
    }

    private void StopJump()
    {
        //playerController._movementController.JumpInputUnPressed();
    }

    private void DashAction(InputAction.CallbackContext context)
    {
        //if (playerController._playerDashController._canDash && playerController._canDash)
        //{
        //    switch (playerController.playerState)
        //    {
        //        case PlayerController.PlayerStates.NONE:
        //        case PlayerController.PlayerStates.MOVING:
        //        case PlayerController.PlayerStates.AIR:
        //            playerController._playerDashController.StartDash(InputManager._instance.ingameAirDirectionAction.action.ReadValue<Vector2>());
        //            break;
        //    }
        //}
    }

    private void InteractingAction(InputAction.CallbackContext context)
    {
        // playerController._interactionController.Interact();
    }

    private void AttackAction(InputAction.CallbackContext context)
    {
        //playerController._attackController.Attack();
    }


    #endregion Actions
}
