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
    private PositionCheckingBox _checkingBox;
    private Tuple<Vector2, Vector2> _checkingBoxOriginalData; //modificar depois
    private bool hasPlayerAtop;
    private float OriginalSizeBoxY;
    private float OriginalOffsetBoxY;


    private void Awake()
    {
        if (TryGetComponent(out _checkingBox))
        {
            OriginalSizeBoxY = _checkingBox.CollisionDetectorSize.y;
            OriginalOffsetBoxY = _checkingBox.CollisionDetectorOffset.y;

        }
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if ((1 << other.gameObject.layer | attachableLayer) != attachableLayer)
        {
            return;
        }
        
        if (other.GetContact(0).normal.y < 0)
        {
            // Debug.Log("Hit the top: " + other.GetContact(0).normal);
            _originalParent = other.transform.parent;
            other.transform.SetParent(transform);
            hasPlayerAtop = true;
            _checkingBoxOriginalData = _checkingBox.AdaptToAboveEntity(
                    _checkingBox.CollisionDetectorSize.x, 7,
                    _checkingBox.CollisionDetectorOffset.x, 2);
                activables.Where(s => s && !s.activeSelf).ForEach(s => s.SetActive(true));
        }
    }


    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.GetContact(0).normal.y < 0 == false)
        {
            return;
        }
        activables.Where(s => s && !s.activeSelf).ForEach(s => s.SetActive(true));
    }


    private void OnCollisionExit2D(Collision2D other)
    {
        if ((1 << other.gameObject.layer | attachableLayer) != attachableLayer)
        {
            return;
        }
        other.transform.SetParent(_originalParent);
        activables.Where(s => s && s.activeSelf).ForEach(s => s.SetActive(false));
        hasPlayerAtop = false;
        if (_checkingBox != null)
        {
            _checkingBox.AdaptToAboveEntity(
                _checkingBox.CollisionDetectorSize.x,  _checkingBoxOriginalData.Item1.y,
                _checkingBox.CollisionDetectorOffset.x, _checkingBoxOriginalData.Item2.y); 
            print(_checkingBox.CollisionDetectorSize);
        }
    }
    
    

    public bool HasPlayerAtop => hasPlayerAtop;
}
