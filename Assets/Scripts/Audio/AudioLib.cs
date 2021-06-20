using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Audio
{
    
    [RequireComponent(typeof(AudioSource))]
    public class AudioLib : MonoBehaviour
    {
        [Header("Character movement sfx:")] [Space(10)] [Header("Audio")] 
        [SerializeField] private AudioClip capa_impacto_grande_finalSFX;
        [SerializeField] private AudioClip capa_transicaoSFX;
        [SerializeField] private AudioClip jump;

        [Header("Door sfx:")] 
        [SerializeField] private AudioClip sliding_doorSFX;

        [Header("Lights sfx:")]  
        [SerializeField] private bool playBlueSound;
        [SerializeField] private AudioClip BLUE_lightSFX;
        [SerializeField] private AudioClip BLUE_light_activateSFX;
        [SerializeField] private bool playRedSound;
        [SerializeField] private AudioClip RED_lightSFX;
        [SerializeField] private AudioClip RED_light_activateSFX;
        
        [Header("Teleport sfx: ")]
        [SerializeField] private AudioClip teleportSFX;
        
        [Header("Wind sfx: ")] 
        [SerializeField] private AudioClip windSFX;
        
        [Header("Background: ")]
        [SerializeField] private AudioClip templeAmbient;
        [SerializeField] private AudioClip natureAmbient;


        
        private AudioSource _audioSource;

        public void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        #region PLAYERS
        
        // character movement sfx
        public void RunSFX()
        {
            _audioSource.PlayOneShot(Random.Range(0.0f, 1.0f) < 0.5 ? capa_impacto_grande_finalSFX : capa_transicaoSFX);
        }

        public void JumpSFX()
        {
            _audioSource.PlayOneShot(jump);
        }
        
        #endregion


        // sliding door sfx
        public void SlidingDoorOpeningSFX()
        {
            _audioSource.PlayOneShot(sliding_doorSFX);
        }

        // lights sfx
        public void LightSFX()
        {
            if (playRedSound)
            {
                if (_audioSource.clip != RED_lightSFX)
                    _audioSource.clip = RED_lightSFX;
                _audioSource.Play(0);
            }
            if (playBlueSound)
            {
                if (_audioSource.clip != BLUE_lightSFX)
                    _audioSource.clip = BLUE_lightSFX;
                _audioSource.Play(0);
            }

        }

        public IEnumerator StopLightSFX()
        {
            if (_audioSource.isPlaying)
                yield return null;
            yield return new WaitWhile(isPlaying);
            _audioSource.Stop();
        }

        private bool isPlaying()
        {
            return _audioSource.volume > 0f;
        }

        public void LightActivateSFX()
        {
            if (playRedSound)
            {
                _audioSource.PlayOneShot(RED_light_activateSFX);

            }
            if(playBlueSound)
            {
                _audioSource.PlayOneShot(BLUE_light_activateSFX);

            }
        }

        public void TeleportSFX()
        {
            _audioSource.PlayOneShot(teleportSFX);
        }

        public void WindSFX()
        {
            if (_audioSource.clip != windSFX)
            {
                _audioSource.clip = windSFX;
            }
            _audioSource.Play(0);
        }

        public void PlayTemple()
        {
            if (_audioSource.clip != templeAmbient)
            {
                _audioSource.clip = templeAmbient;
            }
            _audioSource.Play(0);
        }
    }
}

