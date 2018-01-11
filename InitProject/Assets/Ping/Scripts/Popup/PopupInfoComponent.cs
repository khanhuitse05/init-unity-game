using UnityEngine.UI;
using System;
namespace Ping
{
    public class PopupInfoComponent : Popup
    {
        Action actionOK;
        public Text txtTitle;
        public Text txtMessage;
        public Text txtOk;

        public void Init(string title, string message, Action okAction, string txtOkButton = "OK")
        {
            actionOK = okAction;
            txtTitle.text = title;
            txtMessage.text = message;
            txtOk.text = txtOkButton;
        }
        public void OnYesBtnClicked()
        {
            if (actionOK != null)
                actionOK();
            StartCoroutine(FadeOut());
        }
    }
}