using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace Ping
{
    public class DialogBoxConfig : MonoBehaviour
    {
        [SerializeField] private bool darkenBackground = true;
        [SerializeField] private bool canClickBackgroundToCancel = false;
        [SerializeField] private float animationDuration = 1f;
        [Space(10)]
        public RectTransform backgroundTransform;

        private int state;
        private float animStartTime;
        private float animDeltaTime;
        private RectTransform thisRectTransform;
        private CanvasGroup backroundCanvasGroup;

        private float currentBackgroundAlpha;
        private float currentPivotY;
        private float currentAnchorMinY;
        private float currentAnchorMaxY;

        private Vector2 tempVector2;
        [HideInInspector]
        public bool isOpen = false;
        void Awake()
        {
            thisRectTransform = gameObject.GetComponent<RectTransform>();
            backroundCanvasGroup = backgroundTransform.GetComponent<CanvasGroup>();
        }

        void Start()
        {
            //backgroundTransform.sizeDelta = new Vector2(Screen.width, Screen.height * 3f);
            thisRectTransform.pivot = new Vector2(thisRectTransform.pivot.x, 1.1f);
            thisRectTransform.anchorMin = new Vector2(thisRectTransform.anchorMin.x, 0f);
            thisRectTransform.anchorMax = new Vector2(thisRectTransform.anchorMax.x, 0f);
            isOpen = false;
        }

        public void BackgroundClick()
        {
            if (canClickBackgroundToCancel)
            {
                Close();
            }
        }

        public void Open()
        {
            currentBackgroundAlpha = backroundCanvasGroup.alpha;
            currentPivotY = thisRectTransform.pivot.y;
            currentAnchorMinY = thisRectTransform.anchorMin.y;
            currentAnchorMaxY = thisRectTransform.anchorMax.y;

            backroundCanvasGroup.blocksRaycasts = true;

            animStartTime = Time.realtimeSinceStartup;
            state = 1;
            isOpen = true;
        }

        public void Close()
        {
            currentBackgroundAlpha = backroundCanvasGroup.alpha;
            currentPivotY = thisRectTransform.pivot.y;
            currentAnchorMinY = thisRectTransform.anchorMin.y;
            currentAnchorMaxY = thisRectTransform.anchorMax.y;

            backroundCanvasGroup.blocksRaycasts = false;

            animStartTime = Time.realtimeSinceStartup;
            state = 2;
            isOpen = false;
        }
        public void ForceClose()
        {
            backroundCanvasGroup.alpha = 0;
            thisRectTransform.pivot = new Vector2(thisRectTransform.pivot.x, 1.1f);
            thisRectTransform.anchorMin = new Vector2(thisRectTransform.anchorMin.x, 0f);
            thisRectTransform.anchorMax = new Vector2(thisRectTransform.anchorMax.x, 0f);
            backroundCanvasGroup.blocksRaycasts = false;
            isOpen = false;
            state = 0;
        }
        void Update()
        {
            animDeltaTime = Time.realtimeSinceStartup - animStartTime;

            if (state == 1)
            {
                if (animDeltaTime <= animationDuration)
                {
                    if (darkenBackground)
                    {
                        backroundCanvasGroup.alpha = Anim.Quint.Out(currentBackgroundAlpha, 1f, animDeltaTime, animationDuration);
                    }

                    tempVector2 = thisRectTransform.pivot;
                    tempVector2.y = Anim.Quint.Out(currentPivotY, 0.5f, animDeltaTime, animationDuration);
                    thisRectTransform.pivot = tempVector2;

                    tempVector2 = thisRectTransform.anchorMin;
                    tempVector2.y = Anim.Quint.Out(currentAnchorMinY, 0.5f, animDeltaTime, animationDuration);
                    thisRectTransform.anchorMin = tempVector2;

                    tempVector2 = thisRectTransform.anchorMax;
                    tempVector2.y = Anim.Quint.Out(currentAnchorMaxY, 0.5f, animDeltaTime, animationDuration);
                    thisRectTransform.anchorMax = tempVector2;
                }
                else
                {
                    thisRectTransform.pivot = new Vector2(thisRectTransform.pivot.x, 0.5f);
                    thisRectTransform.anchorMin = new Vector2(thisRectTransform.anchorMin.x, 0.5f);
                    thisRectTransform.anchorMax = new Vector2(thisRectTransform.anchorMax.x, 0.5f);

                    state = 0;
                }
            }
            else if (state == 2)
            {
                if (animDeltaTime <= animationDuration)
                {
                    if (darkenBackground)
                    {
                        backroundCanvasGroup.alpha = Anim.Quint.In(currentBackgroundAlpha, 0f, animDeltaTime, animationDuration * 0.75f);
                    }

                    tempVector2 = thisRectTransform.pivot;
                    tempVector2.y = Anim.Quint.In(currentPivotY, 1.1f, animDeltaTime, animationDuration * 0.75f);
                    thisRectTransform.pivot = tempVector2;

                    tempVector2 = thisRectTransform.anchorMin;
                    tempVector2.y = Anim.Quint.In(currentAnchorMinY, 0f, animDeltaTime, animationDuration * 0.75f);
                    thisRectTransform.anchorMin = tempVector2;

                    tempVector2 = thisRectTransform.anchorMax;
                    tempVector2.y = Anim.Quint.In(currentAnchorMaxY, 0f, animDeltaTime, animationDuration * 0.75f);
                    thisRectTransform.anchorMax = tempVector2;
                }
                else
                {
                    thisRectTransform.pivot = new Vector2(thisRectTransform.pivot.x, 1.1f);
                    thisRectTransform.anchorMin = new Vector2(thisRectTransform.anchorMin.x, 0f);
                    thisRectTransform.anchorMax = new Vector2(thisRectTransform.anchorMax.x, 0f);

                    state = 0;
                }
            }
        }
    }
}

