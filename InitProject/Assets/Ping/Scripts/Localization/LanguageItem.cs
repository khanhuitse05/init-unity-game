using System;
using UnityEngine;
using UnityEngine.UI;

public class LanguageItem : MonoBehaviour
{
    public static LanguageItem current;
    public GameObject tick;
    public Text txtName;
    public string id { get; set; }
    Action<string> actionChoose;
    public void Init(string _language, Action<string> _doIt)
    {
        actionChoose += _doIt;
        id = _language;
        LocalizationConfig _config = LocalizationData.GetConfig(id);
        txtName.text = _config.name;
        tick.SetActive(false);
    }
    public void OnChoose()
    {
        if (current != this)
        {
            if (current != null)
            {
                current.tick.SetActive(false);
            }
            current = this;
            tick.SetActive(true);
            if (actionChoose != null)
            {
                actionChoose(id);
            }
        }
    }
}
