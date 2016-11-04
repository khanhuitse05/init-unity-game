using UnityEngine;
using System.Collections;
using UnityEditor;

public class UnityMenu : MonoBehaviour
{
    [MenuItem("Ping/Delete PlayerPrefs")]
    public static void ClearPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
    }
}
