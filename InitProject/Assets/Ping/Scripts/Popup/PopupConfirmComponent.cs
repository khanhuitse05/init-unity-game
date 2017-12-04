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
        public Text messageLbl;
        public Text noLbl;
        public Text yesLbl;

        public void Init(string message, Action yes, Action no, string txtYes = "YES", string txtNo = "NO")
        {
            actionYes = yes;
            actionNo = no;
            messageLbl.text = message;
            yesLbl.text = txtYes;
            noLbl.text = txtNo;
            messageLbl.text = message;
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