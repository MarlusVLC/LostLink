using System;
using System.Collections;
using UnityEngine;

namespace Aux_Classes
{
    public class SingleMovementResponder : Responder
    {

        [SerializeField] private Vector2 moveGoal;
        [SerializeField] private bool useRelativeGoal;
        [SerializeField] private float lerpTime;
        [SerializeField] private bool isLinear;
        

        private Vector2 _initialPos;
        private bool _canReturn;
        private bool _isInterpolating;




        
        public override void React(Vector2 messagePosition = new Vector2())
        {
            if (!_isInterpolating)
            {
                StartCoroutine(LinearlyInterpolate(_canReturn ? -moveGoal : moveGoal, lerpTime));
            }
        }
        


        private IEnumerator LinearlyInterpolate(Vector2 endPos, float lerpTime)
        {
            _isInterpolating = true;
            
            Vector2 startPos = transform.position;
            var timer = 0.0f;
            float t;
            print("POSICAO: " + startPos);
            if (useRelativeGoal)
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