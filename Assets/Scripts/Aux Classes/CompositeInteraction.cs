using System.Collections;
using System.Collections.Generic;
using Aux_Classes;
using UnityEngine;


//PEGO NO CANAL InfallibleCode 
namespace Aux_Classes
{
    public class CompositeInteraction : MonoBehaviour, IInteractable
    {
        [SerializeField] private List<GameObject> interacbleGameObjects;

        public void Interact()
        {
            foreach (var interactableGameObject in interacbleGameObjects )
            {
                var interactable = interactableGameObject.GetComponent<IInteractable>();
                if (interactable == null) continue;
                interactable.Interact();;
            }
        }
    }

}
