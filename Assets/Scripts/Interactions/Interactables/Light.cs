using System;
using System.Collections.Generic;
using System.Linq;
using Aux_Classes;
using UnityEngine;

namespace Interactions.Interactables
{
    public class Light : Interactable
    {
        [SerializeField] private Transform[] enablerPlayers;
        [SerializeField] private LayerMask enablerLayer;
        [SerializeField] private bool useMask = true;
        [SerializeField] private AudioLib audioLib;

        private BoxCollider2D _presenceDetector;
        protected Animator _anim;
    
        [Space(11)]
        protected bool _canPress;
        protected int _keys;
        protected int _keySet;
        
    
        void Start()
        {
            _canPress = false;
            _anim = transform.GetComponentInParent<Animator>();
            if (!_anim)
                TryGetComponent(out _anim);
            _keySet = useMask ? enablerLayer.value.CountSetBits() : enablerPlayers.Length ;


            audioLib = GetComponent<AudioLib>();
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
                }
            }
            
            else
            {
                if (enablerPlayers.Contains(other.transform))
                    _keys++;
            }

            if (audioLib)
            {
                audioLib.LightSFX();
            }
        }
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        private void OnTriggerExit2D(Collider2D other)
        {
            if (useMask)
            {
                int otherMask = 1 << other.gameObject.layer;

                if ((otherMask | enablerLayer) == enablerLayer)
                {
                    _keys--;
                }
            }

            else
            {
                if (enablerPlayers.Contains(other.transform))
                {
                    _keys--;
                }
            }

            if (audioLib)
            {
                StartCoroutine(audioLib.StopLightSFX());
            }
        }
        
        
        
        
        
        
        
        
        
        public override void Trigger()
        {
            throw new System.NotImplementedException();
        }
    }
}
