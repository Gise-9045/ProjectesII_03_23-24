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
    private SpriteRenderer _spriteRenderer; 
    [Space]
    [Header("Input References")]
    [SerializeField] private InputActionReference _playerMoveInput;
    [SerializeField] private InputActionReference _playerJumpInput;
    [SerializeField] private InputActionReference _playerDashInput;

    [Space]
    [Header("Booleans")]
    [SerializeField] private bool canMove = true;
    // public bool wallGrab;
    [SerializeField] private bool wallJumped;
    [SerializeField] private bool wallSlide;
    [SerializeField] private bool isDashing;
    
    public int sidePlayer = 1;
    private int jumpCount;
    private int maxJump; 

    private bool _groundTouch;

    [Header("Habilities")]
    public bool canDoubleJump = false;
    public bool canDash = false;

    //Particulas De momento nope

    public Vector2 move;
    public Rigidbody2D _physics; 

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
        _spriteRenderer = GetComponent<SpriteRenderer>();
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
        if(_collision.collectingJump) //REVISAR
        {
            canDoubleJump = true; 
        }

        if(_collision.collectingDash)
        {
            canDash = true; 
        }

        if (_collision.onGround && !isDashing)
        {
            wallJumped = false;

            jumpCount = 0; 

            if (canDoubleJump == true)
            {
                maxJump = 2;
            }

            if (canDoubleJump == false)
            {
                maxJump = 1;
            }
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


        if (!_collision.onWall && _collision.onGround && !isDashing)
        {
            canMove = true;
        }

        if (wallJumped || !canMove)
        {
            return;
        }


        if (move.x > 0)
        {
            sidePlayer = 1;

            _spriteRenderer.flipX = false; 
        }
        if (move.x < 0)
        {
            sidePlayer = -1;
            _spriteRenderer.flipX = true; 

        }    
    }
    private void GroundTouch()
    {
        isDashing = false;
    }

    private void PlayerJump(InputAction.CallbackContext obj) 
    {
        if (!canMove) return;
        
       // float gravityScale = _physics.gravityScale;

        _jump.Jump_player(jumpCount ,maxJump);

        if (_collision.onWall && !_collision.onGround)
        {
            _wall.WallJump(jumpCount, maxJump);
        }

        // _physics.gravityScale = gravityScale;
    }

    private void PlayerDash(InputAction.CallbackContext obj)
    {
        if (!canDash) return; 

        canMove = false; 
        _dash.Dashing();
        canMove = true; 

    }

    private void PlayerMove(InputAction.CallbackContext obj) 
    {
        if (!canMove) return;
        move = _playerMoveInput.action.ReadValue<Vector2>();
        _move.SetDirection(move);
    }

    #endregion METODOS


}
