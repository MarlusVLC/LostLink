using UnityEngine;

namespace Aux_Classes
{
    [ExecuteAlways]
    public class PixelPerfectMove : MonoBehaviour
    {
        private void Update()
        {
            transform.position = Vector3Int.RoundToInt(transform.position);
        }
    }
}