using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
namespace Ping
{
    public class PopupInfoComponent : Popup
    {
        Action actionOK;
        public Text messageLbl;
        public Text okLbl;

        public void Init(string message, Action ok, string txtOk = "OK")
        {
            actionOK = ok;
            messageLbl.text = message;
            okLbl.text = txtOk;
        }
        public void OnYesBtnClicked()
        {
            if (actionOK != null)
                actionOK();
            StartCoroutine(FadeOut());
        }
    }
}