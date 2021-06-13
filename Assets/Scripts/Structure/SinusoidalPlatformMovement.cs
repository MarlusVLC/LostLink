using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using Unity.Mathematics;
using UnityEngine;

public class SinusoidalPlatformMovement : MonoCache
{
    [SerializeField] private bool moveHorizontally;
    [SerializeField] private float periodX;
    [SerializeField] private float amplitudeX;
    [SerializeField] private float shiftX;

    
    [SerializeField] private bool moveVertically;
    [SerializeField] private float periodY;
    [SerializeField] private float amplitudeY;
    [SerializeField] private float shiftY;

    
    private float _x;
    private float _y;

    private float _deltaX;
    private float _deltaY;

    private float _midlineX;
    private float _midlineY;
    private float _midlineXDEBUG;
    private float _midlineYDEBUG;
    
    private float _shiftXDEBUG;
    private float _shiftYDEBUG;

    private Vector2 _maxPointX;
    private Vector2 _minPointX;
    private Vector2 _minOffsetX;
    private Vector2 _maxOffsetX;
    
    private Vector2 _maxPointY;
    private Vector2 _minPointY;
    private Vector2 _minOffsetY;
    private Vector2 _maxOffsetY;

    private Collider2D _collider;

    protected override void Awake()
    {
        base.Awake();
        _midlineX = transform.position.x;
        _midlineY = transform.position.y;
    }


    void FixedUpdate()
    {
        _deltaX = _deltaX >= periodX ? 0 : _deltaX + Time.deltaTime;
        _x = moveHorizontally
            ? amplitudeX * Mathf.Sin((2 * Mathf.PI / Mathf.Abs(periodX))*(_deltaX-shiftX)) + _midlineX
            : transform.position.x;
        
        _deltaY = _deltaY >= periodY ? 0 : _deltaY + Time.deltaTime;
        _y = moveVertically
            ? amplitudeY * Mathf.Sin((2 * Mathf.PI / Mathf.Abs(periodY))*(_deltaY-shiftY)) + _midlineY
            : transform.position.y;
        
        transform.position = new Vector3(_x, _y, transform.position.z);
    }

    private void OnDrawGizmos()
    {
        _collider = GetComponent<BoxCollider2D>();

        if (!Application.isPlaying)
        {
            _midlineXDEBUG = transform.position.x;
            _midlineYDEBUG = transform.position.y;
        }
        else
        {
            _midlineXDEBUG = _midlineX;
            _midlineYDEBUG = _midlineY;
        }

        _maxPointX = new Vector2(_midlineXDEBUG + amplitudeX, _midlineYDEBUG);
        _minPointX = new Vector2(_midlineXDEBUG - amplitudeX, _midlineYDEBUG);

        _maxOffsetX = new Vector2(_midlineXDEBUG + amplitudeX + _collider.bounds.size.x / 2, _midlineYDEBUG);
        _minOffsetX = new Vector2(_midlineXDEBUG - amplitudeX - _collider.bounds.size.x / 2, _midlineYDEBUG);
        
        _maxPointY = new Vector2(_midlineXDEBUG, _midlineYDEBUG + amplitudeY);
        _minPointY = new Vector2(_midlineXDEBUG, _midlineYDEBUG - amplitudeY);
        
        _maxOffsetY = new Vector2(_midlineXDEBUG, _midlineYDEBUG + Math.Abs(amplitudeY) + _collider.bounds.size.y/2);
        _minOffsetY = new Vector2(_midlineXDEBUG, _midlineYDEBUG - Math.Abs(amplitudeY) - _collider.bounds.size.y/2);
        
        
        
        if (moveHorizontally)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(_maxPointX,_minPointX);
            Gizmos.DrawSphere(_maxPointX, 0.1f);
            Gizmos.DrawSphere(_minPointX, 0.1f);
            Gizmos.color = Color.green;
            Gizmos.DrawLine(_maxPointX,_maxOffsetX);
            Gizmos.DrawSphere(_maxOffsetX,0.1f);
            Gizmos.DrawLine(_minPointX,_minOffsetX);
            Gizmos.DrawSphere(_minOffsetX,0.1f);
        }
        
        if (moveVertically)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(_maxPointY,_minPointY);
            Gizmos.DrawSphere(_maxPointY, 0.1f);
            Gizmos.DrawSphere(_minPointY, 0.1f);
            Gizmos.color = Color.green;
            Gizmos.DrawLine(_maxPointY,_maxOffsetY);
            Gizmos.DrawSphere(_maxOffsetY,0.1f);
            Gizmos.DrawLine(_minPointY,_minOffsetY);
            Gizmos.DrawSphere(_minOffsetY,0.1f);
        }

    }
}
