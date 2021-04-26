using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Aux_Classes;
using UnityEditor;

public class MainMovement : MonoBehaviour
{
    private enum MoveState
    {
        Idle,
        Walking,
        Jumping,
        Falling
    };
    [Tooltip("Either Player1 or Player2")]
    [SerializeField]private string controls; //Player1 ou Player2
    [Space]
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Vector2 groundCheckBoxPos;
    [SerializeField] private Vector2 groundCheckBoxSize;
    [SerializeField] private LayerMask trapLayer;
    [Space]
    // [SerializeField] private float horizontalMoveRate;
    [SerializeField] private float jumpMoveRate;
    [SerializeField] private float deathHeight = -10; //TALVEZ TENHA QUE MUDAR DEPOIS
    // [SerializeField] private float DEFAULT_coyoteTime;
    [SerializeField] private float DEFAULT_jumpBuffer;
    [SerializeField] private float hDampingBasic;
    [SerializeField] private float hDampingStop;
    [SerializeField] private float hDampingTurn;
    [SerializeField] private float jumpHDampingBasic;
    [SerializeField] private float jumpHDampingStop;
    [SerializeField] private float jumpHDampingTurn;
    [SerializeField] private float jumpCut;
    [SerializeField] private float freeFallForce;

    
    private Rigidbody2D _rb;
    // private BoxCollider2D _boxCollider;
    // private CapsuleCollider2D _capsuleCollider;
    private MoveState _moveState;
    // private Animator _animator;
    private float _coyoteTime;
    private float _jumpBuffer;
    private int _numberOfJumps;
    private bool _canJump;
    private bool _areMovementsDamped;
    
    
    
    
    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        // _boxCollider = GetComponent<BoxCollider2D>();
        _areMovementsDamped = true;
        // _capsuleCollider = GetComponent<CapsuleCollider2D>();
        // _animator = GetComponent<Animator>();
    }

    void Start()
    {
        GameManager.getInstance.SetRespawnState(this.gameObject, transform.position, _rb.velocity, _rb.rotation);
    }


    private void Update()
    {
        if (_moveState == MoveState.Idle && _rb.velocity.x != 0)
        {
            _moveState = MoveState.Walking;
        }
        
        
        DEBUGMarkCheckpoint();
    }

    void FixedUpdate()
    {

        if (transform.position.y <= deathHeight) //TALVEZ TENHA QUE MUDAR PRA CONSIDERAR ALGO MAIS GENÉRICO
        {
            Die();
        }


        if (_areMovementsDamped)
        {
            float horizontalVelocity = _rb.velocity.x;
        
            horizontalVelocity += Input.GetAxisRaw("Horizontal"+controls);
            if (_rb.velocity.y == 0)
            {
                if (Mathf.Abs(Input.GetAxisRaw("Horizontal"+controls)) < 0.01f)
                    horizontalVelocity *= Mathf.Pow(1f - hDampingStop, Time.deltaTime * 10f);
                else if (Mathf.Sign(Input.GetAxisRaw("Horizontal"+controls)) != Mathf.Sign(horizontalVelocity))
                    horizontalVelocity *= Mathf.Pow(1f - hDampingTurn, Time.deltaTime * 10f);
                else
                    horizontalVelocity *= Mathf.Pow(1f - hDampingBasic, Time.deltaTime * 10f);
            }
            else
            {
                if (Mathf.Abs(Input.GetAxisRaw("Horizontal"+controls)) < 0.01f)
                    horizontalVelocity *= Mathf.Pow(1f - jumpHDampingStop, Time.deltaTime * 10f);
                else if (Mathf.Sign(Input.GetAxisRaw("Horizontal"+controls)) != Mathf.Sign(horizontalVelocity))
                    horizontalVelocity *= Mathf.Pow(1f - jumpHDampingTurn, Time.deltaTime * 10f);
                else
                    horizontalVelocity *= Mathf.Pow(1f - jumpHDampingBasic, Time.deltaTime * 10f);
            }
        
            _rb.velocity = new Vector2(horizontalVelocity, _rb.velocity.y);
        }
       

        
        
        
        // _canJump = _boxCollider.IsTouchingLayers(groundLayer) || _coyoteTime > 0;
        _canJump =  Physics2D.OverlapBox((Vector2)transform.position + groundCheckBoxPos, groundCheckBoxSize, 0,
            groundLayer) || _coyoteTime > 0;
        // _animator.SetBool("Can Jump", _canJump);


        _jumpBuffer -= Time.deltaTime;
        if (Input.GetButtonDown("Jump"+controls))
        {
            _jumpBuffer = DEFAULT_jumpBuffer;
        }

        if ((_jumpBuffer > 0) && _canJump)
        {
            _rb.velocity = new Vector2(_rb.velocity.x, jumpMoveRate);
            _coyoteTime = 0;
            // _animator.SetBool("Has Jumped Once", true);
        }

        // if (Input.GetButtonUp("Jump"+controls))
        if (Input.GetButtonUp("Jump"+controls))
        {
            if (_rb.velocity.y > jumpMoveRate * jumpCut)
            {
                _rb.velocity = new Vector2(_rb.velocity.x, jumpMoveRate * jumpCut);
            }
        }
        
        // FreeFall();
    }
    
    
    
    
    
    private void OnDrawGizmos()
    {
        if (Application.isEditor)
        {
            _canJump =  Physics2D.OverlapBox((Vector2)transform.position + groundCheckBoxPos, groundCheckBoxSize, 0,
                groundLayer);
        }
        Gizmos.color = _canJump ? Color.red : Color.blue;
        Gizmos.DrawWireCube((Vector2)transform.position + groundCheckBoxPos, groundCheckBoxSize);
    }



    private void FreeFall()
    {
        if (!_canJump && Input.GetButtonDown("Jump"+controls))
        {
            _rb.AddForce(new Vector2(0,-freeFallForce));
            _jumpBuffer = 0;
        }
    }


    private void Die()
    {
        RespawnState respawnState = GameManager.getInstance.GetRespawnState(this.gameObject);
        transform.position = respawnState.Position;
        _rb.rotation = respawnState.Rotation;
        _rb.velocity = respawnState.Velocity;
    }

    private void DEBUGMarkCheckpoint()
    {
        if (Input.GetKey(KeyCode.LeftShift) && (Input.GetKey(KeyCode.M)))
        {
            GameManager.getInstance.SetRespawnState(gameObject,transform.position, Vector3.zero, 0);
            print("Checkpoint saved!");
            print("Die as you wish :DDDD");
        }
    }


    public void EnableMoveDamping()
    {
        _areMovementsDamped = true;
    }

    public void DisableMoveDamping()
    {
        _areMovementsDamped = false;
    }

    public bool CanJump
    {
        get { return _canJump; }
    }
    
    
}
