using System;
using UnityEngine;

namespace Aux_Classes
{
    public class HorizontalPlatform : Responder
    {
        private AnimationClip a;

        
        private Animator _anim;
        private int _animOn, _animOff;

        private void Awake()
        {
            _anim = GetComponent<Animator>();
            _animOn = Animator.StringToHash("On");
            _animOff = Animator.StringToHash("Off");
        }
        
        public override void React(Vector2 messagePosition = new Vector2())
        {
            _anim.SetTrigger(_animOn);
        }
    }
}