using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
    public PlayerInput playerInput;

    Vector2 movementController;
    bool jumpKeyTap = false;
    bool jumpKeyHold = false;
    bool powerUpKey = false;
    bool pauseKey = false;
    bool colorsMenuKey = false;

    private void Awake()
    {
        playerInput.actions["Player/JumpTap"].started += OnStartJumpIA;
        playerInput.actions["Player/JumpTap"].canceled += OnStopJumpIA;

        playerInput.actions["Player/JumpHold"].started += OnStartJumpHoldIA;
        playerInput.actions["Player/JumpHold"].canceled += OnStopJumpHoldIA;

        playerInput.actions["Player/PowerUp"].performed += OnPowerUpAI;
        playerInput.actions["Player/PowerUp"].canceled += OnStopPowerUpAI;


        playerInput.actions["Player/Pause"].performed += OnPauseAI;
        playerInput.actions["Player/Pause"].canceled += OnStopPauseAI;

        playerInput.actions["Player/ColorsMenu"].performed += OnColorsAI;
        playerInput.actions["Player/ColorsMenu"].canceled += OnStopColorsAI;
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

    public bool GetColorsMenu()
    {
        return colorsMenuKey;
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

    private void OnColorsAI(InputAction.CallbackContext context)
    {
        colorsMenuKey = true;
    }

    private void OnStopColorsAI(InputAction.CallbackContext context)
    {
        colorsMenuKey = false;
    }

    private void FixedUpdate()
    {
        movementController = playerInput.actions["Player/Move"].ReadValue<Vector2>();

        if (jumpKeyTap)
        {
            jumpKeyTap = false;
        }

        if (colorsMenuKey)
        {
            colorsMenuKey = false;
        }
    }
}
