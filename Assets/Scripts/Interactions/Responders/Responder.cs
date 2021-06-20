using System;
using System.Collections;
using System.Collections.Generic;
using Audio;
using UnityEngine;

namespace Responders
{
    [DisallowMultipleComponent]
    public abstract class Responder : MonoBehaviour
    {

        protected AudioLib _audioLib;
        public abstract void React(Vector2 messagePosition = new Vector2());


        public void Awake()
        {
            _audioLib = GetComponent<AudioLib>();
        }
        // public abstract void React();


    }
}

