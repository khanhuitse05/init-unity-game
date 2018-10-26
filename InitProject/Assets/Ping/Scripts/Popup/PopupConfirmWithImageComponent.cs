using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

namespace Ping
{
    public class PopupConfirmWithImageComponent : Popup
    {
        Action actionYes;
        Action actionNo;
        Action actionBG;
        [SerializeField] private Image img;
        [SerializeField] private Text txtMessage;
        [SerializeField] private Text txtNo;
        [SerializeField] private Text txtYes;
        [SerializeField] private RectTransform rect;
        public void Init(string imgName, string message, Action _actionYes, Action _actionNo, string _yes = "YES", string _no = "NO", Action _actionBG = null)
        {
            Sprite sprite = Utils.loadResourcesSprite(imgName);
            Init(sprite, message, _actionYes, _actionNo, _yes, _no, _actionBG);
        }
        public void Init(Sprite sprite, string message, Action _actionYes, Action _actionNo, string _yes = "YES", string _no = "NO", Action _actionBG = null)
        {
            actionYes = _actionYes;
            actionNo = _actionNo;
            actionBG = _actionBG;
            img.sprite = sprite;
            txtMessage.text = message;
            txtYes.text = _yes;
            txtNo.text = _no;
            Snap();
        }
        void Snap()
        {
            float _height = heighBonus;
            Vector2 tempVect2 = Vector2.zero;
            tempVect2.x = rect.rect.width;
            //
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

        public override void OnClickBackground()
        {
            if (clickBackgroundToBack)
            {
                if (actionBG != null)
                    actionBG();
                StartCoroutine(FadeOut());
            }
        }

    }
}