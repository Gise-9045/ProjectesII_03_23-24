using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
    //public static Action OnJump;
    //public static Action<Vector2> OnMove;

    //private void Start()
    //{
    //    InputManager.Instance.jumpActionReference.action.performed += Jump;
    //    InputManager.Instance.moveActionReference.action.performed += Move;
    //}

    //private void OnDestroy()
    //{
    //    InputManager.Instance.jumpActionReference.action.performed -= Jump;
    //    InputManager.Instance.moveActionReference.action.performed -= Move;
    //}

    //private void Jump(InputAction.CallbackContext callbackContext)
    //{
    //    if (callbackContext.performed)
    //    {
    //        OnJump?.Invoke();
    //    }
    //}

    //private void Move(InputAction.CallbackContext callbackContext)
    //{
    //    if (callbackContext.performed)
    //    {
    //        //OnMove?.Invoke(callbackContext.);
    //    }
    //}


    [SerializeField]
    private InputActionReference movement, jump;

    private GameObject player;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    private void Update()
    {
        //if((int)movement.action.ReadValue<Vector2>().x != 0)
        //{
        //    player.GetComponent<Player>().SetDirection((int)movement.action.ReadValue<Vector2>().x);
        //}
        //Debug.Log(movement.action.ReadValue<Vector2>());
    }


}
