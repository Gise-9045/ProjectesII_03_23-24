using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
    public static Action OnJump;
    public static Action<Vector2> OnMove;

    private void Start()
    {
        InputManager.Instance.jumpActionReference.action.performed += Jump;
        InputManager.Instance.moveActionReference.action.performed += Move;
    }

    private void OnDestroy()
    {
        InputManager.Instance.jumpActionReference.action.performed -= Jump;
        InputManager.Instance.moveActionReference.action.performed -= Move;
    }

    private void Jump(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.performed)
        {
            OnJump?.Invoke();
        }
    }

    private void Move(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.performed)
        {
            //OnMove?.Invoke(callbackContext.);
        }
    }
}
