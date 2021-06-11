using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Aux_Classes;
using UnityEngine;



public class PlayerStandAtopPlatform : MonoBehaviour
{
    // [SerializeField] private GameObject player;
    [SerializeField] private LayerMask attachableLayer;
    [SerializeField] private GameObject[] activables;

    private Transform _originalParent;




    private void OnCollisionEnter2D(Collision2D other)
    {
        if ((1 << other.gameObject.layer | attachableLayer) != attachableLayer)
        {
            return;
        }
        
        if (other.GetContact(0).normal.y < 0)
        {
            Debug.Log("Hit the top: " + other.GetContact(0).normal);
            _originalParent = other.transform.parent;
            other.transform.SetParent(transform);
            activables.Where(s => !s.activeSelf).ForEach(s => s.SetActive(true));
        }
    }


    private void OnCollisionStay2D(Collision2D other)
    {
        activables.Where(s => s && !s.activeSelf).ForEach(s => s.SetActive(true));
    }


    private void OnCollisionExit2D(Collision2D other)
    {
        if ((1 << other.gameObject.layer | attachableLayer) != attachableLayer)
        {
            return;
        }
        other.transform.SetParent(_originalParent);
        activables.Where(s =>  s.activeSelf).ForEach(s => s.SetActive(false));

    }
}
