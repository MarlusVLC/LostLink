using System;
using System.Collections;
using System.Collections.Generic;
using Aux_Classes;
using UnityEngine;

public class TogglerLight : MonoBehaviour, IInteractable
{
    [SerializeField] private Transform enablerPlayer;
    [SerializeField] private Transform affectedObject;

    private BoxCollider2D _presenceDetector;
    private Animator _anim;
    private Animator _affectedAnim;

    private bool _canPress;

    private void Awake()
    {
        _canPress = false;
    }

    void Start()
    {
        // _presenceDetector = transform.GetChild(0).GetComponent<BoxCollider2D>();
        _anim = transform.GetComponentInParent<Animator>();
        _affectedAnim = affectedObject.GetComponent<Animator>();
    }

    void Update()
    {
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform == enablerPlayer)
        {
            _anim.SetTrigger("ToggleOn");
            _canPress = true;
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.transform == enablerPlayer)
        {
            _anim.SetTrigger("ToggleOff");
            _canPress = false;
        }
    }
    
    
    
    
    public void Interact()
    {
        if (_canPress)
        {
            _affectedAnim.SetTrigger("On");
            _anim.SetTrigger("Defunct");
        }
    }
    
    
}
