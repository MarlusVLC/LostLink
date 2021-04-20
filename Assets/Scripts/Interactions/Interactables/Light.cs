using System.Linq;
using UnityEngine;

namespace Interactions.Interactables
{
    public class Light : Interactable
    {
        [SerializeField] private Transform[] enablerPlayer;
        [SerializeField] private LayerMask enablerLayer;
    
        private BoxCollider2D _presenceDetector;
        protected Animator _anim;
    
        protected bool _canPress;
    
        private void Awake()
        {
            _canPress = false;
        } 
    
        void Start()
        {
            _anim = transform.GetComponentInParent<Animator>();
        }
        
    
        private void OnTriggerEnter2D(Collider2D other)
        {
            if ( enablerPlayer.Contains(other.transform) || 
                 enablerLayer == (enablerLayer | (1 << other.gameObject.layer)))
            {
                _anim.SetTrigger("ToggleOn");
                _canPress = true;
            }
        }
        
        private void OnTriggerExit2D(Collider2D other)
        {
            if (enablerPlayer.Contains(other.transform) || 
                enablerLayer == (enablerLayer | (1 << other.gameObject.layer)))
            {
                _anim.SetTrigger("ToggleOff");
                _canPress = false;
            }
        }
        
        public override void Trigger()
        {
            throw new System.NotImplementedException();
        }
    }
}