using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Aux_Classes
{
    [DisallowMultipleComponent]
    public abstract class Responder : MonoBehaviour
    {
        //TODO: Perguntar pro Breno se há um meio melhor
        public abstract void React(Vector2 messagePosition = new Vector2());

        // public abstract void React();
    }
}

