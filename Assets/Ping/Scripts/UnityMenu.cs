using UnityEngine;
using System.Collections;
using UnityEditor;

public class UnityMenu : MonoBehaviour
{
    [MenuItem("Ping/Delete Saved files")]
    public static void ClearPlayerPrefs()
    {
        PlayerPrefs.DeleteKey(ResourceDataTags.settingDataKey);
    }
}
