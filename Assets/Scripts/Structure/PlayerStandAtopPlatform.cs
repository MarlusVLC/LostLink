using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerStandAtopPlatform : MonoBehaviour
{
    // [SerializeField] private GameObject player;
    [SerializeField] private LayerMask attachableLayer;

    private Transform _originalParent;




    private void OnCollisionEnter2D(Collision2D other)
    {
        if (1 << other.gameObject.layer != attachableLayer)
        {
            return;
        }
        
        if (other.GetContact(0).normal.y < 0)
        {
            Debug.Log("Hit the top: " + other.GetContact(0).normal);
            _originalParent = other.transform.parent;
            other.transform.SetParent(transform);
        }
        

    }



    private void OnCollisionExit2D(Collision2D other)
    {
        if (1 << other.gameObject.layer != attachableLayer)
        {
            return;
        }
        other.transform.SetParent(_originalParent);
    }
}
