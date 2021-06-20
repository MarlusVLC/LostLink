using System;
using System.Collections;
using System.Security.Cryptography;
using Audio;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Responders
{
    public class ScreenFade : Responder
    {
        [SerializeField] private float fadeTime;
        [SerializeField] private GameObject[] cameras;
        [SerializeField] private GameObject[] players;


        private FadeCamera _fadeCameraA;
        private FadeCamera _fadeCameraB;

        
        public void Awake()
        {
            _fadeCameraA = cameras[0].GetComponent<FadeCamera>();
            _fadeCameraB = cameras[1].GetComponent<FadeCamera>();
        }

        public override void React(Vector2 messagePosition = new Vector2())
        {
            Array.ForEach(players, DisableMovement);
            _fadeCameraA.FadeOut(fadeTime);
            _fadeCameraB.FadeOut(fadeTime);

        }

        // private IEnumerator TeleportGo(float time)
        // {
        //     Array.ForEach(players, DisableMovement);
        //     yield return new WaitForSeconds(time);
        //     StartCoroutine(InterpolateShader(_dissolvingMaterial, 0, 1, 4));
        //     // _audioLib.TeleportSFX();
        //     yield return new WaitForSeconds(4.1f);
        //     DestroyPlayers();
        //     SceneManager.LoadScene((SceneManager.GetActiveScene().buildIndex + 1)%SceneManager.sceneCountInBuildSettings);
        // }






        private void DisableMovement(GameObject player)
        {
            player.GetComponent<MainMovement>().enabled = false;
        }

        private void EnableMovement(GameObject player)
        {
            player.GetComponent<MainMovement>().enabled = true;
        }
    }
}