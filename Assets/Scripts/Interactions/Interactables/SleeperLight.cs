using System;
using System.Collections;
using System.Collections.Generic;
using Aux_Classes;
using UnityEngine;

namespace Interactions.Interactables

{
    public class SleeperLight : Light
    {

        [SerializeField] private bool destroyOnExit;

        private bool _isPlayingDefunctAnim;
    
        public override void Trigger()
        {
            if (_canPress)
            {
                // _affectedAnim.SetTrigger("On");  //> Isso agora é responsabilidade do responder
                Interact();
                _isPlayingDefunctAnim = true;
                _audioLib.LightActivateSFX();
                _anim.SetTrigger("Defunct");
            }    
        }

        private void OnDisable()
        {
            if (destroyOnExit && _isPlayingDefunctAnim)
                Destroy(gameObject);
        }
    }
}

