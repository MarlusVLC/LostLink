using Unity.Collections;
using UnityEngine;

namespace Aux_Classes
{
    public class RespawnState : ScriptableObject
    {
        private Vector3 position;
        private Vector3 velocity;
        private float rotation;

        public RespawnState(Vector3 position, Vector3 velocity, float rotation)
        {
            this.position = position;
            this.velocity = velocity;
            this.rotation = rotation;
        }
        
        public Vector3 Position
        {
            get { return position; }
        }
        
        public Vector3 Velocity
        {
            get { return velocity; }
        }
        
        public float Rotation
        {
            get { return rotation; }
        }
    }
}