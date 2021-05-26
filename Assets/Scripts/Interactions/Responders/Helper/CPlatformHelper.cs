using System;
using UnityEngine;

namespace Aux_Classes
{
    public class CPlatformHelper : MonoBehaviour
    {

        private PositionCheckingBox _checkingBox;
        
        private enum Direction
        {
            UP, 
            DOWN,
            RIGHT,
            LEFT,
        }

        [SerializeField] private Sprite positiveSign;
        [SerializeField] private Sprite negativeSign;
        [SerializeField] private Direction direction; 
        
        //0: Up
        //1: Down
        //2: Right
        //3: Left
        private SpriteRenderer[] signRenderers;
        private Color _bgColor;

        private ControlledPlatform _mainControlScript;

        private bool _detected;

        private void Awake()
        {
            _bgColor = transform.GetComponent<SpriteRenderer>().color;
            _checkingBox = GetComponent<PositionCheckingBox>();
        }

        private void Start()
        {
            _mainControlScript = GetComponent<ControlledPlatform>();

            signRenderers = new SpriteRenderer[transform.childCount];

            for (int i = 0; i < signRenderers.Length; i++)
            {
                signRenderers[i] = transform.GetChild(i).GetComponent<SpriteRenderer>();
            }
        }

        private void Update()
        {
            UpdateSprites();
        }

        private void UpdateSprites()
        {
            for (int i = 0; i < signRenderers.Length; i++)
            {
                if (_checkingBox.IsBlocked(i) && signRenderers[i].color == Color.white)
                {
                    Debug.Log(i.ToString());
                    signRenderers[i].color = _bgColor;
                }
                else if (!_checkingBox.IsBlocked(i) && signRenderers[i].color == _bgColor)
                {
                    signRenderers[i].color = Color.white;
                }
            }
        }
    }
}