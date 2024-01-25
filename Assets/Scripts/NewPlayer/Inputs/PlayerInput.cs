using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{

    //[SerializeField] private PlayerController _playerController;

    public bool canMove;
    public bool canJump;

    public Rigidbody2D rb;

    public float moveSpeed;
    public float jumpPower;

    public Transform platformCheck;
    public float platformCheckRadius;
    public LayerMask whatIsPlatform;
    private bool platform;

    private void Start()
    {
        InputManger._newInputManager._playerMoveInput.action.started += MoveAction;
        InputManger._newInputManager._playerMoveInput.action.canceled += CanceledMoveAction;

        // te hará falta saber cuando empieza y cuando acaba para el salto largo
        //NewInputManger._newInputManager._playerJumpInput.action.performed += JumpAction;   
        InputManger._newInputManager._playerJumpInput.action.started += JumpAction;   

        InputManger._newInputManager._playerAttackInput.action.started += AttackAction;

        InputManger._newInputManager._playerInteractInput.action.started += InteractingAction;

        InputManger._newInputManager._playerGodModeInput.action.started += GodModeAction; 

        InputManger._newInputManager._playerDashInput.action.started += DashAction;


        canMove = true;
        canJump = true;

        rb = GetComponent<Rigidbody2D>();   

    }

    private void OnDestroy()
    {
        InputManger._newInputManager._playerMoveInput.action.started -= MoveAction;
        InputManger._newInputManager._playerMoveInput.action.canceled -= CanceledMoveAction;
        
        //NewInputManger._newInputManager._playerJumpInput.action.performed -= JumpAction;
        InputManger._newInputManager._playerJumpInput.action.started -= JumpAction;

        InputManger._newInputManager._playerAttackInput.action.started -= AttackAction;

        InputManger._newInputManager._playerInteractInput.action.started -= InteractingAction;

        InputManger._newInputManager._playerGodModeInput.action.started -= GodModeAction;

        InputManger._newInputManager._playerDashInput.action.started -= DashAction;
    }

    private void Update()
    {
        platform = Physics2D.OverlapCircle(platformCheck.position, platformCheckRadius, whatIsPlatform);

        if (GetComponent<Rigidbody2D>().velocity.x > 0)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
        else if (GetComponent<Rigidbody2D>().velocity.x < 0)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }

        if (Input.GetKey(KeyCode.D) && platform)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(moveSpeed, 0);
            Debug.Log("D");
        }
        if (Input.GetKey(KeyCode.A) && platform)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(-moveSpeed, 0);
        }

    }

    /*#region Actions

    //private void MoveAction(InputAction.CallbackContext context)
    //{
    //    if(!canMove) return;

    //    _playerController.SetPlayerMoveDirection(context.ReadValue<Vector2>());    
    //}

    //private void CanceledMoveAction(InputAction.CallbackContext context)
    //{
    //    _playerController.SetPlayerMoveDirection(Vector2.zero);
    //}

    //private void JumpAction(InputAction.CallbackContext context)
    //{
    //    if(!canJump) return;

    //    //switch (playerController.playerState)
    //    //{
    //    //    case PlayerController.PlayerStates.WALL_SLIDE:
    //    //        playerController._wallJumpController.WallJump();
    //    //        break;

    //    //    case PlayerController.PlayerStates.NONE:
    //    //    case PlayerController.PlayerStates.MOVING:
    //    //        playerController._movementController.JumpInputPressed();
    //    //        break;
    //    //    case PlayerController.PlayerStates.AIR:
    //    //        playerController._wallJumpController.CheckWallJumpInAir();
    //    //        break;
    //    //}

    //    switch(_playerController.playerStats.hasJumpPowerUp)
    //    {
    //        case false:
    //            _playerController.playerJumpController.Jump_player();
    //            break;
    //        case true:
    //            _playerController.playerJumpController.Jump_player_PowerUp();
    //            break;
    //    }
    //}

    //private void StopJump()
    //{
    //    //playerController._movementController.JumpInputUnPressed();
    //}

    //private void DashAction(InputAction.CallbackContext context)
    //{
    //    //if (playerController._playerDashController._canDash && playerController._canDash)
    //    //{
    //    //    switch (playerController.playerState)
    //    //    {
    //    //        case PlayerController.PlayerStates.NONE:
    //    //        case PlayerController.PlayerStates.MOVING:
    //    //        case PlayerController.PlayerStates.AIR:
    //    //            playerController._playerDashController.StartDash(InputManager._instance.ingameAirDirectionAction.action.ReadValue<Vector2>());
    //    //            break;
    //    //    }
    //    //}
    //    if(!_playerController.playerStats.hasDashPowerUp)
    //    {
    //        return; 
    //    }
    //    canMove = false;
    //    canJump = false;

    //    _playerController.playerDashController.PlayerDashing();

    //    canMove = true;
    //    canJump = true;

    //}

    //private void InteractingAction(InputAction.CallbackContext context)
    //{
    //    Debug.Log("INTERACTIOM");
    //    _playerController.playerInteractionController.Interact();
    //}

    //private void AttackAction(InputAction.CallbackContext context)
    //{

    //    _playerController.playerAttackControlller.Attack(); 
    //}

    //private void GodModeAction(InputAction.CallbackContext context)
    //{
    //    _playerController.playerStats.GodModeActivated();
    //    Debug.Log("GOD MODE ACTIVE"); 
    //}

    //#endregion Actions*/

    #region NEW ACTIONS

    void MoveAction(InputAction.CallbackContext context)
    {
       
    }

    void CanceledMoveAction(InputAction.CallbackContext context)
    {

    }

    void JumpAction(InputAction.CallbackContext context)
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, jumpPower);
    }

    void DashAction(InputAction.CallbackContext context)
    {

    }   

    void InteractingAction(InputAction.CallbackContext context)
    {

    }

    void AttackAction(InputAction.CallbackContext context)
    {

    }   

    void GodModeAction(InputAction.CallbackContext context)
    {

    }   
    #endregion NEW ACTIONS
}
