using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{

    [SerializeField] private PlayerController _playerController;

    public bool canMove;
    public bool canJump; 

    private void Start()
    {
        NewInputManger._newInputManager._playerMoveInput.action.started += MoveAction;
        NewInputManger._newInputManager._playerMoveInput.action.canceled += CanceledMoveAction;

        // te har� falta saber cuando empieza y cuando acaba para el salto largo
        //NewInputManger._newInputManager._playerJumpInput.action.performed += JumpAction;   
        NewInputManger._newInputManager._playerJumpInput.action.started += JumpAction;   

        NewInputManger._newInputManager._playerAttackInput.action.started += AttackAction;

        NewInputManger._newInputManager._playerInteractInput.action.started += InteractingAction;

        NewInputManger._newInputManager._playerGodModeInput.action.started += GodModeAction; 

        NewInputManger._newInputManager._playerDashInput.action.started += DashAction;


        canMove = true;
        canJump = true;

    }

    private void OnDestroy()
    {
        NewInputManger._newInputManager._playerMoveInput.action.started -= MoveAction;
        NewInputManger._newInputManager._playerMoveInput.action.canceled -= CanceledMoveAction;
        
        //NewInputManger._newInputManager._playerJumpInput.action.performed -= JumpAction;
        NewInputManger._newInputManager._playerJumpInput.action.started -= JumpAction;

        NewInputManger._newInputManager._playerAttackInput.action.started -= AttackAction;

        NewInputManger._newInputManager._playerInteractInput.action.started -= InteractingAction;

        NewInputManger._newInputManager._playerGodModeInput.action.started -= GodModeAction;

        NewInputManger._newInputManager._playerDashInput.action.started -= DashAction;
    }

    #region Actions

    private void MoveAction(InputAction.CallbackContext context)
    {
        if(!canMove) return;

        _playerController.SetPlayerMoveDirection(context.ReadValue<Vector2>());    
    }

    private void CanceledMoveAction(InputAction.CallbackContext context)
    {
        _playerController.SetPlayerMoveDirection(Vector2.zero);
    }

    private void JumpAction(InputAction.CallbackContext context)
    {
        if(!canJump) return;

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

        switch(_playerController.playerStats.hasJumpPowerUp)
        {
            case false:
                _playerController.playerJumpController.Jump_player();
                break;
            case true:
                _playerController.playerJumpController.Jump_player_PowerUp();
                break;
        }
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
        if(!_playerController.playerStats.hasDashPowerUp)
        {
            return; 
        }
        canMove = false;
        canJump = false;

        _playerController.playerDashController.PlayerDashing();

        canMove = true;
        canJump = true;

    }

    private void InteractingAction(InputAction.CallbackContext context)
    {
        Debug.Log("INTERACTIOM");
        _playerController.playerInteractionController.Interact();
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
