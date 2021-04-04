using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

namespace SpecificEvents
{
    public class ChangeCameras : MonoBehaviour
    {
    
        [SerializeField] private Transform[] gates = new Transform[2];
    
        // [SerializeField] private Transform[] players = new Transform[2];
        [SerializeField] private Transform _blueOne;
        [SerializeField] private Transform _redOne;
    
        [SerializeField] private GameObject initialCam;
    
        [SerializeField] private GameObject[] playerCams = new GameObject[2];
    
        [Tooltip("A distância que ambos os jogadores devem estar da origem para que a" +
                 "mudança de camêras seja executada")]
        [SerializeField] private float changingPoint;
    
        private Animator[] _anims = new Animator[2];
    
        private short _animsFinished;
        private short _framesFinishedAfterMainCamDeactivation;
        private AnimatorStateInfo _currAnimation;
    
        private void Awake()
        {
            _animsFinished = 0;
        }
    
        void Start()
        {
            for (int i = 0; i < 2; i++)
            {
                _anims[i] = gates[i].GetComponent<Animator>();
            }
        }
    
        // Update is called once per frame
        void Update()
        {
            if (_blueOne.position.x < -changingPoint && _redOne.position.x > changingPoint)
            {
                foreach (var _anim in _anims)
                {
                    _anim.SetTrigger("Close");
                }
            }
            
            foreach (var _anim in _anims)
            {
                _currAnimation = _anim.GetCurrentAnimatorStateInfo(0);
                if (_currAnimation.normalizedTime > 0.9 && _currAnimation.IsName("Close"))
                {
                    _animsFinished++;
                }
            }
    
            if (_animsFinished >= 2)
            {
                initialCam.GetComponent<Camera>().farClipPlane = 0.32f;
                // initialCam.SetActive(false);
            }
        }

        private void LateUpdate()
        {
            if (_framesFinishedAfterMainCamDeactivation > 3)
            {
                playerCams[0].SetActive(true);
                playerCams[1].SetActive(true);
                
                GameManager.getInstance.SetRespawnState(_redOne.gameObject,_redOne.position, Vector3.zero, 0);
                GameManager.getInstance.SetRespawnState(_blueOne.gameObject,_blueOne.position, Vector3.zero, 0);

                
                Destroy(this);
            }
    
            if (_animsFinished >= 2)
                _framesFinishedAfterMainCamDeactivation++;
        }
    }
}

