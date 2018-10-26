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
        [SerializeField] private RectTransform scroll;

        public void Init(string title, string message, Action okAction, string txtOkButton = "OK")
        {
            actionOK = okAction;
            txtTitle.text = title;
            txtMessage.text = message;
            if (txtOk) txtOk.text = txtOkButton;
            Snap();
        }
        void Snap()
        {
            float _height = heighBonus;
            Vector2 tempVect2 = Vector2.zero;
            tempVect2.x = rect.rect.width;
            //
            _height += txtTitle.preferredHeight;
            _height += txtMessage.preferredHeight;
            //
            _height  = _height > 900 ? 900 : _height;
            tempVect2.y = _height;
            rect.sizeDelta = tempVect2;
            scroll.sizeDelta = new Vector2(scroll.sizeDelta.x, _height - heighBonus - txtTitle.preferredHeight + 10);
        }
        public void OnYesBtnClicked()
        {
            if (actionOK != null)
                actionOK();
            StartCoroutine(FadeOut());
        }

        public override void OnClickBackground()
        {
            if (clickBackgroundToBack)
            {
                if (actionOK != null)
                    actionOK();
                StartCoroutine(FadeOut());
            }
        }
    }
}