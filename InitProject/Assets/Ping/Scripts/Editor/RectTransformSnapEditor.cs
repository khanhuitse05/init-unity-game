using UnityEngine;
using UnityEditor;
using System.Collections;


namespace Ping
{
    [CustomEditor(typeof(RectTransformSnap))]
    class RectTransformSnapEditor : Editor
    {

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            RectTransformSnap myTarget = (RectTransformSnap)target;

            if (GUILayout.Button("Snap"))
            {
                myTarget.GetComponent<RectTransformSnap>().Snap();
            }
        }
    }
}