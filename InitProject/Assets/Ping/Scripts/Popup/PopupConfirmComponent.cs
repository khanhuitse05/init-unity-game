using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

namespace Ping
{
    public class PopupConfirmComponent : Popup
    {
        Action actionYes;
        Action actionNo;
        public Text txtTitle;
        public Text txtMessage;
        public Text txtNo;
        public Text txtYes;

        public void Init(string title, string message, Action _actionYes, Action _actionNo, string _yes = "YES", string _no = "NO")
        {
            actionYes = _actionYes;
            actionNo = _actionNo;
            txtTitle.text = title;
            txtMessage.text = message;
            txtYes.text = _yes;
            txtNo.text = _no;
        }
        public void OnYesBtnClicked()
        {
            if (actionYes != null)
                actionYes();
            StartCoroutine(FadeOut());
        }
        public void OnNoBtnClicked()
        {
            if (actionNo != null)
                actionNo();
            StartCoroutine(FadeOut());
        }
    }
}