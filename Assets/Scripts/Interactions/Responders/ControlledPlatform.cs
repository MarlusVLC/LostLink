using System;
using System.Collections;
using Aux_Classes;
using UnityEngine;
using UnityEngine.UIElements;

namespace Responders
{
    
    [RequireComponent(typeof(PositionCheckingBox))]
    public class ControlledPlatform : Responder
    {
        
        private enum MovementType
        {
            useTime,
            useSpeed
        }

        [SerializeField] private MovementType movementType;
        [SerializeField] private bool useRelativePosition;
        [SerializeField] private float timeLimit;
        [SerializeField] private float speed;

        private PositionCheckingBox _checkingBox;
        
        private Vector2 goalDist;
        private Vector2 _target;
        private Vector2 _dist;
        private Vector2 _initialPos;
        
        private bool _isMoving;
        

        private void Awake()
        {
            _checkingBox = GetComponent<PositionCheckingBox>();
        }

        public override void React(Vector2 messagePosition = new Vector2())
        {
            if (_isMoving)
                return;

            goalDist = messagePosition;
            _initialPos = transform.position;
            _target = useRelativePosition ? _initialPos + goalDist : goalDist;

            switch (movementType)
            {
                case MovementType.useSpeed:
                    StartCoroutine(MoveTowards());
                    break;
                case MovementType.useTime:
                    StartCoroutine(MoveTowards(timeLimit));
                    break;
            }

        }

        
        
        private IEnumerator MoveTowards(float _timeLimit)
        {
            _isMoving = true;
            float _timer = 0;
            while (_timer < _timeLimit)
            {
                _checkingBox.UpdateDetectorSettings();
                _checkingBox.CheckDetectorsStatus();
                
                if (CannotMoveTowardsDirection())
                {
                    _isMoving = false;
                    yield break;
                }
        
                
                _timer += Time.deltaTime;
                transform.position = (_initialPos) + goalDist * _timer / timeLimit;
                yield return null;
            }
            _isMoving = false;

        }
        
        private IEnumerator MoveTowards()
        {
            _isMoving = true;
            
            Vector2 currPos = transform.position;

            while (currPos != _target)
            {

                _checkingBox.UpdateDetectorSettings();
                _checkingBox.CheckDetectorsStatus();
            
                if (CannotMoveTowardsDirection())
                {
                    _isMoving = false;
                    yield break;
                }
                
                transform.position = Vector2.MoveTowards(currPos, _target, speed * Time.deltaTime);
                currPos = transform.position;
                yield return null;
            }

            _isMoving = false;
        }
        
        
        private bool CannotMoveTowardsDirection()
        {
            return (
                (goalDist.y > 0 && _checkingBox.ColliderDetectors[0]) ||
                (goalDist.y < 0 && _checkingBox.ColliderDetectors[1]) ||
                (goalDist.x > 0 && _checkingBox.ColliderDetectors[2]) ||
                (goalDist.x < 0 && _checkingBox.ColliderDetectors[3])
            );
        }
        
        
        
    }
}
