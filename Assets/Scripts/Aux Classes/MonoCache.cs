using System;
using UnityEditor;
using UnityEngine;

namespace DefaultNamespace
{
    public class MonoCache : MonoBehaviour
    {
        [SerializeField] private bool UpdateTransformOnEditor;

        private Transform _transform;
        public Transform Transform => _transform;

        protected virtual void Awake()
        {
            CacheTransform();
        }

        private void Start()
        {
            OnStart();
        }

        protected virtual void OnValidate()
        {
            if (UpdateTransformOnEditor)
                CacheTransform();
        }

        protected virtual void OnStart()
        {
        }

        private void CacheTransform()
        {
            TryGetComponent(out _transform);
        }
    }
}