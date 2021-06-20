using System;
using System.Collections.Generic;
using System.Linq;
using Audio;
using Aux_Classes;
using UnityEngine;

namespace Interactions.Interactables
{
    public class Light : Interactable
    {
        [SerializeField] private Transform[] enablerPlayers;
        [SerializeField] private LayerMask enablerLayer;
        [SerializeField] private bool useMask = true;
    
        private BoxCollider2D _presenceDetector;
        protected Animator _anim;
        protected AudioLib _audioLib;
    
        [Space(11)]
        protected bool _canPress;
        protected int _keys;
        protected int _keySet;


        private void Awake()
        {
            _audioLib = GetComponent<AudioLib>();
        }

        void Start()
        {
            _canPress = false;
            _anim = transform.GetComponentInParent<Animator>();
            if (!_anim)
                TryGetComponent(out _anim);
            _keySet = useMask ? enablerLayer.value.CountSetBits() : enablerPlayers.Length ;
        }
        
        
        
        
        
        

        private void Update()
        {
            _anim.SetBool("isOn", _keys >= _keySet);
            _canPress = _keys >= _keySet;
        }

        
        
        
        
        
        
        

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (useMask)
            {
                int otherMask = 1 << other.gameObject.layer;

                if ((otherMask | enablerLayer) == enablerLayer)
                {
                    _keys++;
                    _audioLib.LightSFX();
                }
            }
            
            else
            {
                if (enablerPlayers.Contains(other.transform))
                {
                    _audioLib.LightSFX();
                    _keys++;

                }
            }
            // print("Current quantity of keys: " + _keys);

        }
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        private void OnTriggerExit2D(Collider2D other)
        {
            if (useMask)
            {
                int otherMask = 1 << other.gameObject.layer;

                if ((otherMask | enablerLayer) == enablerLayer)
                {
                    _keys--;
                    StartCoroutine(_audioLib.StopLightSFX());
                }
            }

            else
            {
                if (enablerPlayers.Contains(other.transform))
                {
                    _keys--;
                    StartCoroutine(_audioLib.StopLightSFX());
                }
            }
        }
        
        
        
        
        
        
        
        
        
        public override void Trigger()
        {
            throw new System.NotImplementedException();
        }
    }
}