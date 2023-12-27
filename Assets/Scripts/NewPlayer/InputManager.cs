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
    private laserBehaviour _laser;
    [SerializeField] private Attack _attack;
    [SerializeField] private GameObject laser;
    public PlayerStats _playerStats;
    public Transform LaunchOffset;
    [Space]
    [Header("Input References")]
    [SerializeField] private InputActionReference _playerMoveInput;
    [SerializeField] private InputActionReference _playerJumpInput;
    [SerializeField] private InputActionReference _playerDashInput;
    [SerializeField] private InputActionReference _playerAttackInput;
    [SerializeField] private InputActionReference _playerLaserInput;

    [Space]
    [Header("Booleans")]
    [SerializeField] private bool canMove = true;
    // public bool wallGrab;
    [SerializeField] private bool wallJumped;
    [SerializeField] private bool wallSlide;
    [SerializeField] private bool isDashing;
    [SerializeField] private bool isAttacking;

    public int sidePlayer = 1;
    private int jumpCount = 0;
    private int maxJump = 2;
    private bool _groundTouch;

    [Header("Habilities")]
    public bool canDoubleJump = false;
    public bool canDash = false;

    //Particulas De momento nope

    public Vector2 move;
    public Vector2 shoot;
    public Rigidbody2D _physics;
    [SerializeField] private GameObject model;
    public bool facingRight; 

    #endregion VARIABLES 

    #region METODOS DEFAULT

    private void OnEnable()
    {
        _playerJumpInput.action.started += PlayerJump;
        _playerDashInput.action.performed += PlayerDash;
        _playerMoveInput.action.started += PlayerMove;
        _playerMoveInput.action.canceled += PlayerMove;

        //_playerLaserInput.action.performed += PlayerLaser;
    }

    private void OnDisable() 
    {
        _playerJumpInput.action.started -= PlayerJump;
        _playerDashInput.action.performed -= PlayerDash;
        _playerMoveInput.action.started -= PlayerMove;
        _playerMoveInput.action.canceled -= PlayerMove;
        //_playerLaserInput.action.canceled -= PlayerLaser;
    }

    void Awake()
    {
        _collision = GetComponent<Collisions>();
       // _spriteRenderer = GetComponent<SpriteRenderer>();
        // Scripts

        _move = GetComponent<Move>();
        _wall = GetComponent<Wall>();
        _jump = GetComponent<Jump>();
        _dash = GetComponent<Dash>();
        _laser = GetComponent<laserBehaviour>();
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
        //_attack.StartAttack(_playerAttackInput.action.ReadValue<float>());

        if (_collision.collectingJump)
        {
            canDoubleJump = true; 
        }
        if (_collision.collectingDash)
        {
            canDash = true;
        }
        if (_collision.onGround && !isDashing && _physics.velocity.y < 0.2f)
        {
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

        if (_collision.onGround && !_groundTouch)
        {
            GroundTouch();
            _groundTouch = true;
        }

        if (!_collision.onGround && _groundTouch)
        {
            _groundTouch = false;
        }


        if (_collision.onGround && !isDashing)
        {
            canMove = true;
        }

        if (!canMove)
        {
            return;
        }

        if (move.x > 0 && facingRight)
        {
            sidePlayer = 1;
            Flip(); 
        }
        if (move.x < 0 && !facingRight)
        {
            sidePlayer = -1;
            Flip();
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

        if (_jump.inWater == false)
        {
            _jump.Jump_player(jumpCount, maxJump);
            jumpCount++;
        } else if(_jump.inWater == true)
        {
            _jump.Jump_player(jumpCount, maxJump);
            jumpCount = 0;
        }

        // _physics.gravityScale = gravityScale;
    }

    private void PlayerDash(InputAction.CallbackContext obj)
    {
        if (!canDash)
        {
            return;
        }
        if (move != Vector2.zero) // GetAxisRaw ??
        {
            _dash.PlayerDashing();
        }
    }

    private void PlayerMove(InputAction.CallbackContext obj) 
    {

        if (!canMove) return;
        move = _playerMoveInput.action.ReadValue<Vector2>();
        _move.SetDirection(move);
  
    }

    private void PlayerLaser(InputAction.CallbackContext obj)
    {
        Instantiate(laser, LaunchOffset.position, transform.rotation);
        _laser.Shoot();
    }

    private void Flip()
    {
        Vector2 currentScale = model.transform.localScale;
        currentScale.x *= -1;

        model.transform.localScale = currentScale;

        facingRight = !facingRight;
    }

   
    #endregion METODOS


}
