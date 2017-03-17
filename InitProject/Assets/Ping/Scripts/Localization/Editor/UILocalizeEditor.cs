using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(UILocalize)), CanEditMultipleObjects]
public class UILocalizeEditor : Editor
{
    public SerializedProperty textProp;
    public SerializedProperty stringType;
    private List<string> mKeys;

    private void OnEnable()
    {
        LocalizationData.LoadLocalization();
        textProp = serializedObject.FindProperty("key");
        stringType = serializedObject.FindProperty("type");

        Dictionary<string, string[]> dict = Localization.dictionary;

        if (dict.Count > 0)
        {
            mKeys = new List<string>();

            foreach (KeyValuePair<string, string[]> pair in dict)
            {
                if (pair.Key == "KEY") continue;
                mKeys.Add(pair.Key);
            }
            mKeys.Sort(delegate(string left, string right) { return left.CompareTo(right); });
        }
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        GUILayout.Space(6f);
        EditorGUIUtility.labelWidth = 80f;
        GUILayout.BeginHorizontal();
        EditorGUILayout.PropertyField(textProp);
        string myKey = textProp.stringValue;
        bool isPresent = (mKeys != null) && mKeys.Contains(myKey);
        GUI.color = isPresent ? Color.green : Color.red;
        GUILayout.BeginVertical(GUILayout.Width(22f));
        GUILayout.Space(2f);
        GUILayout.Label(isPresent ? "\u2714" : "\u2718", "TL SelectionButtonNew", GUILayout.Height(20f));
        GUILayout.EndVertical();
        GUI.color = Color.white;
        GUILayout.EndHorizontal();

        EditorGUILayout.PropertyField(stringType);

        if (isPresent)
        {
            EditorGUILayout.LabelField("Preview");
            string[] keys = Localization.knownLanguages;
            string[] values;

            if (Localization.dictionary.TryGetValue(myKey, out values))
            {
                if (keys.Length != values.Length)
                {
                    EditorGUILayout.HelpBox(
                        "Number of keys doesn't match the number of values! Did you modify the dictionaries by hand at some point?",
                        MessageType.Error);
                }
                else
                {
                    for (int i = 0; i < keys.Length; ++i)
                    {
                        GUILayout.BeginHorizontal();
                        GUILayout.Label(keys[i], GUILayout.Width(66f));

                        if (GUILayout.Button(values[i], "AS TextArea", GUILayout.MinWidth(80f),
                            GUILayout.MaxWidth(Screen.width - 110f), GUILayout.MaxHeight(30)))
                        {
                            (target as UILocalize).value = values[i];
                            GUIUtility.hotControl = 0;
                            GUIUtility.keyboardControl = 0;
                        }
                        GUILayout.EndHorizontal();
                    }
                }
            }
            else GUILayout.Label("No preview available");
        }
        else if (mKeys != null && !string.IsNullOrEmpty(myKey))
        {
            GUILayout.BeginHorizontal();
            GUILayout.Space(80f);
            GUILayout.BeginVertical();
            GUI.backgroundColor = new Color(1f, 1f, 1f, 0.35f);

            int matches = 0;

            for (int i = 0, imax = mKeys.Count; i < imax; ++i)
            {
                if (mKeys[i].StartsWith(myKey, StringComparison.OrdinalIgnoreCase) || mKeys[i].Contains(myKey))
                {
                    if (GUILayout.Button(mKeys[i] + " \u25B2", "CN CountBadge"))
                    {
                        textProp.stringValue = mKeys[i];
                        GUIUtility.hotControl = 0;
                        GUIUtility.keyboardControl = 0;
                    }

                    if (++matches == 8)
                    {
                        GUILayout.Label("...and more");
                        break;
                    }
                }
            }
            GUI.backgroundColor = Color.white;
            GUILayout.EndVertical();
            GUILayout.Space(22f);
            GUILayout.EndHorizontal();
        }

        serializedObject.ApplyModifiedProperties();
    }

    //void OnInspectorGUI2()
    //{
    //    DrawDefaultInspector();
    //    EditorGUILayout.Space();
    //    DropDownBox();
    //}
    //void DropDownBox()
    //{
    //    _choices = LocalizationData.GetListKey(textProp.stringValue);
    //    // Update the serializedProperty - always do this in the beginning of OnInspectorGUI.
    //    serializedObject.Update();

    //    //doing the orientation thing
    //    if (_choiceIndex >= _choices.Length)
    //    {
    //        _choiceIndex = 0;
    //    }
    //    // 0
    //    string translate = LocalizationData.Get(textProp.stringValue, 1);
    //    string language = LocalizationData.GetConfig(1).name;
    //    EditorGUILayout.TextField(language + " ", translate);
    //    // 1
    //    translate = LocalizationData.Get(textProp.stringValue, 2);
    //    language = LocalizationData.GetConfig(2).name;
    //    EditorGUILayout.TextField(language + " ", translate);
    //    //
    //    _choiceIndex = EditorGUILayout.Popup("Orientation", _choiceIndex, _choices);
    //    if (GUILayout.Button("Choose Key"))
    //        ClickChooseKey();
    //    // Apply changes to the serializedProperty - always do this in the end of OnInspectorGUI.
    //    serializedObject.ApplyModifiedProperties();
    //}
    //void ClickChooseKey()
    //{
    //    if (_choiceIndex >= 0)
    //        textProp.stringValue = _choices[_choiceIndex];
    //    _choiceIndex = 0;
    //}
}