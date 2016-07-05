using UnityEngine;
using System.Collections.Generic;
using System;
using System.IO;

public class Utils
{
    public const int MAX_CUSTOMIZE = 2;
    public static int ColorToInt(Color32 clr)
    {
        return (clr.a << 24 | clr.r << 16 | clr.g << 8 | clr.b);
    }
    public static Color32 IntToColor(int clr)
    {
        int a = clr >> 24;
        int r = (clr & 0x00ff0000) >> 16;
        int g = (clr & 0x0000ff00) >> 8;
        int b = clr & 0x000000ff;
        return new Color32((byte)r, (byte)g, (byte)b, (byte)a);
    }
    public static string ArrayIntToString(int[] A)
    {
        string s = "";
        if (A != null)
        {
            for (int i = 0; i < A.Length; i++)
            {
                s += A[i] + ",";
            }
        }
        return s;
    }
    public static int[] StringtoArrayInt(string s)
    {
        string[] array = s.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
        int[] A = new int[array.Length];
        for (int i = 0; i < array.Length; i++)
        {
            A[i] = Int32.Parse(array[i]);
        }
        return A;
    }
    //
    public static void removeAllChildren(Transform paramParent, bool paramInstant=true)
    {
        if (paramParent == null)
            return;
        for (int i = paramParent.childCount - 1; i >= 0; i--)
        {
            if (paramInstant)
            {
                GameObject.DestroyImmediate(paramParent.GetChild(i).gameObject);
            } else
            {
                paramParent.GetChild(i).gameObject.SetActive(false);
                GameObject.Destroy(paramParent.GetChild(i).gameObject);
            }
        }
    }
    public static int Random(int paramMin, int paramMax)
    {
        return UnityEngine.Random.Range(paramMin, paramMax);
    }
    public static void Log(string paramLog)
    {
        Debug.Log(paramLog);
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
        newObject.transform.parent = paramParent;
        newObject.transform.localPosition = paramPrefab.transform.localPosition;
        newObject.transform.localScale = paramPrefab.transform.localScale;
        newObject.SetActive(true);
        return newObject;
    }
    public static string removeExtension(string paramPath)
    {
        int index = paramPath.LastIndexOf('.');
        if (index < 0)
            return paramPath;
        return paramPath.Substring(0, index);
    }
    public static void setActive(GameObject paramObject, bool paramValue)
    {
        if (paramObject != null)
            paramObject.SetActive(paramValue);
    }
  
    public static bool isURL(string paramURL)
    {
        if (string.IsNullOrEmpty(paramURL))
            return false;
        return (paramURL.IndexOf("http") >= 0);
    }
}

public class EnumParser
{
    public static T Parse<T>(string value)
    {
        return (T)Enum.Parse(typeof(T), value, true);
    }
}
