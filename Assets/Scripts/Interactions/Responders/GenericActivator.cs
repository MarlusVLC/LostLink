using System;
using System.Collections;
using UnityEngine;

namespace Responders
{
    public class GenericActivator : Responder
    {
        [SerializeField] private MonoBehaviour[] componentsToBeActivated;
        
        public override void React(Vector2 messagePosition = new Vector2())
        {
            Array.ForEach(componentsToBeActivated, c => c.enabled = true);
        }
    }
}