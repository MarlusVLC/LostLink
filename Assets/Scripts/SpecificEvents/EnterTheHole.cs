using System;
using Responders;
using UnityEngine;

namespace SpecificEvents
{
    public class EnterTheHole : MonoBehaviour
    {
        [SerializeField] private string triggerTag;

        private Animator _anim;
        private int _enterHoleTrigger;


        private void Awake()
        {
            _anim = GetComponent<Animator>();
            _enterHoleTrigger = Animator.StringToHash("EnterTheHole");
        }


        private void OnTriggerEnter2D(Collider2D other)
        {
            // print("EnteredTrigger");
            if (other.gameObject.CompareTag(triggerTag))
            {
                // print("EnteredHole");
                Destroy(GetComponent<ControlledPlatform>());
                // _anim.SetTrigger(_enterHoleTrigger);
            }
        }
    }
}