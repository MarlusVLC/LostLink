using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[ExecuteAlways]
public class DistanceGradient : MonoBehaviour
{
    [Space] [Header("Color features")]
    [SerializeField] [Range(0,1)] private float primaryScale;
    [SerializeField] private float gradientSmoothing;

    
    private GameObject _redOne, _blueOne;
    private SpriteRenderer _redRenderer, _blueRenderer;

    private float _currentDist;



    void Start()
    {
        if (transform.childCount >= 2)
        {
            _redOne = transform.GetChild(1).gameObject;
            _blueOne = transform.GetChild(0).gameObject;
            _redRenderer = _redOne.GetComponent<SpriteRenderer>();
            _blueRenderer = _blueOne.GetComponent<SpriteRenderer>();
        }
        

    }

    // Update is called once per frame
    void Update()
    {
        _currentDist = Vector2.Distance(_redOne.transform.position,_blueOne.transform.position);

        UpdateColor(_currentDist);
    }
    

    private void UpdateColor(float dist)
    {
        float additiveColor = primaryScale / Mathf.Clamp(dist/gradientSmoothing, 1,float.PositiveInfinity);
        _blueRenderer.color = new Color(additiveColor, 0, primaryScale);
        _redRenderer.color = new Color(primaryScale, 0, additiveColor);
    }
}