using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayersInteractions : MonoBehaviour
{
    [Header("Initial camera expansion")]
    [SerializeField] private Camera mainCam;
    [SerializeField] private GameObject blueOne;
    [SerializeField] private GameObject redOne;
    [SerializeField] [Range(1,9)] private float camExpansionFactor;

    [Space] [Header("Color features")]
    [SerializeField] [Range(0,1)] private float primaryScale;
    [SerializeField] private float gradientSmoothing;

    private SpriteRenderer _blueRenderer, _redRenderer;
    
    private float _currentMaximumDist_X;
    private float _currentDist_X;

    void Awake()
    {
        _currentMaximumDist_X = 0;
        camExpansionFactor /= 1000.0f;
    }

    void Start()
    {
        _blueRenderer = blueOne.GetComponent<SpriteRenderer>();
        _redRenderer = redOne.GetComponent<SpriteRenderer>();

    }

    // Update is called once per frame
    void Update()
    {
        _currentDist_X = Mathf.Abs(blueOne.transform.position.x - redOne.transform.position.x);

        ExpandCamera(_currentDist_X);
        UpdateColor(_currentDist_X);
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

    private void UpdateColor(float dist)
    {
        float additiveColor = primaryScale / Mathf.Clamp(dist/gradientSmoothing, 1,float.PositiveInfinity);
        // Debug.Log(dist);
        _blueRenderer.color = new Color(additiveColor, 0, primaryScale);
        _redRenderer.color = new Color(primaryScale, 0, additiveColor);
    }
}
