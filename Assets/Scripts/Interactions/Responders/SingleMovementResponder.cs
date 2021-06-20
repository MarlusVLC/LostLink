using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Responders
{
    public class SingleMovementResponder : Responder
    {
        [SerializeField] private Vector2 moveGoal;
        [SerializeField] private bool useRelativeGoal;
        [SerializeField] private bool ignoreX;
        [SerializeField] private bool ignoreY;
        [SerializeField] private float lerpTime;
        [SerializeField] private bool isLinear;
        [SerializeField] private bool playSound;
        [Space(11)] 
        [SerializeField] private bool FINAL_ASCENSION;
        


        private Vector2 _initialPos;
        
        private bool _canReturn;
        private bool _isInterpolating;




        
        public override void React(Vector2 messagePosition = new Vector2())
        {
            if (FINAL_ASCENSION)
            {
                StartCoroutine(ExecuteFinalAscension());
            }
            
            if (!_isInterpolating)
            {
                if (playSound) _audioLib.SlidingDoorOpeningSFX();
                StartCoroutine(useRelativeGoal
                    ? InterpolatingMovement(_canReturn ? -moveGoal : moveGoal, lerpTime)
                    : InterpolatingMovement(_canReturn ? _initialPos : moveGoal, lerpTime));
            }
        }


        private IEnumerator ExecuteFinalAscension()
        {
            var players = GameObject.FindGameObjectsWithTag("Player");
            print(players[0].name);
            Array.ForEach(players, DisableMovement);
            Aux_Classes.SceneManager sceneManager;
            yield return new WaitForSeconds(10f);

            if (TryGetComponent(out sceneManager))
            {
                print("players[0].name");

                sceneManager.ShowCredits();
            }
        }
        
        private void DisableMovement(GameObject player)
        {
            player.GetComponent<MainMovement>().enabled = false;
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