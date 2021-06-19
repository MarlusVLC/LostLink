using UnityEngine;

namespace Aux_Classes
{
    public class PositionCheckingBox : MonoBehaviour
    {
        
        [Space] [Header("Collision/Space related parameters")]
        [SerializeField] private Vector2 collisionDetectorSize;
        [SerializeField] private Vector2 collisionDetectorOffset;
        [SerializeField] private Vector2 collisionDetectorBorderThickness;
        [SerializeField] private LayerMask detectableLayers;
        
        public readonly bool[] ColliderDetectors = new bool[4];
        public readonly Vector2[] DetectorCenters = new Vector2[4];
        public readonly Vector2[] DetectorSizes = new Vector2[4];
        
        
        private void OnDrawGizmos()
        {
            if (Application.isEditor)
            {
                UpdateDetectorSettings();

                CheckDetectorsStatus();
    
            }

            for (int i = 0; i < ColliderDetectors.Length; i++)
            {
                Gizmos.color = ColliderDetectors[i] ? Color.red : Color.blue;
                Gizmos.DrawWireCube(DetectorCenters[i], DetectorSizes[i]);
            }
            
            
        }


        public void UpdateDetectorSettings()
        {
            Vector2 currPos = transform.position;
            
            DetectorCenters[0].x = collisionDetectorOffset.x + currPos.x;
            DetectorCenters[0].y = collisionDetectorOffset.y + currPos.y + collisionDetectorSize.y/2;
            DetectorSizes[0].x = collisionDetectorSize.x;
            DetectorSizes[0].y = collisionDetectorBorderThickness.y;
            
            DetectorCenters[1].x = collisionDetectorOffset.x + currPos.x;
            DetectorCenters[1].y = collisionDetectorOffset.y + currPos.y - collisionDetectorSize.y/2;
            DetectorSizes[1].x = collisionDetectorSize.x;
            DetectorSizes[1].y = collisionDetectorBorderThickness.y;
            
            
            DetectorCenters[3].x = collisionDetectorOffset.x + currPos.x - collisionDetectorSize.x/2;
            DetectorCenters[3].y = collisionDetectorOffset.y + currPos.y;
            DetectorSizes[3].x = collisionDetectorBorderThickness.x;
            DetectorSizes[3].y = collisionDetectorSize.y;
            
            DetectorCenters[2].x = collisionDetectorOffset.x + currPos.x + collisionDetectorSize.x/2;
            DetectorCenters[2].y = collisionDetectorOffset.y + currPos.y;
            DetectorSizes[2].x = collisionDetectorBorderThickness.x;
            DetectorSizes[2].y = collisionDetectorSize.y;
        }



        public bool IsBlocked(int i)
        {
            return ColliderDetectors[i];
        }

        public void CheckDetectorsStatus()
        {
            for (int i = 0; i < ColliderDetectors.Length; i++)
            {
                ColliderDetectors[i] = Physics2D.OverlapBox(DetectorCenters[i], DetectorSizes[i],
                    0, detectableLayers);
            }
        }
        
        
    }
}