using System;
using System.Collections;
using System.Collections.Generic;
using Aux_Classes;
using UnityEngine;

public class PlayerCommands : MonoBehaviour
{
    [SerializeField] private string controls;
    [SerializeField] private Vector2 detectorBoxSize;
    
    private Collider2D _detectedCollider;

    private IInteractable _responder;
    // Start is called before the first frame update
    void Awake()
    {
        _responder = null;
    }

    // Update is called once per frame
    void Update()
    {
        _detectedCollider = Physics2D.OverlapBox(transform.position, detectorBoxSize, 0,
            1 << LayerMask.NameToLayer("Interactable"));
        // Debug.Log(_detectedCollider);

        if (_detectedCollider)
        {
            if (_responder == null)
            {
                _responder = _detectedCollider.transform.GetComponent<IInteractable>();
            }
        }
        else
        {
            _responder = null;
        }

        if (_responder != null)
        {
            ActUpon(_responder);
        }   
        
    }

    private void ActUpon(IInteractable interactable)
    {
        if (Input.GetButtonDown("Interact" + controls))
        {
            interactable.Interact();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = gameObject.GetComponent<SpriteRenderer>().color;
        Gizmos.DrawWireCube(transform.position, detectorBoxSize);
    }
}
