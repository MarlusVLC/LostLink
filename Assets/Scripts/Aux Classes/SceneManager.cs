using UnityEngine;

namespace Aux_Classes
{
    public class SceneManager : MonoBehaviour
    {
        public void PlayGame()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("FINAL");
        }
    }
}