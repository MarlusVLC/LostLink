using Interactions.Interactables;
using UnityEngine;

public class PlayerCommands : MonoBehaviour
{
    [SerializeField] private string controls;
    [SerializeField] private Vector2 detectorBoxSize;
    
    private Collider2D _detectedCollider;
    private Interactable _interactable;
    void Awake()
    {
        _interactable = null;
    }

    void Update()
    {
        _detectedCollider = Physics2D.OverlapBox(transform.position, detectorBoxSize, 0,
            1 << LayerMask.NameToLayer("Interactable"));

        if (_detectedCollider)
        {
            if (_interactable == null)
            {
                _interactable = _detectedCollider.transform.GetComponent<Interactable>();
            }
        }
        else
        {
            _interactable = null;
        }

        if (_interactable != null)
        {
            ActUpon(_interactable);
        }   
        
    }

    private void ActUpon(Interactable interactable)
    {
        if (interactable.HasContinuousInteraction)
        {
            if (Input.GetButton("Interact" + controls))
            {
                interactable.Trigger();
            }
        }
        else
        {
            if (Input.GetButtonDown("Interact" + controls))
            {
                interactable.Trigger();
            }
        }

    }
    

    private void OnDrawGizmos()
    {
        Gizmos.color = gameObject.GetComponent<SpriteRenderer>().color;
        Gizmos.DrawWireCube(transform.position, detectorBoxSize);
    }
}
