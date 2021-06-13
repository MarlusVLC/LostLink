using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Responders
{
    [DisallowMultipleComponent]
    public abstract class Responder : MonoBehaviour
    {
        public abstract void React(Vector2 messagePosition = new Vector2());

        // public abstract void React();


    }
}

