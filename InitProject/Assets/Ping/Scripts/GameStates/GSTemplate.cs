using System.Collections;
using UnityEngine;

////////////////////////////////////////////////////////
//Author:
//TODO: a game state sample
////////////////////////////////////////////////////////
namespace Ping
{
    public class GSTemplate : IState
    {
        protected bool isFirst;
        public GameObject guiMain;
        protected override void Awake()
        {
            guiMain.SetActive(false);
            canvasGroup = guiMain.GetComponent<CanvasGroup>();
            rect = guiMain.GetComponent<RectTransform>();
            isFirst = true;
        }
        /// One time when start
        protected virtual void init() { }

        public override void onSuspend(SwipeEffect effect)
        {
            base.onSuspend(effect);
            onDisableState(effect);
        }

        public override void onResume(SwipeEffect effect)
        {
            base.onResume(effect);
            onEnableState(effect);
        }

        public override void onEnter(SwipeEffect effect)
        {
            base.onEnter(effect);
            if (isFirst)
            {
                isFirst = false;
                init();
            }
            onEnableState(effect);
        }
        public override void onExit(SwipeEffect effect)
        {
            base.onExit(effect);
            onDisableState(effect);
        }

        /// Back key
        public virtual void onBackKey() { }


        #region Effect
        CanvasGroup canvasGroup;
        RectTransform rect;
        const float animationDuration = 0.75f;
        private float animStartTime;
        private float animDeltaTime;
        public virtual void onEnableState(SwipeEffect effect)
        {
            GameStatesManager.onBackKey = onBackKey;
            canvasGroup.alpha = 1;
            guiMain.transform.localScale = Vector3.one;
            rect.pivot = new Vector2(0.5f, rect.pivot.y);
            rect.anchorMin = new Vector2(0.5f, rect.anchorMin.y);
            rect.anchorMax = new Vector2(0.5f, rect.anchorMax.y);
            switch (effect)
            {
                case SwipeEffect.Active:
                    guiMain.SetActive(true);
                    break;
                case SwipeEffect.SlideLeft:
                    InitSlideLeftIn();
                    break;
                case SwipeEffect.SlideRight:
                    InitSlideRightIn();
                    break;
                case SwipeEffect.Fade:
                    InitFadeIn();
                    break;
                case SwipeEffect.Zome:
                    InitZoomIn();
                    break;
                default:
                    guiMain.SetActive(true);
                    break;
            }
        }
        public virtual void onDisableState(SwipeEffect effect)
        {
            GameStatesManager.onBackKey = null;
            switch (effect)
            {
                case SwipeEffect.Active:
                    guiMain.SetActive(false);
                    break;
                case SwipeEffect.SlideLeft:
                    InitSlideLeftOut();
                    break;
                case SwipeEffect.SlideRight:
                    InitSlideRightOut();
                    break;
                case SwipeEffect.Fade:
                    InitFadeOut();
                    break;
                case SwipeEffect.Zome:
                    InitZoomOut();
                    break;
                default:
                    guiMain.SetActive(false);
                    break;
            }
        }
        SwipeEffect currentEffect;
        bool isIn;
        protected override void Update()
        {
            base.Update();
            if (currentEffect != SwipeEffect.Active)
            {
                switch (currentEffect)
                {
                    case SwipeEffect.SlideLeft:
                        if (isIn) RoutineSlideLeftIn(); else RoutineSlideLeftOut();
                        break;
                    case SwipeEffect.SlideRight:
                        if (isIn) RoutineSlideRightIn(); else RoutineSlideRightOut();
                        break;
                    case SwipeEffect.Fade:
                        if (isIn) RoutineFadeIn(); else RoutineFadeOut();
                        break;
                    case SwipeEffect.Zome:
                        if (isIn) RoutineZoomIn(); else RoutineZoomOut();
                        break;
                }
            }
        }
        /// <summary>
        /// currentAlpha
        /// </summary>
        float currentAlpha;
        void InitFadeIn()
        {
            isIn = true;
            currentEffect = SwipeEffect.Fade;
            animStartTime = Time.realtimeSinceStartup;
            animDeltaTime = 0;
            guiMain.SetActive(true);
            currentAlpha = 0;
            canvasGroup.alpha = 0;
        }
        void RoutineFadeIn()
        {
            if (animDeltaTime <= animationDuration)
            {
                animDeltaTime = Time.realtimeSinceStartup - animStartTime;
                // effect
                canvasGroup.alpha = Anim.Quint.In(currentAlpha, 1f, animDeltaTime, animationDuration);
            }
            else
            {
                // finish
                canvasGroup.alpha = 1;
                currentEffect = SwipeEffect.Active;
            }
        }
        void InitFadeOut()
        {
            isIn = false;
            currentEffect = SwipeEffect.Fade;
            animStartTime = Time.realtimeSinceStartup;
            animDeltaTime = 0;
            currentAlpha = 1;
            canvasGroup.alpha = 1;
        }
        void RoutineFadeOut()
        {
            if (animDeltaTime <= animationDuration)
            {
                animDeltaTime = Time.realtimeSinceStartup - animStartTime;
                // effect
                canvasGroup.alpha = Anim.Quint.Out(currentAlpha, 0f, animDeltaTime, animationDuration);
            }
            else
            {
                // finish
                guiMain.SetActive(false);
                currentEffect = SwipeEffect.Active;
            }
        }
        /// <summary>
        /// RoutineZoom
        /// </summary>
        Vector3 scaleEffect = new Vector3(3, 3, 3);
        Vector3 currentScale;
        void InitZoomIn()
        {
            isIn = true;
            currentEffect = SwipeEffect.Zome;
            animStartTime = Time.realtimeSinceStartup;
            animDeltaTime = 0;
            guiMain.SetActive(true);
            currentAlpha = 0;
            canvasGroup.alpha = 0;
            guiMain.transform.localScale = scaleEffect;
        }
        void RoutineZoomIn()
        {
            if (animDeltaTime <= animationDuration)
            {
                animDeltaTime = Time.realtimeSinceStartup - animStartTime;
                // effect
                canvasGroup.alpha = Anim.Quint.In(currentAlpha, 1f, animDeltaTime, animationDuration);
                guiMain.transform.localScale = Anim.Quint.In(currentScale, Vector3.one, animDeltaTime, animationDuration);
            }
            else
            {
                // finish
                canvasGroup.alpha = 1;
                guiMain.transform.localScale = Vector3.one;
                currentEffect = SwipeEffect.Active;
            }
        }
        void InitZoomOut()
        {
            isIn = false;
            currentEffect = SwipeEffect.Zome;
            animStartTime = Time.realtimeSinceStartup;
            animDeltaTime = 0;
            currentAlpha = 1;
            canvasGroup.alpha = 1;
            guiMain.transform.localScale = Vector3.one;
        }
        void RoutineZoomOut()
        {
            if (animDeltaTime <= animationDuration)
            {
                animDeltaTime = Time.realtimeSinceStartup - animStartTime;
                // effect
                canvasGroup.alpha = Anim.Quint.Out(currentAlpha, 0f, animDeltaTime, animationDuration);
                guiMain.transform.localScale = Anim.Quint.Out(currentScale, scaleEffect, animDeltaTime, animationDuration);
            }
            else
            {
                // finish
                guiMain.SetActive(false);
                currentEffect = SwipeEffect.Active;
            }
        }
        /// <summary>
        /// RoutineSlide
        /// </summary>
        private Vector2 tempVector2;
        private float currentPivotX;
        private float currentAnchorMinX;
        private float currentAnchorMaxX;
        void InitSlideLeftIn()
        {
            isIn = true;
            currentEffect = SwipeEffect.SlideLeft;
            animStartTime = Time.realtimeSinceStartup;
            animDeltaTime = 0;
            guiMain.SetActive(true);

            currentPivotX = 0;
            currentAnchorMinX = 1;
            currentAnchorMaxX = 1;
        }
        void RoutineSlideLeftIn()
        {

            if (animDeltaTime <= animationDuration)
            {
                animDeltaTime = Time.realtimeSinceStartup - animStartTime;
                // effect
                tempVector2 = rect.pivot;
                tempVector2.x = Anim.Quint.Out(currentPivotX, 0.5f, animDeltaTime, animationDuration);
                rect.pivot = tempVector2;

                tempVector2 = rect.anchorMin;
                tempVector2.x = Anim.Quint.Out(currentAnchorMinX, 0.5f, animDeltaTime, animationDuration);
                rect.anchorMin = tempVector2;

                tempVector2 = rect.anchorMax;
                tempVector2.x = Anim.Quint.Out(currentAnchorMaxX, 0.5f, animDeltaTime, animationDuration);
                rect.anchorMax = tempVector2;
            }
            else
            {
                // finish
                rect.pivot = new Vector2(0.5f, rect.pivot.y);
                rect.anchorMin = new Vector2(0.5f, rect.anchorMin.y);
                rect.anchorMax = new Vector2(0.5f, rect.anchorMax.y);
                currentEffect = SwipeEffect.Active;
            }
        }
        void InitSlideLeftOut()
        {
            isIn = false;
            currentEffect = SwipeEffect.SlideLeft;
            animStartTime = Time.realtimeSinceStartup;
            animDeltaTime = 0;
            canvasGroup.alpha = 1;
            currentPivotX = 0.5f;
            currentAnchorMinX = 0.5f;
            currentAnchorMaxX = 0.5f;
        }
        void RoutineSlideLeftOut()
        {
            if (animDeltaTime <= animationDuration)
            {
                animDeltaTime = Time.realtimeSinceStartup - animStartTime;
                // effect
                tempVector2 = rect.pivot;
                tempVector2.x = Anim.Quint.Out(currentPivotX, 1, animDeltaTime, animationDuration);
                rect.pivot = tempVector2;

                tempVector2 = rect.anchorMin;
                tempVector2.x = Anim.Quint.Out(currentAnchorMinX, 0, animDeltaTime, animationDuration);
                rect.anchorMin = tempVector2;

                tempVector2 = rect.anchorMax;
                tempVector2.x = Anim.Quint.Out(currentAnchorMaxX, 0, animDeltaTime, animationDuration);
                rect.anchorMax = tempVector2;
            }
            else
            {
                // finish
                guiMain.SetActive(false);
                currentEffect = SwipeEffect.Active;
            }
        }
        //
        void InitSlideRightIn()
        {
            isIn = true;
            currentEffect = SwipeEffect.SlideRight;
            animStartTime = Time.realtimeSinceStartup;
            animDeltaTime = 0;
            guiMain.SetActive(true);

            currentPivotX = 1;
            currentAnchorMinX = 0;
            currentAnchorMaxX = 0;
        }
        void RoutineSlideRightIn()
        {
            if (animDeltaTime <= animationDuration)
            {
                animDeltaTime = Time.realtimeSinceStartup - animStartTime;
                // effect
                tempVector2 = rect.pivot;
                tempVector2.x = Anim.Quint.Out(currentPivotX, 0.5f, animDeltaTime, animationDuration);
                rect.pivot = tempVector2;

                tempVector2 = rect.anchorMin;
                tempVector2.x = Anim.Quint.Out(currentAnchorMinX, 0.5f, animDeltaTime, animationDuration);
                rect.anchorMin = tempVector2;

                tempVector2 = rect.anchorMax;
                tempVector2.x = Anim.Quint.Out(currentAnchorMaxX, 0.5f, animDeltaTime, animationDuration);
                rect.anchorMax = tempVector2;
            }
            else
            {
                // finish
                rect.pivot = new Vector2(0.5f, rect.pivot.y);
                rect.anchorMin = new Vector2(0.5f, rect.anchorMin.y);
                rect.anchorMax = new Vector2(0.5f, rect.anchorMax.y);
                currentEffect = SwipeEffect.Active;
            }
        }
        void InitSlideRightOut()
        {
            isIn = false;
            currentEffect = SwipeEffect.SlideRight;
            animStartTime = Time.realtimeSinceStartup;
            animDeltaTime = 0;
            canvasGroup.alpha = 1;
            currentPivotX = 0.5f;
            currentAnchorMinX = 0.5f;
            currentAnchorMaxX = 0.5f;
        }
        void RoutineSlideRightOut()
        {
            if (animDeltaTime <= animationDuration)
            {
                animDeltaTime = Time.realtimeSinceStartup - animStartTime;
                // effect
                tempVector2 = rect.pivot;
                tempVector2.x = Anim.Quint.Out(currentPivotX, 0, animDeltaTime, animationDuration);
                rect.pivot = tempVector2;

                tempVector2 = rect.anchorMin;
                tempVector2.x = Anim.Quint.Out(currentAnchorMinX, 1, animDeltaTime, animationDuration);
                rect.anchorMin = tempVector2;

                tempVector2 = rect.anchorMax;
                tempVector2.x = Anim.Quint.Out(currentAnchorMaxX, 1, animDeltaTime, animationDuration);
                rect.anchorMax = tempVector2;
            }
            else
            {
                // finish
                guiMain.SetActive(false);
                currentEffect = SwipeEffect.Active;
            }
        }
        #endregion
    }
}