using System;
using System.Collections;
using System.Collections.Generic;
using Aux_Classes;
using UnityEngine;

namespace Interactions.Interactables
{
    public abstract class Interactable : MonoBehaviour
    {
        [SerializeField] protected Responder[] _responders;
        
        
        private void OnValidate()
        {
            MakeFullyInteractable(LayerMask.NameToLayer("Interactable"));
        }


        
        protected void Interact()
        {
            foreach (Responder responder in _responders)
            {
                responder.React();
            }
        }
        
        protected void Interact(Vector2 messageCoordinates)
        {
            foreach (Responder responder in _responders)
            {
                responder.React(messageCoordinates);
            }
        }

        public abstract void Trigger();

        /// <summary>
        /// Guarantee that the GameObject is completely interactable by setting its layerMask to interactable
        /// </summary>
        private void MakeFullyInteractable(int _interactableLayerNum)
        {
            if (!gameObject.layer.Equals(_interactableLayerNum))
            {
                gameObject.layer = _interactableLayerNum;

            }
        }
    }
}

    