using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ContentWidthFitter))]
public class ContentWidthFitterEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        ContentWidthFitter myTarget = (ContentWidthFitter)target;

        if (GUILayout.Button("Snap"))
        {
            myTarget.GetComponent<ContentWidthFitter>().Snap();
        }
    }
}
