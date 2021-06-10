using System;
using SpecificEvents;
using UnityEditor;
using UnityEngine;


namespace MyEditor
{
    [CustomEditor(typeof(ChangeCameras))]
    public class CameraChanger : Editor
    {
        private ChangeCameras changer;
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            changer = (ChangeCameras) target;
            if (GUILayout.Button("Set Split Screen"))
            {
                changer.SetSplitScreen();
            }
        }
    }
}