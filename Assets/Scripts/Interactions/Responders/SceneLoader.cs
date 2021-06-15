using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Responders
{
    public class SceneLoader : Responder
    {
        [SerializeField] private float waitingTime;
        public override void React(Vector2 messagePosition = new Vector2())
        {
            StartCoroutine(WaitAndLoadScene(waitingTime));
        }

        private IEnumerator WaitAndLoadScene(float time)
        {
            yield return new WaitForSeconds(time);
            SceneManager.LoadScene((SceneManager.GetActiveScene().buildIndex + 1)%SceneManager.sceneCountInBuildSettings);
        }
    }
}