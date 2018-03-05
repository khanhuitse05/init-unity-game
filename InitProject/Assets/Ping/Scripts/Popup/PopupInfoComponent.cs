using UnityEngine.UI;
using System;
using UnityEngine;

namespace Ping
{
    public class PopupInfoComponent : Popup
    {
        Action actionOK;
        [SerializeField] private Text txtTitle;
        [SerializeField] private Text txtMessage;
        [SerializeField] private Text txtOk;
        [SerializeField] private RectTransform rect;

        public void Init(string title, string message, Action okAction, string txtOkButton = "OK")
        {
            actionOK = okAction;
            txtTitle.text = title;
            txtMessage.text = message;
            txtOk.text = txtOkButton;
            Snap();
        }
        void Snap()
        {
            float _height = 160;
            Vector2 tempVect2 = Vector2.zero;
            tempVect2.x = rect.rect.width;
            //
            _height += txtTitle.preferredHeight;
            _height += txtMessage.preferredHeight;
            //
            tempVect2.y = _height;
            rect.sizeDelta = tempVect2;
        }
        public void OnYesBtnClicked()
        {
            if (actionOK != null)
                actionOK();
            StartCoroutine(FadeOut());
        }
    }
}