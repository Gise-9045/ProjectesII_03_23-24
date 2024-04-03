using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
    public PlayerInput playerInput;

    Vector2 movementController;
    bool jumpKeyTap;
    bool jumpKeyHold;
    bool powerUpKey;
    bool pauseKey;

    void Start()
    {
        playerInput.actions["Player/JumpTap"].started += OnStartJumpIA;
        playerInput.actions["Player/JumpTap"].canceled += OnStopJumpIA;

        playerInput.actions["Player/JumpHold"].started += OnStartJumpHoldIA;
        playerInput.actions["Player/JumpHold"].canceled += OnStopJumpHoldIA;

        playerInput.actions["Player/PowerUp"].performed += OnPowerUpAI;
        playerInput.actions["Player/PowerUp"].canceled += OnStopPowerUpAI;


        playerInput.actions["Player/Pause"].performed += OnPauseAI;
        playerInput.actions["Player/Pause"].canceled += OnStopPauseAI;
    }

    public bool GetJumpKeyTap()
    {
        return jumpKeyTap;
    }

    public bool GetJumpkeyHold()
    {
        return jumpKeyHold;
    }

    public bool GetPowerUpKey()
    {
        return powerUpKey;
    }

    public Vector2 GetMovement()
    {
        return movementController;
    }

    public bool GetPause()
    {
        return pauseKey;
    }

    ///////----------///////

    public void SetJumpKeyTap(bool tap)
    {
        jumpKeyTap = tap;
    }

    public void SetJumpkeyHold(bool hold)
    {
        jumpKeyHold = hold;
    }




    private void OnStartJumpIA(InputAction.CallbackContext context)
    {
        jumpKeyTap = true;
    }
    private void OnStopJumpIA(InputAction.CallbackContext context)
    {
        jumpKeyTap = false;
    }

    private void OnStartJumpHoldIA(InputAction.CallbackContext context)
    {
        jumpKeyHold = true;
    }
    private void OnStopJumpHoldIA(InputAction.CallbackContext context)
    {
        jumpKeyHold = false;
    }

    private void OnPowerUpAI(InputAction.CallbackContext context)
    {
        powerUpKey = true;
    }
    private void OnStopPowerUpAI(InputAction.CallbackContext context)
    {
        powerUpKey = false;
    }

    private void OnPauseAI(InputAction.CallbackContext context)
    {
        pauseKey = true;
    }

    private void OnStopPauseAI(InputAction.CallbackContext context)
    {
        pauseKey = false;
    }


    void Update()
    {
        movementController = playerInput.actions["Player/Move"].ReadValue<Vector2>();
        jumpKeyTap = false;
        pauseKey = false;

    }
}
