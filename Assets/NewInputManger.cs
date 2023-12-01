using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class NewInputManger : MonoBehaviour
{
    
    public static NewInputManger _newInputManager;

    [Header("Ingame Actions")]
    public InputActionReference _playerMoveInput;
    public InputActionReference _playerJumpInput;
    public InputActionReference _playerDashInput;   
    public InputActionReference _playerAttackInput;
    public InputActionReference _playerLaserInput;
    public InputActionReference _playerInteractInput;

    [Header("Menu Actions")]
    public InputActionReference _playerPauseInput;

    private void Awake()
    {
        if (_newInputManager != null)
        {
            if (_newInputManager != this)
            {
                Destroy(_newInputManager.gameObject);
            }
        }
        _newInputManager = this;
    }
}
