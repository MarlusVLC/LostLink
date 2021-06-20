using UnityEngine;

namespace Aux_Classes
{
    public class SceneManager : MonoBehaviour
    {
        public void PlayGame()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("FINAL");
        }

        public void ShowCredits()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("FINAL CREDITOS");
        }

        public void StartAgain()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("FINAL MENU");
        }
    }
}