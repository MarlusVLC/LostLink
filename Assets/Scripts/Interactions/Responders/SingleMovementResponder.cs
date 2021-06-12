using System;
using System.Collections;
using UnityEngine;

namespace Aux_Classes
{
    public class SingleMovementResponder : Responder
    {
        [SerializeField] private Vector2 moveGoal;
        [SerializeField] private bool useRelativeGoal;
        [SerializeField] private bool ignoreX;
        [SerializeField] private bool ignoreY;
        [SerializeField] private float lerpTime;
        [SerializeField] private bool isLinear;

        

        private Vector2 _initialPos;
        
        private bool _canReturn;
        private bool _isInterpolating;




        
        public override void React(Vector2 messagePosition = new Vector2())
        {
            if (!_isInterpolating)
            {
                StartCoroutine(useRelativeGoal
                    ? InterpolatingMovement(_canReturn ? -moveGoal : moveGoal, lerpTime)
                    : InterpolatingMovement(_canReturn ? _initialPos : moveGoal, lerpTime));
            }
        }
        


        private IEnumerator InterpolatingMovement(Vector2 endPos, float lerpTime)
        {
            _isInterpolating = true;
            
            _initialPos = transform.position;
            var timer = 0.0f;
            float t;
            // print("POSICAO: " + startPos);
            if (useRelativeGoal)
                endPos = _initialPos + endPos;
            endPos.x = ignoreX ? transform.position.x : endPos.x;
            endPos.y = ignoreY ? transform.position.y : endPos.y;
            // print("ENDPOS: " + endPos);
        
            // var interpolationCounter = 1 / numberOfFrames;
            while (timer <= lerpTime)
            {
                timer += Time.deltaTime;
                t = timer / lerpTime;
                if (!isLinear)
                {
                    t = t * t * (3f - 2f * t);

                }
                transform.position = Vector2.Lerp(_initialPos, endPos, t);
                yield return null;
            }
            transform.position = endPos;
            _canReturn = !_canReturn;
            // print("Can return: " + _canReturn);

            _isInterpolating = false;
        }

        public void LERPReturn()
        {
            if (!_isInterpolating)
            {
                StartCoroutine(InterpolatingMovement(-moveGoal, lerpTime));
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