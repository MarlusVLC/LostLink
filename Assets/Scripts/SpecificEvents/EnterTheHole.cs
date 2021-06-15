using System;
using System.Linq;
using Aux_Classes;
using Interactions.Interactables;
using Responders;
using UnityEngine;

namespace SpecificEvents
{
    public class EnterTheHole : MonoBehaviour
    {
        [SerializeField] private string triggerTag;
        [SerializeField] private GameObject[] blockedWindEffectors;
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag(triggerTag))
            {
                Destroy(GetComponent<ControlledPlatform>());
                blockedWindEffectors.Where(s => s && !s.activeSelf).ForEach(s => s.SetActive(true));
                Destroy(other.gameObject);
            }
        }
    }
}