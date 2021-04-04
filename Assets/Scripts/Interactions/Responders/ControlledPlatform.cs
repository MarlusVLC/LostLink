using System;
using System.Collections;
using UnityEngine;

namespace Aux_Classes
{
    public class ControlledPlatform : Responder
    {
        
        private enum MovementType
        {
            useTime,
            useSpeed
        }

        [SerializeField] private MovementType movementType;
        [SerializeField] private float timeLimit;
        [SerializeField] private float speed;

        [Space] [Header("Collision/Space related parameters")]
        [SerializeField] private Vector2 collisionDetectorSize;
        [SerializeField] private Vector2 collisionDetectorOffset;
        [SerializeField] private Vector2 collisionDetectorBorderThickness;
        [SerializeField] private LayerMask detectableLayers;
        
        private Vector2 goalDist;
        private Vector2 _target;
        private Vector2 _dist;
        private Vector2 _initialPos;

        // private bool _detectedCollider;
        // private bool  _UPdetectedCollider;
        // private bool  _DOWNdetectedCollider;
        // private bool  _RIGHTdetectedCollider;
        // private bool  _LEFTdetectedCollider;
        //
        // private Vector2 _UPdetectorCenter;
        // private Vector2 _DOWNdetectorCenter;
        // private Vector2 _RIGHTdetectorCenter;
        // private Vector2 _LEFTdetectorCenter;
        //
        // private Vector2 _UPdetectorSize;
        // private Vector2 _DOWNdetectorSize;
        // private Vector2 _RIGHTdetectorSize;
        // private Vector2 _LEFTdetectorSize;
        
        //1: UP
        //2: DOWN
        //3: RIGHT
        //4: LEFT
        private bool[] _colliderDetectors = new bool[4];
        private Vector2[] _detectorCenters = new Vector2[4];
        private Vector2[] _detectorSizes = new Vector2[4];

        // private int _undetectedLayerMask;

        private bool _isMoving;
        
        private LayerMask _detectableLayers;

        private Rigidbody2D _rb;


        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            
        }

        public override void React(Vector2 messagePosition = new Vector2())
        {
            if (_isMoving)
                return;

            goalDist = messagePosition;
            _initialPos = transform.position;
            _target = _initialPos + goalDist;

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
            float _timer = 0;
            while (_timer < _timeLimit)
            {
                UpdateDetectorSettings();
                
                if (cannotMoveTowardsDirection())
                {
                    yield break;
                }
        
                
                _timer += Time.deltaTime;
                transform.position = (_initialPos) + goalDist * _timer / timeLimit;
                yield return null;
            }
        }
        
        private IEnumerator MoveTowards()
        {
            _isMoving = true;
            
            Vector2 currPos = transform.position;


            
            while (currPos != _target)
            {

                UpdateDetectorSettings();

            
                for (int i = 0; i < _colliderDetectors.Length; i++)
                {
                    _colliderDetectors[i] = Physics2D.OverlapBox(_detectorCenters[i], _detectorSizes[i],
                        0, detectableLayers);
                }
            
                if (cannotMoveTowardsDirection())
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
        
        private IEnumerator MoveTowardsWithRigidbody()
        {
            Vector2 currPos = transform.position;

            while (Vector2.Distance(currPos, _target) > 0.1f)
            {
                _rb.MovePosition(Vector2.MoveTowards(currPos, _target, speed * Time.deltaTime));
                currPos = transform.position;
                yield return null;
            }
        }
        
        // private IEnumerator MoveTowardsVelocity()
        // {
        //     Vector2 currPos = transform.position;
        //
        //     _detectedCollider = Physics2D.OverlapBox((Vector2)transform.position + collisionDetectorOffset,
        //         collisionDetectorSize, 0,detectableLayers);
        //     
        //     while (currPos != _target && !_detectedCollider)
        //     {
        //         _rb.velocity = new Vector2(2,2);
        //         currPos = transform.position;
        //         yield return null;
        //     }
        // }
        
        // private IEnumerator AddForce(Vector2 forceDirection)
        // {
        //     _rb.AddForce(speed * forceDirection.normalized);
        //     
        //     Vector2 currPos = transform.position;
        //
        //     while (Vector2.Distance(currPos, _target) > 0.1f)
        //     {
        //         _rb.MovePosition(Vector2.MoveTowards(currPos, _target, speed * Time.deltaTime));
        //         currPos = transform.position;
        //         yield return null;
        //     }
        // }
        

        private void OnDrawGizmos()
        {
            if (Application.isEditor)
            {
                UpdateDetectorSettings();

                for (int i = 0; i < _colliderDetectors.Length; i++)
                {
                    _colliderDetectors[i] = Physics2D.OverlapBox(_detectorCenters[i], _detectorSizes[i],
                        0, detectableLayers);
                }
    
            }

            for (int i = 0; i < _colliderDetectors.Length; i++)
            {
                Gizmos.color = _colliderDetectors[i] ? Color.red : Color.blue;
                Gizmos.DrawWireCube(_detectorCenters[i], _detectorSizes[i]);
            }
            
            
        }


        private void UpdateDetectorSettings()
        {


        Vector2 currPos = transform.position;
        
        _detectorCenters[0].x = collisionDetectorOffset.x + currPos.x;
        _detectorCenters[0].y = collisionDetectorOffset.y + currPos.y + collisionDetectorSize.y/2;
        _detectorSizes[0].x = collisionDetectorSize.x;
        _detectorSizes[0].y = collisionDetectorBorderThickness.y;
        
        _detectorCenters[1].x = collisionDetectorOffset.x + currPos.x;
        _detectorCenters[1].y = collisionDetectorOffset.y + currPos.y - collisionDetectorSize.y/2;
        _detectorSizes[1].x = collisionDetectorSize.x;
        _detectorSizes[1].y = collisionDetectorBorderThickness.y;
        
        
        _detectorCenters[3].x = collisionDetectorOffset.x + currPos.x - collisionDetectorSize.x/2;
        _detectorCenters[3].y = collisionDetectorOffset.y + currPos.y;
        _detectorSizes[3].x = collisionDetectorBorderThickness.x;
        _detectorSizes[3].y = collisionDetectorSize.y;
        
        _detectorCenters[2].x = collisionDetectorOffset.x + currPos.x + collisionDetectorSize.x/2;
        _detectorCenters[2].y = collisionDetectorOffset.y + currPos.y;
        _detectorSizes[2].x = collisionDetectorBorderThickness.x;
        _detectorSizes[2].y = collisionDetectorSize.y;
        }

        private bool cannotMoveTowardsDirection()
        {
            return (
                (goalDist.y > 0 && _colliderDetectors[0]) ||
                (goalDist.y < 0 && _colliderDetectors[1]) ||
                (goalDist.x > 0 && _colliderDetectors[2]) ||
                (goalDist.x < 0 && _colliderDetectors[3])
            );
        }

        public bool isBlocked(int i)
        {
            return _colliderDetectors[i];
        }



    }
}
