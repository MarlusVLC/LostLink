using System;
using System.Collections.Generic;
using System.Linq;
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
    
        protected bool _canPress;
        
        protected int _insiderLayer;
        protected List<Transform> _insidersTransform;
        
    
        private void Awake()
        {
            _canPress = false;
            _insiderLayer = 0;
        } 
    
        void Start()
        {
            _anim = transform.GetComponentInParent<Animator>();
        }
        


        private void OnTriggerEnter2D(Collider2D other)
        {
            // ( enablerPlayer.Contains(other.transform) || 

            if (useMask)
            {
                int otherMask = 1 << other.gameObject.layer;
            
                print(other.gameObject.name + " has entered");
            
                if (otherMask == enablerLayer || (otherMask | _insiderLayer) == enablerLayer)
                {
                    _anim.SetTrigger("ToggleOn");
                    _canPress = true;
                    // _insiderLayer |= otherMask;
                    return;
                }
            
                if (enablerLayer == (enablerLayer | (otherMask)))
                {
                    _insiderLayer |= otherMask;
                }
            }
            
            else
            
            {
                Transform otherTrans = other.transform;
                
                if (enablerPlayers.Contains(otherTrans))
                {
                    _insidersTransform.Add(otherTrans);
                }
                
                if (enablerPlayers.Length == 1 && enablerPlayers[0] == otherTrans
                    || _insidersTransform.Count == enablerPlayers.Length)
                {
                    _anim.SetTrigger("ToggleOn");
                    _canPress = true;
                }
            }

            
            
            
            
            
        }
        
        private void OnTriggerExit2D(Collider2D other)
        {
            if (useMask)
            {
                int otherMask = 1 << other.gameObject.layer;
            
                print(other.gameObject.name + " has left");
            
                if (enablerLayer == (enablerLayer | (otherMask)))
                {

                    _insiderLayer ^= otherMask;
                }

                if (_insiderLayer != enablerLayer)
                {
                    _anim.SetTrigger("ToggleOff");
                    _canPress = false;
                }
            }

            else
            {
                Transform otherTrans = other.transform;
                
                if (enablerPlayers.Contains(otherTrans))
                {
                    _insidersTransform.Remove(otherTrans);
                }
                
                if (_insidersTransform.Count < enablerPlayers.Length)
                {
                    _anim.SetTrigger("ToggleOff");
                    _canPress = false;
                } 
            }

        }
        
        public override void Trigger()
        {
            throw new System.NotImplementedException();
        }
    }
}