using UnityEngine;

namespace Interactions.Interactables
{
    public class InstantLight : SleeperLight
    {
        [Header("Moving message related features")]
        [Tooltip("Enable InputPosition if you are passing a position for the Responders to move to")]
        [SerializeField] private bool inputPosition;
        [Tooltip("Position relative to the responders. Goal to where they should go when Interact() is" +
                 "called")]
        [SerializeField] private Vector2 relativeGoal;
        
        public override void Trigger()
        {
            if (_canPress)
            {
                if (inputPosition)
                {
                    Interact(relativeGoal);
                }
                else
                {
                    Interact();
                }
                _anim.SetTrigger("Interact");
            }    
        }
    }
}