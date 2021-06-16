using DefaultNamespace;
using UnityEngine;

namespace Responders
{
    public class PersistentGameObject : MonoCache
    {
        protected override void Awake()
        {
            base.Awake();
            // print("persist");
            DontDestroyOnLoad(this.gameObject);
        }
    }
}