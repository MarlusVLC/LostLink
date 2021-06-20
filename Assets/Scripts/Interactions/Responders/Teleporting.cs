using System;
using System.Collections;
using System.Security.Cryptography;
using Audio;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Responders
{
    public class Teleporting : Responder
    {
        [SerializeField] private float waitingTime;
        [SerializeField] private GameObject[] players;
        

        private Material _dissolvingMaterial;
        private AudioLib _audioLib;

        public void Awake()
        {
            _audioLib = GetComponent<AudioLib>();
            _dissolvingMaterial = players[0].GetComponent<SpriteRenderer>().sharedMaterial;
            if (_dissolvingMaterial.GetFloat("DissolveAmount") > 0.9f)
            {
                StartCoroutine(TeleportCome(waitingTime));
            }
        }

        public override void React(Vector2 messagePosition = new Vector2())
        {
            StartCoroutine(TeleportGo(waitingTime));
        }

        private IEnumerator TeleportGo(float time)
        {
            Array.ForEach(players, DisableMovement);
            yield return new WaitForSeconds(time);
            StartCoroutine(InterpolateShader(_dissolvingMaterial, 0, 1, 4));
            // _audioLib.TeleportSFX();
            yield return new WaitForSeconds(4.1f);
            DestroyPlayers();
            SceneManager.LoadScene((SceneManager.GetActiveScene().buildIndex + 1)%SceneManager.sceneCountInBuildSettings);
        }

        private IEnumerator TeleportCome(float time)
        {
            Array.ForEach(players, DisableMovement);
            yield return new WaitForSeconds(time);
            StartCoroutine(InterpolateShader(_dissolvingMaterial, 1, 0, 4));
            yield return new WaitForSeconds(4.1f);
            Array.ForEach(players, EnableMovement);


        }
        private void DestroyPlayers()
        {
            Array.ForEach(players, Destroy);
        }
        

        private void DisableMovement(GameObject player)
        {
            player.GetComponent<MainMovement>().enabled = false;
        }
        
        private void EnableMovement(GameObject player)
        {
            player.GetComponent<MainMovement>().enabled = true;
        }
        
        public static IEnumerator InterpolateShader(Material material, float startValue, float endValue, float lerpTime)
        {

            var timer = 0.0f;
            float t;
            while (timer <= lerpTime)
            {
                timer += Time.deltaTime;
                t = timer / lerpTime;
                material.SetFloat("DissolveAmount", Mathf.Lerp(startValue, endValue, t));
                yield return null;
            }
            material.SetFloat("DissolveAmount", endValue);
        }

    }
}