using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(SimpleGrid))]
public class SimpleGridEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        SimpleGrid simpleGrid = (SimpleGrid)target;

        if (GUILayout.Button("Reposition"))
        {
            simpleGrid.Reposition();
        }

        if (GUILayout.Button("Rename"))
        {
            simpleGrid.Rename();
        }
    }
}