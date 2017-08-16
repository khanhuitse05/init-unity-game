using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System;
using System.Linq;
using System.Reflection;
using LitJson;

public class BaseOnline : MonoBehaviour
{

    private static BaseOnline _instance;
    public static BaseOnline Instance { get { return _instance; } }
    private void Awake()
    {
        _instance = this;
    }

    public const string Success = "success";
    public const string Error = "error";

    public void SendData(string url, object data, Action<string> onSendDataFinish, int timeOut = 30)
    {
        StartCoroutine(SendDataCoroutine(url, data, onSendDataFinish, timeOut));
    }

    /// <summary>
    ///     If connection exceeds timeout, return null
    ///     Otherwise return raw string of data
    /// </summary>
    /// <param name="url"></param>
    /// <param name="data"></param>
    /// <param name="onSendDataFinish"></param>
    /// <param name="type"></param>
    /// <param name="timeOut"> less or equal zeo mean endless</param>
    /// <param name="onUploadDownloading"></param>
    /// <param name="requestHeaderType"></param>
    /// <returns></returns>
    public IEnumerator SendDataCoroutine(string url, object data, Action<string> onSendDataFinish, int timeOut = 30)
    {
        byte[] dataSend = data != null ? Encoding.UTF8.GetBytes(JsonMapper.ToJson(data)) : null;
        Dictionary<string, string> headers = new Dictionary<string, string>();
        headers.Add("Content-Type", "application/json");
        WWW www = new WWW(url, dataSend, headers);

        float waitTime = 0;
        if (timeOut > 0)
        {
            while (waitTime <= timeOut)
            {
                waitTime += Time.deltaTime;
                if (www.isDone)
                    break;
                yield return null;
            }
        }
        else
        {
            while (!www.isDone)
            {
                yield return null;
            }
        }

        if (www.isDone)
        {
            if (onSendDataFinish != null)
                onSendDataFinish(www.text);
        }
        else
        {
            if (onSendDataFinish != null)
                onSendDataFinish(null);
        }
    }
    

    public static bool IsSuccess(string jsonString)
    {
        try
        {
            JsonData jsonData = JsonMapper.ToObject(jsonString);
            if (jsonData["status"].ToString() == Success)
                return true;
            return false;
        }
        catch (Exception e)
        {
            Debug.LogError(e);
            return false;
        }
    }
    public static bool IsError(string jsonString)
    {
        try
        {
            JsonData jsonData = JsonMapper.ToObject(jsonString);
            if (jsonData["status"].ToString() == Error)
                return true;
            return false;
        }
        catch (Exception e)
        {
            Debug.LogError(e);
            return false;
        }
    }
}
