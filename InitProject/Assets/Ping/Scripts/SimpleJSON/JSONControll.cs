using UnityEngine;
using System.IO;
using System;

public class JSONControll
{
    public static string LoadJsonFromFile(string filePath)
    {
        try
        {
            string json;
            using (StreamReader r = new StreamReader(filePath))
            {
                json = r.ReadToEnd();
            }
            return json;
        }
        catch (Exception error)
        {
            Debug.Log("Read Json File Error: " + error);
            return null;
        }
    }
    public static string jsonToBase64(string paramJson)
    {
        try
        {
            SimpleJSON.JSONNode node = SimpleJSON.JSON.Parse(paramJson);
            return node.SaveToBase64();
        }
        catch (Exception ex)
        {
            Utils.LogError(ex.Message);
            return paramJson;
        }
    }

    public static string base64ToJson(string paramBase64)
    {
        try
        {
            SimpleJSON.JSONNode node = SimpleJSON.JSONNode.LoadFromBase64(paramBase64);
            return node.ToString();
        }
        catch (Exception ex)
        {
            Utils.LogError(ex.Message);
            return paramBase64;
        }
    }
}
