using System;
using System.Collections;
using UnityEngine;

namespace Aux_Classes
{
    public class HorizontalPlatform : Responder
    {

        [SerializeField] private Vector2 moveGoal;
        [SerializeField] private float lerpTime;
        [SerializeField] private bool isLinear;
        
        private Animator _anim;
        private int _animOn, _animOff;

        private Vector2 _initialPos;
        private bool _canReturn;
        private bool _isInterpolating;



        // private float currFrame = 0.0f;


        private void Awake()
        {
            _anim = GetComponent<Animator>();
            // _initialPos = transform.position;
            // _animOn = Animator.StringToHash("On");
            // _animOff = Animator.StringToHash("Off");
        }
        
        public override void React(Vector2 messagePosition = new Vector2())
        {
            // _anim.SetTrigger(_animOn);
            if (!_isInterpolating)
            {
                StartCoroutine(LinearlyInterpolate(_canReturn ? -moveGoal : moveGoal, lerpTime));
            }
        }

        public void Update()
        {
            // print(Vector2.Lerp(new Vector2(0,0), new Vector2(1,1), currFrame));
            // print(new Vector2(0,0) * (1-currFrame) + new Vector2(1,1)*currFrame);
            // transform.localPosition = Vector2.Lerdp(new Vector2(0, 0), new Vector2(1, 1), currFrame);
        }


        private IEnumerator LinearlyInterpolate(Vector2 endPos, float lerpTime)
        {
            _isInterpolating = true;
            
            Vector2 startPos = transform.position;
            var timer = 0.0f;
            float t;
            print("POSICAO: " + startPos);
            endPos = startPos + endPos;
            print("ENDPOS: " + endPos);
        
            // var interpolationCounter = 1 / numberOfFrames;
            while (timer <= lerpTime)
            {
                timer += Time.deltaTime;
                t = timer / lerpTime;
                if (!isLinear)
                {
                    t = t * t * (3f - 2f * t);

                }
                transform.position = Vector2.Lerp(startPos, endPos, t);
                yield return null;
            }
            transform.position = endPos;
            _canReturn = !_canReturn;
            print("Can return: " + _canReturn);

            _isInterpolating = false;
        }

        public void LERPReturn()
        {
            if (!_isInterpolating)
            {
                StartCoroutine(LinearlyInterpolate(-moveGoal, lerpTime));
            }
        }
        
        public bool CanReturn
        {
            get => _canReturn;
            set => _canReturn = value;
        }
        
        public bool IsInterpolating => _isInterpolating;

    }
}