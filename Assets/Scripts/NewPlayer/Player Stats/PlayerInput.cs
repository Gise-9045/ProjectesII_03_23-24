using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{

    [SerializeField] private PlayerController _playerController;

    private void Start()
    {
        NewInputManger._newInputManager._playerMoveInput.action.performed += MoveAction;
        NewInputManger._newInputManager._playerMoveInput.action.canceled += MoveAction;

        // te hará falta saber cuando empieza y cuando acaba para el salto largo
        NewInputManger._newInputManager._playerJumpInput.action.performed += JumpAction;   

        NewInputManger._newInputManager._playerAttackInput.action.started += AttackAction;

    }

    private void OnDestroy()
    {
        NewInputManger._newInputManager._playerMoveInput.action.performed -= MoveAction;
        NewInputManger._newInputManager._playerMoveInput.action.canceled -= MoveAction;
        NewInputManger._newInputManager._playerJumpInput.action.performed -= JumpAction;

        NewInputManger._newInputManager._playerAttackInput.action.started -= AttackAction;

    }

    #region Actions

    private void MoveAction(InputAction.CallbackContext context)
    {
        _playerController.SetPlayerMoveDirection(context.ReadValue<Vector2>());
        
    }

    private void JumpAction(InputAction.CallbackContext context)
    {
        Debug.Log("WAAAAAAAAAAAAA");
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
