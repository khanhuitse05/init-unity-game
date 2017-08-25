using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

namespace Ping
{
    public class MessageComponent : MonoBehaviour
    {
        static List<MessageComponent> listMessage = new List<MessageComponent>();

        public Text messageLbl;
        public RectTransform rect;
        public float timeLive = 2;
        public void Init(string message)
        {
            canvasGroup = rect.GetComponent<CanvasGroup>();
            messageLbl.text = message;
            Canvas.ForceUpdateCanvases();
            RectTransformSnap rectSnap = rect.GetComponent<RectTransformSnap>();
            rectSnap.Snap();
            AddMessage(this, rect.sizeDelta.y);
            StartCoroutine(FadeIn());
        }
                
        CanvasGroup canvasGroup;
        const float animationDuration = 0.5f;
        private float animStartTime;
        private float animDeltaTime;
        float currentAlpha;
        Vector3 currentScale;
        protected IEnumerator FadeIn()
        {
            animStartTime = Time.realtimeSinceStartup;
            animDeltaTime = 0;
            currentScale = Vector3.zero;
            canvasGroup.alpha = 1;
            while (animDeltaTime <= animationDuration)
            {
                animDeltaTime = Time.realtimeSinceStartup - animStartTime;
                // effect
                rect.localScale = Anim.Quint.Out(currentScale, Vector3.one, animDeltaTime, animationDuration);
                yield return new WaitForFixedUpdate();
            }
            // finish
            rect.localScale = Vector3.one;
            StartCoroutine(FadeOut());
        }
        protected IEnumerator FadeOut()
        {
            yield return new WaitForSeconds(timeLive);
            animStartTime = Time.realtimeSinceStartup;
            animDeltaTime = 0;
            currentAlpha = 1;
            canvasGroup.alpha = 1;
            while (animDeltaTime <= animationDuration)
            {
                animDeltaTime = Time.realtimeSinceStartup - animStartTime;
                // effect
                canvasGroup.alpha = Anim.Quint.Out(currentAlpha, 0, animDeltaTime, animationDuration);
                yield return new WaitForFixedUpdate();
            }
            // finish
            OnDestroyMessagePopup(this);
        }


        /// <summary>
        /// </summary>
        /// <param name="_item"></param>
        public void AddMessage(MessageComponent message, float _size)
        {
            for (int i = 0; i < listMessage.Count; i++)
            {
                if (listMessage[i] != null)
                {
                    listMessage[i].OnMoveUp(_size);
                }
            }
            listMessage.Add(message);
        }
        public void OnDestroyMessagePopup(MessageComponent _item)
        {
            listMessage.Remove(_item);
            Destroy(_item.gameObject);
        }

        /// <summary>
        /// </summary>
        /// <param name="_size"></param>
        private float moveStartTime;
        private float moveDeltaTime;
        Vector3 currentPosition;
        Vector3 endPosition = Vector3.zero;
        public void OnMoveUp(float _size)
        {
            if (endPosition != Vector3.zero)
            {
                rect.localPosition = endPosition;
            }
            StopCoroutine(MoveUp());
            currentPosition = rect.localPosition;
            endPosition = new Vector3(currentPosition.x, currentPosition.y + _size + 50, currentPosition.z);
            StartCoroutine(MoveUp());
        }
        protected IEnumerator MoveUp()
        {
            moveStartTime = Time.realtimeSinceStartup;
            moveDeltaTime = 0;
            while (moveDeltaTime <= animationDuration)
            {
                moveDeltaTime = Time.realtimeSinceStartup - moveStartTime;
                // effect
                rect.localPosition = Anim.Quint.Out(currentPosition, endPosition, moveDeltaTime, animationDuration);
                yield return new WaitForFixedUpdate();
            }
            // finish
            rect.localPosition = endPosition;
        }
    }
}