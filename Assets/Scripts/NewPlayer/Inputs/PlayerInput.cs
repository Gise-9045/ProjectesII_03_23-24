using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{

    [SerializeField] private PlayerController _playerController;

    private void Start()
    {
        NewInputManger._newInputManager._playerMoveInput.action.started += MoveAction;
        NewInputManger._newInputManager._playerMoveInput.action.canceled += CanceledMoveAction;

        // te hará falta saber cuando empieza y cuando acaba para el salto largo
        //NewInputManger._newInputManager._playerJumpInput.action.performed += JumpAction;   
        NewInputManger._newInputManager._playerJumpInput.action.started += JumpAction;   

        NewInputManger._newInputManager._playerAttackInput.action.started += AttackAction;

        NewInputManger._newInputManager._playerInteractInput.action.started += InteractingAction;

        NewInputManger._newInputManager._playerGodModeInput.action.started += GodModeAction; 

    }

    private void OnDestroy()
    {
        NewInputManger._newInputManager._playerMoveInput.action.started -= MoveAction;
        NewInputManger._newInputManager._playerMoveInput.action.canceled -= CanceledMoveAction;
        
        //NewInputManger._newInputManager._playerJumpInput.action.performed -= JumpAction;
        NewInputManger._newInputManager._playerJumpInput.action.started -= JumpAction;

        NewInputManger._newInputManager._playerAttackInput.action.started -= AttackAction;

        NewInputManger._newInputManager._playerInteractInput.action.started -= InteractingAction;

    }

    #region Actions

    private void MoveAction(InputAction.CallbackContext context)
    {
        
        _playerController.SetPlayerMoveDirection(context.ReadValue<Vector2>());    
    }

    private void CanceledMoveAction(InputAction.CallbackContext context)
    {
        _playerController.SetPlayerMoveDirection(Vector2.zero);
    }

    private void JumpAction(InputAction.CallbackContext context)
    {
        _playerController.playerJumpController.Jump_player(0,2); 
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
        Debug.Log("INTERACTION"); 
    }

    private void AttackAction(InputAction.CallbackContext context)
    {
        _playerController.playerAttackControlller.Attack(); 
    }

    private void GodModeAction(InputAction.CallbackContext context)
    {
        _playerController.playerStats.GodModeActivated();
        Debug.Log("GOD MODE ACTIVE"); 
    }

    #endregion Actions
}
