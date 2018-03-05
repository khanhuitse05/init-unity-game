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
        [SerializeField] private Text txtTitle;
        [SerializeField] private Text txtMessage;
        [SerializeField] private Text txtNo;
        [SerializeField] private Text txtYes;
        [SerializeField] private RectTransform rect;

        public void Init(string title, string message, Action _actionYes, Action _actionNo, string _yes = "YES", string _no = "NO")
        {
            actionYes = _actionYes;
            actionNo = _actionNo;
            txtTitle.text = title;
            txtMessage.text = message;
            txtYes.text = _yes;
            txtNo.text = _no;
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