using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ContentHeightFitter))]
public class ContentHeightFitterEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        ContentHeightFitter myTarget = (ContentHeightFitter)target;

        if (GUILayout.Button("Snap"))
        {
            myTarget.GetComponent<ContentHeightFitter>().Snap();
        }
    }
}
