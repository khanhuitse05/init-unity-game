using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class InfoPopUpComponent : Popup
{
    Action actionOK;
    string message;
    string txtOk;
    public Text messageLbl;
    public Text okLbl;

    public void Init(string message, Action ok, string _ok = "OK")
    {
        this.message = message;
        this.txtOk = _ok;
        actionOK = ok;
        messageLbl.text = this.message;
        okLbl.text = this.txtOk;
    }
    public void OnYesBtnClicked()
    {
        if (actionOK != null)
            actionOK();
        StartCoroutine(FadeOut());
    }
}