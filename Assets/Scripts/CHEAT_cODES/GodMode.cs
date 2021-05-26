using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GodMode : MonoBehaviour
{
    [SerializeField] private float godSpeed;
    private MainMovement _mainMov;
    private Rigidbody2D _rb;
    private PlayerCommands _commands;
    private BoxCollider2D _collider;
    private bool isInGodMode;
    private bool godStabilished;
    
    // Start is called before the first frame update
    void Start()
    {
        _mainMov = GetComponent<MainMovement>();
        _rb = GetComponent<Rigidbody2D>();
        _commands = GetComponent<PlayerCommands>();
        _collider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftControl))
        {
            if (Input.GetKeyDown(KeyCode.G))
            {
                isInGodMode = !isInGodMode;
            }
        }

        if (isInGodMode )
        {
            transform.Translate(Input.GetAxis("Horizontal"+_mainMov.Controls) * godSpeed,
                Input.GetAxis("Vertical"+_mainMov.Controls) * godSpeed, 
                0);
            if (!godStabilished)
            {
                _mainMov.enabled = false;
                // _rb.gravityScale = 0;
                _rb.bodyType = RigidbodyType2D.Kinematic;
                _commands.enabled = false;
                _collider.enabled = false;
                godStabilished = true;
            }
        }

        else if (godStabilished)
        {
            _mainMov.enabled = true;
            // _rb.gravityScale = 1;
            _rb.bodyType = RigidbodyType2D.Dynamic;
            _commands.enabled = true;
            _collider.enabled = true;
            godStabilished = false;
        }

        
    }
}
