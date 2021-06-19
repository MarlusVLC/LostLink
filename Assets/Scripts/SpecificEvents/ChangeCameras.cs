using System;
using System.Collections;
using System.Collections.Generic;
using Aux_Classes;
using Cinemachine;
using UnityEditor.U2D.Path;
using UnityEngine;

namespace SpecificEvents
{
    public class ChangeCameras : MonoBehaviour
    {
        [SerializeField] private Transform _blueOne;
        [SerializeField] private Transform _redOne;
    
        [SerializeField] private GameObject initialCam;

        [SerializeField] private GameObject[] playerCams = new GameObject[2];
    
        [Tooltip("A distância que ambos os jogadores devem estar da origem para que a" +
                 "mudança de camêras seja executada")]
        [SerializeField] private float changingPointX;
        [SerializeField] private float changingPointY;

        private Vector2 _startGizmoLineX;
        private Vector2 _endGizmoLineX;
        private Vector2 _startGizmoLineY;
        private Vector2 _endGizmoLineY;
        
        private short _framesFinishedAfterInitialCamDeactivation;


        void Start()
        {
            Array.ForEach(playerCams, p => p.SetActive(false));
        }
    
        // Update is called once per frame
        void Update()
        {
            if (CanChangeCameras(_redOne) || (CanChangeCameras(_blueOne)))
            {
                initialCam.transform.Translate(0,0,1000);
            }
        }

        private void LateUpdate()
        {
            if (_framesFinishedAfterInitialCamDeactivation > 1)
            {
                SetSplitScreen();
                
            }
            
            if (CanChangeCameras(_redOne) || (CanChangeCameras(_blueOne)))
            {
                _framesFinishedAfterInitialCamDeactivation++;
            }
        }


        public void SetSplitScreen()
        {
            playerCams[0].SetActive(true);
            playerCams[1].SetActive(true);
                
            GameManager.getInstance.SetRespawnState(_redOne.gameObject,_redOne.position, Vector3.zero, 0);
            GameManager.getInstance.SetRespawnState(_blueOne.gameObject,_blueOne.position, Vector3.zero, 0);

            initialCam.SetActive(false);
            
            Destroy(this);

        }

        private bool CanChangeCameras(Transform element)
        {
            return (Mathf.Abs(element.position.x) > changingPointX
                    && element.position.y > changingPointY);
        }


        private void OnDrawGizmosSelected()
        {
            _startGizmoLineX.y = _startGizmoLineY.x = 1000f;
            _endGizmoLineX.y = _endGizmoLineY.x = -1000f;
            _startGizmoLineX.x = _endGizmoLineX.x = changingPointX;
            _startGizmoLineY.y = _endGizmoLineY.y = changingPointY; 
            Gizmos.color = Color.red;
            Gizmos.DrawLine(_startGizmoLineX, _endGizmoLineX);
            Gizmos.DrawLine(-_startGizmoLineX, -_endGizmoLineX);
            Gizmos.DrawLine(_startGizmoLineY, _endGizmoLineY);
        }
    }
}

