using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem; 

public class InputManager : MonoBehaviour
{
    #region VARIABLES 

    private Collisions _collision;
    private Dash _dash;
    private Jump _jump;
    private Move _move;
    private Wall _wall;

    [Space]
    [Header("Input References")]
    [SerializeField] private InputActionReference _playerMoveInput;
    [SerializeField] private InputActionReference _playerJumpInput;
    [SerializeField] private InputActionReference _playerDashInput;

    [Space]
    [Header("Booleans")]
    [SerializeField] private bool canMove;
    // public bool wallGrab;
    [SerializeField] private bool wallJumped;
    [SerializeField] private bool wallSlide;
    [SerializeField] private bool isDashing;
    
    public int sidePlayer = 1;

    private bool _groundTouch;
    private bool _hasDashed;

    //Particulas De momento nope

    public Vector2 move; 

    #endregion VARIABLES 

    #region METODOS DEFAULT

    private void OnEnable()
    {
        _playerJumpInput.action.performed += PlayerJump;
        _playerDashInput.action.performed += PlayerDash;
        _playerMoveInput.action.started += PlayerMove;
        _playerMoveInput.action.canceled += PlayerMove;
    }

    private void OnDisable() 
    {
        _playerJumpInput.action.performed -= PlayerJump;
        _playerDashInput.action.performed -= PlayerDash;
        _playerMoveInput.action.started -= PlayerMove;
        _playerMoveInput.action.canceled -= PlayerMove;
    }

    void Awake()
    {
        _collision = GetComponent<Collisions>();

        // Scripts

        _move = GetComponent<Move>();
        _wall = GetComponent<Wall>();
        _jump = GetComponent<Jump>();
        _dash = GetComponent<Dash>();

    }

    // Update is called once per frame
    void Update() //FixedUpdate ??
    {
        CambioEstados();
    }
    
    #endregion METODOS DEFAULT

    #region METODOS

    private void CambioEstados() 
    {
        if (_collision.onGround && !isDashing)
        {
            wallJumped = false;
        }

        if (_collision.onWall && !_collision.onGround)
        {
            if (move != Vector2.zero)
            {
                wallSlide = true;
                _wall.WallSlide(); 
            }
        }

        if (!_collision.onWall || _collision.onGround)
        {
            wallSlide = false;
        }


        if (_collision.onGround && !_groundTouch)
        {
            GroundTouch();
            _groundTouch = true;
        }

        if (!_collision.onGround && _groundTouch)
        {
            _groundTouch = false;
        }


        if (wallJumped || !canMove)
        {
            return;
        }

        if (!_collision.onWall && _collision.onGround && !isDashing)
        {
            canMove = true;
        }

        if (move.x > 0)
        {
            sidePlayer = 1;
            //metodo Flip ->script Animation
        }
        if (move.x < 0)
        {
            sidePlayer = -1;
            //metodo Flip -> script Animation

        }    
    }
    private void GroundTouch()
    {
        _hasDashed = false;
        isDashing = false;

        //animation parte 

        //particulas
    }

    private void PlayerJump(InputAction.CallbackContext obj) {
        if (!canMove) return;
        
        if (_collision.onGround)
        {
            _jump.Jump_player();
        }
        if (_collision.onWall && !_collision.onGround)
        {
            _wall.WallJump();
        }
    }

    private void PlayerDash(InputAction.CallbackContext obj)
    {
        if (move != Vector2.zero) // GetAxisRaw ??
        {
            _dash.Dash_player();
        }
    }

    private void PlayerMove(InputAction.CallbackContext obj) 
    {
        if (!canMove) return;
        move = _playerMoveInput.action.ReadValue<Vector2>();
        _move.SetDirection(move);
    }

    #endregion METODOS


}
