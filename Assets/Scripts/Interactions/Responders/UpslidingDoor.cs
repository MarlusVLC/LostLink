using System;
using UnityEngine;

namespace Aux_Classes
{
    public class UpslidingDoor : Responder
    {
        private enum IdleState
        {
            Open,
            Closed
        }

        [SerializeField] private bool startOpen = false; 
        
        private Animator _anim;
        private int _animOpen, _animClose, _animIdle;
        private IdleState _idleState;

        private void Awake()
        {
            _anim = GetComponent<Animator>();
            _animIdle = Animator.StringToHash("idle");
            _animOpen = Animator.StringToHash("Open");
            _animClose = Animator.StringToHash("Close");

            if (startOpen)
            {
                _anim.SetTrigger(_animOpen);
            }
        }

        public override void React(Vector2 messagePosition = new Vector2())
        {
            AnimatorStateInfo _currentAnim = _anim.GetCurrentAnimatorStateInfo(0);
            if (_currentAnim.normalizedTime >= 1)
            {
                if (_currentAnim.shortNameHash == _animOpen)
                {
                    _anim.SetTrigger(_animClose);
                }
                else if (_currentAnim.shortNameHash == _animClose || _currentAnim.shortNameHash == _animIdle)
                {
                    _anim.SetTrigger(_animOpen);
                }
                
                return;
            }

            if (_currentAnim.shortNameHash == _animIdle)
            {
                _anim.SetTrigger(_animOpen);
            }
        }
        
        
    }
}