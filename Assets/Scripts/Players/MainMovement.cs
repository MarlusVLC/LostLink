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
    [Space(10)]
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Vector2 groundCheckBoxPos;
    [SerializeField] private Vector2 groundCheckBoxSize;
    [SerializeField] private LayerMask trapLayer;
    [Space(10)]
    [SerializeField] private float horizontalMoveRate;
    [SerializeField] private float jumpMoveRate;
    [SerializeField] private float deathHeight = -10; //TALVEZ TENHA QUE MUDAR DEPOIS
    // [SerializeField] private float DEFAULT_coyoteTime;
    [Space(10)]
    [Range(0,1)][SerializeField] private float DEFAULT_jumpBuffer;
    [Range(0,1)][SerializeField] private float hDampingBasic;
    [Range(0,1)][SerializeField] private float hDampingStop;
    [Range(0,1)][SerializeField] private float hDampingTurn;
    [Range(0,1)][SerializeField] private float jumpHDampingBasic;
    [Range(0,1)][SerializeField] private float jumpHDampingStop;
    [Range(0,1)][SerializeField] private float jumpHDampingTurn;
    [Range(0,1)][SerializeField] private float jumpCut;
    [Range(0,1)][SerializeField] private float freeFallForce;    
    private Rigidbody2D _rb;
    private MoveState _moveState;
    private float _coyoteTime;
    private float _jumpBuffer;
    private int _numberOfJumps;
    private bool _canJump;
    private bool _areMovementsDamped;
    private Animator _animator;
    private int _animVertSpeed = Animator.StringToHash("VerticalSpeed");
    private int _animHorSpeed = Animator.StringToHash("HorizontalSpeed");
    private int _animPrepareJump = Animator.StringToHash("PrepareJump");
    
    
    
    
    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _areMovementsDamped = true;
        _animator = GetComponent<Animator>();
    }

    void Start()
    {
        GameManager.getInstance.SetRespawnState(gameObject, transform.position, _rb.velocity, _rb.rotation);
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
        


        #region Horizontal Movement

        if (_areMovementsDamped)
        {
            float horizontalVelocity = _rb.velocity.x;
        
            horizontalVelocity += Input.GetAxisRaw("Horizontal"+controls) * horizontalMoveRate;
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
            
            _animator.SetFloat(_animHorSpeed, Mathf.Abs(horizontalVelocity));
        }
        
        
        
        #endregion



        #region Jump Process

        _canJump =  Physics2D.OverlapBox((Vector2)transform.position + groundCheckBoxPos, groundCheckBoxSize, 0,
            groundLayer) || _coyoteTime > 0;


        _jumpBuffer -= Time.deltaTime;
        if (Input.GetButtonDown("Jump"+controls))
        {
            _jumpBuffer = DEFAULT_jumpBuffer;
        }

        if (_jumpBuffer > 0 /*Input.GetButtonDown("Jump"+controls)*/ && _canJump)
        {
            // StartCoroutine(PrepareJump(new Vector2(_rb.velocity.x, jumpMoveRate),0.1f));
            _animator.SetTrigger(_animPrepareJump);
            _rb.velocity = new Vector2(_rb.velocity.x, jumpMoveRate);
            _coyoteTime = 0;
        }
        
        
        if (Input.GetButtonUp("Jump"+controls))
        {
            if (_rb.velocity.y > jumpMoveRate * jumpCut)
            {
                _rb.velocity = new Vector2(_rb.velocity.x, jumpMoveRate * jumpCut);
            }
        }
        
        _animator.SetFloat(_animVertSpeed, _rb.velocity.y);

        // if (!_canJump && Input.GetButtonDown("Jump" + controls))
        // {
        //     FreeFall();
        // }

        #endregion
        
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
        _rb.AddForce(new Vector2(0,-freeFallForce), ForceMode2D.Impulse);
        _jumpBuffer = 0;
    }


    private void Die()
    {
        RespawnState respawnState = GameManager.getInstance.GetRespawnState(gameObject);
        transform.position = respawnState.Position;
        _rb.rotation = respawnState.Rotation;
        _rb.velocity = respawnState.Velocity;
    }

    private IEnumerator PrepareJump(Vector2 jumpRate, float preparationTime)
    {
        _animator.SetTrigger(_animPrepareJump);
        yield return new WaitForSeconds(preparationTime);
        _rb.velocity = jumpRate;
        _coyoteTime = 0;
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
    

    public string Controls => controls;
}
