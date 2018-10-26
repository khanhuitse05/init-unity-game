
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace Ping
{
    [ExecuteInEditMode()]
    public class RectTransformSnap : MonoBehaviour
    {
        public RectTransform targetRect;
        private RectTransform thisRect;

        public float xPadding = 0f;
        public float yPadding = 0f;

        public bool snapEveryFrame;
        public bool sizeOnly;

        Vector2 lastSize;

        private void OnEnable()
        {
            if (!thisRect)
            {
                thisRect = gameObject.GetComponent<RectTransform>();
            }
        }

        private void LateUpdate()
        {
            if (snapEveryFrame)
                Snap();
        }

        public void Snap()
        {
            if (targetRect)
            {
#if !UNITY_EDITOR
                if (lastSize != new Vector2(targetRect.rect.width, targetRect.rect.height))
#endif
                {
                    lastSize = new Vector2(targetRect.rect.width, targetRect.rect.height);

                    if (!sizeOnly)
                        thisRect.position = targetRect.position;

                    Vector2 tempVect2;
                    tempVect2.x = targetRect.rect.width + xPadding;
                    tempVect2.y = targetRect.rect.height + yPadding;
                    thisRect.sizeDelta = tempVect2;
                }
            }
            else
            {
                Debug.Log("No target rect! Please attach one.");
            }
        }
    }
}