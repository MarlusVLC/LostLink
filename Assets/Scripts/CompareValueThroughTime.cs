using CreativeSpore.SuperTilemapEditor;
using UnityEngine;

namespace DefaultNamespace
{
    public class CompareValueThroughTime : MonoBehaviour
    {
        private int _newValue;
        private int _oldValue;


        private void Update()
        {
            if (hasValuedChanged())
            {
                SetOldValue();
                //Executa o resto
            }
        }


        bool hasValuedChanged()
        {
            return _oldValue != _newValue;
        }

        void SetOldValue()
        {
            _oldValue = _newValue;
        }
    }
}