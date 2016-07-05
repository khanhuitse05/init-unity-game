using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using LitJson;

public class SaveGameManager
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="paramKey"></param>
    /// <returns></returns>
    public static T loadData<T>(string paramKey) where T : class
    {
        string jsonData = loadStringData(paramKey);
        if (string.IsNullOrEmpty(jsonData))
            return null;

        try
        {
            string tmpJsonData = JSONControll.base64ToJson(jsonData);

            return JsonMapper.ToObject<T>(tmpJsonData);
        }
        catch (Exception ex)
        {
            Utils.LogError(ex.StackTrace);
            return null;
        }
    }

    public static bool saveData<T>(string paramKey, T paramData) where T : class
    {
        string jsonData = "";
        if (paramData != null)
            jsonData = JsonMapper.ToJson(paramData);

        string base64Data = JSONControll.jsonToBase64(jsonData);
        return saveData(paramKey, base64Data);
    }

    //---------------------------------------------------------------------------

    #region Basic Get/Set Functions

    static bool saveData(string paramKey, string paramData)
    {
        try
        {
            PlayerPrefs.SetString(paramKey, paramData);
            PlayerPrefs.Save();
            return true;
        }
        catch (Exception ex)
        {
            Utils.LogError(ex.StackTrace);
            return false;
        }
    }

    public static bool saveData(string paramKey, int paramData)
    {
        try
        {
            PlayerPrefs.SetInt(paramKey, paramData);
            return true;
        }
        catch (Exception ex)
        {
            Utils.LogError(ex.StackTrace);
            return false;
        }
    }

    static bool saveData(string paramKey, float paramData)
    {
        try
        {
            PlayerPrefs.SetFloat(paramKey, paramData);
            return true;
        }
        catch (Exception ex)
        {
            Utils.LogError(ex.StackTrace);
            return false;
        }
    }

    public static int loadIntData(string paramKey)
    {
        try
        {
            return PlayerPrefs.GetInt(paramKey, -1);
        }
        catch (Exception ex)
        {
            Utils.LogError(ex.StackTrace);
            return -1;
        }
    }

    static float loadFloatData(string paramKey)
    {
        try
        {
            return PlayerPrefs.GetFloat(paramKey, -1);
        }
        catch (Exception ex)
        {
            Utils.LogError(ex.StackTrace);
            return -1;
        }
    }

    static string loadStringData(string paramKey)
    {
        try
        {
            return PlayerPrefs.GetString(paramKey, null);
        }
        catch (Exception ex)
        {
            Utils.LogError(ex.StackTrace);
            return null;
        }
    }

    #endregion

}