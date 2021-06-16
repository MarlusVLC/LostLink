using System;
using System.Collections;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Responders
{
    public class SceneLoader : Responder
    {
        [SerializeField] private float waitingTime;
        [SerializeField] private GameObject[] destroyables;

        public override void React(Vector2 messagePosition = new Vector2())
        {
            StartCoroutine(WaitAndLoadScene(waitingTime));
        }

        private IEnumerator WaitAndLoadScene(float time)
        {
            yield return new WaitForSeconds(time);
            DestroyThisBitches();
            SceneManager.LoadScene((SceneManager.GetActiveScene().buildIndex + 1)%SceneManager.sceneCountInBuildSettings);
        }

        private void DestroyThisBitches()
        {
            Array.ForEach(destroyables, Destroy);
        }
        
        
    }
}