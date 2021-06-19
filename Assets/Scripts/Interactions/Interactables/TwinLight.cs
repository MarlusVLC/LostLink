using System.Collections;
using UnityEngine;

namespace Interactions.Interactables
{
    public class TwinLight : Light
    {
        [SerializeField] private TwinLight otherLight;
        [SerializeField] private float _answerTimer;
        [SerializeField] private float _currTime;

        void Awake()
        {
            _currTime = 0f;
        }
        
        
        
        public override void Trigger()
        {
            if (_canPress)
            {
                if (_currTime > 0)
                {
                    Interact();
                    _anim.SetTrigger("Defunct");
                    otherLight.Cease();
                    return;
                }
                _anim.SetTrigger("Interact");
                StartCoroutine(otherLight.StartTimer());
            }    
        }


        public IEnumerator StartTimer()
        {
            _currTime = _answerTimer;

            while (_currTime > 0)
            {
                _currTime -= Time.deltaTime;
                yield return null;
            }
        }


        public void Cease()
        {
            _anim.SetTrigger("Defunct");
        }
    }
}