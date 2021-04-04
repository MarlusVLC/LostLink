using System;
using System.Collections;
using System.Collections.Generic;
using Aux_Classes;
using UnityEngine;

namespace Interactions.Interactables

{
    public class SleeperLight : Interactable
    {
        // [SerializeField] private float[] Responders;
        [SerializeField] private Transform enablerPlayer;
    
        private BoxCollider2D _presenceDetector;
        protected Animator _anim;
        // private Animator _affectedAnim;
    
        protected bool _canPress;
    
        private void Awake()
        {
            _canPress = false;
        }
    
        void Start()
        {
            // _presenceDetector = transform.GetChild(0).GetComponent<BoxCollider2D>();
            _anim = transform.GetComponentInParent<Animator>();
            // _affectedAnim = affectedObject.GetComponent<Animator>();
        }
        
    
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.transform == enablerPlayer)
            {
                _anim.SetTrigger("ToggleOn");
                _canPress = true;
            }
        }
        
        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.transform == enablerPlayer)
            {
                _anim.SetTrigger("ToggleOff");
                _canPress = false;
            }
        }
    
    
        public override void Trigger()
        {
            if (_canPress)
            {
                // _affectedAnim.SetTrigger("On");  //> Isso agora é responsabilidade do responder
                Interact();
                _anim.SetTrigger("Defunct");
            }    
        }
    }
}

