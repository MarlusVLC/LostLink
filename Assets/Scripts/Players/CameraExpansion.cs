using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraExpansion : MonoBehaviour
{
    [Header("Initial camera expansion")]
    [SerializeField] private Camera mainCam;
    [SerializeField] private float camExpansionFactor;
    
    
    private Transform _redOne;
    private Transform _blueOne;
    
    private float _currentMaximumDist_X;
    private float _currentDist_X;
    void Awake()
    {
        _currentMaximumDist_X = 0;
        camExpansionFactor /= 1000.0f;
    }

    void Start()
    {
        if (transform.childCount >= 2)
        {
            _redOne = transform.GetChild(0);
            _blueOne = transform.GetChild(1);
        }
    }

    // Update is called once per frame
    void Update()
    {
        _currentDist_X = Mathf.Abs(_blueOne.position.x - _redOne.position.x);

        ExpandCamera(_currentDist_X);
    }

    private void ExpandCamera(float dist)
    {

        if (dist > _currentMaximumDist_X)
        {
            mainCam.orthographicSize += _currentDist_X * camExpansionFactor;
            _currentMaximumDist_X = dist;
        }

        if (mainCam.orthographicSize >= 15.0f)
        {
            Vector3 camPosition = mainCam.transform.position;
            mainCam.transform.position = new Vector3(camPosition.x, mainCam.orthographicSize-10, camPosition.z);
        }
    }
    

}
