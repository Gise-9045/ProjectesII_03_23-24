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
    [SerializeField] private InputActionReference _playerMove;
    [SerializeField] private InputActionReference _playerJump;
    [SerializeField] private InputActionReference _playerDash;

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

    #endregion VARIABLES 

    #region METODOS DEFAULT
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
        Debug.Log(_playerMove.action.ReadValue<Vector2>());

        Vector2 aux = _playerMove.action.ReadValue<Vector2>();
        Vector2 move = new Vector2(aux.x, aux.y);

        _move.Walk(move, canMove, wallJumped, _physyics, _speed, _wallJumpLerp);

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

        #region Jump

        if (_playerJump.action.ReadValue<bool>())
        {
            //activar animacion salto

            if (_collision.onGround)
            {
                _jump.Jump_player(aux, _physyics, _jumpForce); 
            }
            if (_collision.onWall && !_collision.onGround)
            {
                _wall.WallJump(canMove, _collision, _jump, wallJumped, _physyics, _jumpForce); 
            }
        }

        #endregion Jump

        #region Dash
        if (_playerDash.action.ReadValue<bool>() && !_hasDashed)
        {
            if (aux.x != 0 || aux.y != 0) // GetAxisRaw ??
            {
                _dash.Dash_player(_hasDashed, _physyics, move, _jump, wallJumped, isDashing); 
            }
        }
        #endregion Dash

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
        canMove = false ; 
    }
    #endregion METODOS DEFAULT
    // Start is called before the first frame update

    #region METODOS
    private void GroundTouch()
    {
        _hasDashed = false;
        isDashing = false;

        //animation parte 

        //particulas
    }

    #endregion METODOS


}
