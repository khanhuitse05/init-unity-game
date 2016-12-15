using UnityEngine;
using System.Collections.Generic;
using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Globalization;

public class Utils
{
    public static void Log(string paramLog)
    {
        Debug.Log(paramLog);
    }
    public static void LogYellow(string paramLog)
    {
        Debug.Log("<color=yellow>" + paramLog + "</color>");
    }
    public static void LogRed(string paramLog)
    {
        Debug.Log("<color=red>" + paramLog + "</color>");
    }
    public static void LogError(string paramLog)
    {
        Debug.LogError(paramLog);
    }
    public static Sprite loadResourcesSprite(string param)
    {
        return Resources.Load<Sprite>("" + param);
    }
    public static T[] CloneArray<T>(T[] paramArray)
    {
        if (paramArray == null)
            return null;
        return paramArray.Clone() as T[];
    }
    public static List<T> CloneArray<T>(List<T> paramArray)
    {
        if (paramArray == null)
            return null;
        List<T> list = new List<T>(paramArray.ToArray());
        return list;
    }
    public static GameObject Spawn(GameObject paramPrefab, Transform paramParent = null)
    {
        GameObject newObject = GameObject.Instantiate(paramPrefab) as GameObject;
        newObject.transform.SetParent(paramParent);
        newObject.transform.localPosition = Vector3.zero;
        newObject.transform.localScale = paramPrefab.transform.localScale;
        newObject.SetActive(true);
        return newObject;
    }
    public static void setActive(GameObject paramObject, bool paramValue)
    {
        if (paramObject != null)
            paramObject.SetActive(paramValue);
    }
    public static void removeAllChildren(Transform paramParent, bool paramInstant = true)
    {
        if (paramParent == null)
            return;
        for (int i = paramParent.childCount - 1; i >= 0; i--)
        {
            if (paramInstant)
            {
                GameObject.DestroyImmediate(paramParent.GetChild(i).gameObject);
            }
            else
            {
                paramParent.GetChild(i).gameObject.SetActive(false);
                GameObject.Destroy(paramParent.GetChild(i).gameObject);
            }
        }
    }
}