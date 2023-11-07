using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem; 

public class InputManager : MonoBehaviour
{
    #region VARIABLES 

    public Rigidbody2D _physyics;

    [Space]
    [Header("Scripts")]
    [SerializeField] private Collisions _collision;
    [SerializeField] private Dash _dash;
    [SerializeField] private Jump _jump;
    [SerializeField] private Move _move;
    [SerializeField] private Wall _wall;

    [Space]
    [Header("Inputs")]
    [SerializeField] private InputActionReference _playerMoveInput;
    [SerializeField] private InputActionReference _playerJumpInput;
    [SerializeField] private InputActionReference _playerDashInput;

    // Animaciones Luego 

    [Space]
    [Header("Stats")]
    [SerializeField] private float _speed = 10f;
    [SerializeField] private float _jumpForce = 50f;
    [SerializeField] private float _slideSpeed = 5f;
    [SerializeField] private float _wallJumpLerp = 10f;


    [Space]
    [Header("Booleans")]
    public bool canMove;
    // public bool wallGrab;
    public bool wallJumped;
    public bool wallSlide;
    public bool isDashing;

    [Space]

    public int sidePlayer = 1;

    private bool _groundTouch;
    private bool _hasDashed;

    //Particulas De momento nope

    public Vector2 aux; 
    public Vector2 move; 

    #endregion VARIABLES 

    #region METODOS DEFAULT

    private void OnEnable()
    {
        _playerJumpInput.action.performed += PlayerJump;
        _playerDashInput.action.performed += PlayerDash;
        _playerMoveInput.action.performed += PlayerMove;
    }

    void Awake()
    {
        _collision = GetComponent<Collisions>();
        _physyics = GetComponent<Rigidbody2D>();

        // Scripts

        _move = GetComponent<Move>();
        _wall = GetComponent<Wall>();
        _jump = GetComponent<Jump>();
        _dash = GetComponent<Dash>();

    }

    // Update is called once per frame
    void Update() //FixedUpdate ??
    {

        aux = _playerMoveInput.action.ReadValue<Vector2>();
        move = new Vector2(aux.x, aux.y);

        #region Wall

        if (_collision.onGround && !isDashing)
        {
            wallJumped = false;
            GetComponent<BetterJumping>().enabled = true; 
        }

        if (_collision.onWall && !_collision.onGround)
        {
            if (aux.x != 0 && aux.y != 0)
            {
                wallSlide = true;
                _wall.WallSlide(canMove, _physyics, _collision, _slideSpeed); 
            }
        }

        if (!_collision.onWall || _collision.onGround)
        {
            wallSlide = false;
        }

        #endregion Wall

        #region Ground

        if (_collision.onGround && !_groundTouch)
        {
            GroundTouch();
            _groundTouch = true;
        }

        if (!_collision.onGround && _groundTouch)
        {
            _groundTouch = false;
        }

        #endregion Ground

        if (wallJumped || !canMove)
        {
            return;
        }

        if (!_collision.onWall && _collision.onGround && !isDashing)
        {
            canMove = true;
        }

        if (aux.x > 0)
        {
            sidePlayer = 1;
            //metodo Flip ->script Animation
        }
        if (aux.x < 0)
        {
            sidePlayer = -1;
            //metodo Flip -> script Animation

        }
    }
    #endregion METODOS DEFAULT

    #region METODOS
    private void GroundTouch()
    {
        _hasDashed = false;
        isDashing = false;

        //animation parte 

        //particulas
    }

    private void PlayerJump(InputAction.CallbackContext obj)
    {
        if (_collision.onGround)
        {
            _jump.Jump_player();
        }
        if (_collision.onWall && !_collision.onGround)
        {
            _wall.WallJump(canMove, _collision, _jump, wallJumped, _physyics, _jumpForce);
        }
    }

    private void PlayerDash(InputAction.CallbackContext obj)
    {
        if (aux.x != 0 || aux.y != 0) // GetAxisRaw ??
        {
            _dash.Dash_player(_hasDashed, _physyics, move, _jump, wallJumped, isDashing);
        }
    }

    private void PlayerMove(InputAction.CallbackContext obj) 
    {

        if (canMove == false)
            return;

        if (!wallJumped)
        {
            _physyics.velocity = new Vector2(move.x * 0.5f, _physyics.velocity.y);
        }

        if (canMove == true)
        {
            Debug.Log("puede moverse"); 
            _move.Walk(_playerMoveInput);
        }
    }

    #endregion METODOS


}
