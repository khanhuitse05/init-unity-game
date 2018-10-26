using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System;
using LitJson;

public class BaseOnline : MonoSingleton<BaseOnline>
{
    public const string Success = "success";
    public const string Error = "error";

    public static readonly Dictionary<string, string> JsonHeader = new Dictionary<string, string>
    {
        {"Content-Type", "application/json"}
    };

    public static readonly Dictionary<string, string> MultipartHeader = new Dictionary<string, string>
    {
        {"Content-Type", "multipart/form-data"}
    };

    public static bool Tracking = true;
    public static float DefaultTimeOut = 30;

    #region WWW
    // Get and Default time out
    public void WWW(string url,
        Action<string> onFinish = null,
        Action<float> onUploadDownloading = null)
    {
        StartCoroutine(WWWCoroutine(url, null, DefaultTimeOut, onFinish, onUploadDownloading));
    }
    // Get
    public void WWW(string url,
        int timeOut,
        Action<string> onFinish = null,
        Action<float> onUploadDownloading = null)
    {
        StartCoroutine(WWWCoroutine(url, null, timeOut, onFinish, onUploadDownloading));
    }
    // Post and Default time out
    public void WWW(string url,
        object data,
        Action<string> onFinish = null,
        Action<float> onUploadDownloading = null)
    {
        StartCoroutine(WWWCoroutine(url, data, DefaultTimeOut, onFinish, onUploadDownloading));
    }
    // Post
    public void WWW(string url,
        object data,
        float timeOut,
        Action<string> onFinish = null,
        Action<float> onUploadDownloading = null)
    {
        StartCoroutine(WWWCoroutine(url, data, timeOut, onFinish, onUploadDownloading));
    }

    public IEnumerator WWWCoroutine(string url,
        object data,
        float timeOut,
        Action<string> onFinish = null,
        Action<float> onUploadDownloading = null)
    {
        PrintTrack("<color=yellow>url: " + url + "</color> \ndata: " + (data != null ? JsonMapper.ToJson(data) : "GET"));
        if (data != null)
        {
            WWWForm wwwForm = data as WWWForm;
            if (wwwForm != null)
            {
                // Header "multipart/form-data" added by default
                WWW www = new WWW(url, wwwForm);
                yield return WWWCoroutineCore(www, timeOut, onFinish, onUploadDownloading);
            }
            else
            {
                string dataSendString = JsonMapper.ToJson(data);
                byte[] dataSend = Encoding.UTF8.GetBytes(dataSendString);
                WWW www = new WWW(url, dataSend, JsonHeader);
                yield return WWWCoroutineCore(www, timeOut, onFinish, onUploadDownloading);
            }
        }
        else
        {
            WWW www = new WWW(url, null, JsonHeader);
            yield return WWWCoroutineCore(www, timeOut, onFinish, onUploadDownloading);
        }
    }

    private IEnumerator WWWCoroutineCore(WWW www,
        float timeOut,
        Action<string> onFinish,
        Action<float> onUploadDownloading)
    {
        float startTime = Time.realtimeSinceStartup;

        if (timeOut > 0)
        {
            while (Time.realtimeSinceStartup - startTime <= timeOut)
            {
                if (www.isDone)
                    break;

                if (onUploadDownloading != null)
                    onUploadDownloading((www.uploadProgress + www.progress) / 2f);

                yield return null;
            }
        }
        else
        {
            while (!www.isDone)
            {
                if (onUploadDownloading != null)
                    onUploadDownloading((www.uploadProgress + www.progress) / 2f);

                yield return null;
            }
        }

        if (onUploadDownloading != null)
            onUploadDownloading((www.uploadProgress + www.progress) / 2f);

        if (www.isDone)
        {
            PrintTrack("<color=green>url: " + www.url + "</color>\n Text: " + www.text);
        }

        if (www.isDone)
        {
            if (onFinish != null)
                onFinish(www.text);
        }
        else
        {
            if (onFinish != null)
                onFinish("");
        }
        www.Dispose();
    }

    #endregion

    private void PrintTrack(string message)
    {
#if UNITY_EDITOR
        if (Tracking)
        {
            Debug.Log("BaseOnline: " + message);
        }
#endif
    }

    public static bool IsSuccess(string jsonString)
    {
        JsonData jsonData = null;
        try
        {
            jsonData = JsonMapper.ToObject(jsonString);
        }
        catch (Exception)
        {
            return false;
        }

        return IsSuccess(jsonData);
    }

    public static bool IsSuccess(JsonData jsonData)
    {
        try
        {
            if (jsonData["status"].ToString() == Success)
                return true;
            return false;
        }
        catch (Exception)
        {
            return false;
        }
    }
    public static string Message(string jsonString)
    {
        JsonData jsonData = null;
        try
        {
            jsonData = JsonMapper.ToObject(jsonString);
            return jsonData["message"].ToString();
        }
        catch (Exception)
        {
            return "";
        }
    }
}
