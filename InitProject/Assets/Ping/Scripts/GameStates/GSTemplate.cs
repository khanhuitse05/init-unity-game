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
            StopAllCoroutines();
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
                case SwipeEffect.Slide:
                    StartCoroutine(RoutineSlideIn(effect));
                    break;
                case SwipeEffect.Fade:
                    StartCoroutine(RoutineFadeIn(effect));
                    break;
                case SwipeEffect.Zome:
                    StartCoroutine(RoutineZoomIn(effect));
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
                case SwipeEffect.Slide:
                    StartCoroutine(RoutineSlideOut(effect));
                    break;
                case SwipeEffect.Fade:
                    StartCoroutine(RoutineFadeOut(effect));
                    break;
                case SwipeEffect.Zome:
                    StartCoroutine(RoutineZoomOut(effect));
                    break;
                default:
                    guiMain.SetActive(false);
                    break;
            }
        }

        float currentAlpha;
        protected virtual IEnumerator RoutineFadeIn(SwipeEffect effect)
        {
            animStartTime = Time.realtimeSinceStartup;
            animDeltaTime = 0;
            guiMain.SetActive(true);
            currentAlpha = 0;
            canvasGroup.alpha = 0;
            while (animDeltaTime <= animationDuration)
            {
                animDeltaTime = Time.realtimeSinceStartup - animStartTime;
                // effect
                canvasGroup.alpha = Anim.Quint.In(currentAlpha, 1f, animDeltaTime, animationDuration);
                yield return new WaitForFixedUpdate();
            }
            // finish
            canvasGroup.alpha = 1;
        }
        protected virtual IEnumerator RoutineFadeOut(SwipeEffect effect)
        {
            animStartTime = Time.realtimeSinceStartup;
            animDeltaTime = 0;
            currentAlpha = 1;
            canvasGroup.alpha = 1;
            while (animDeltaTime <= animationDuration)
            {
                animDeltaTime = Time.realtimeSinceStartup - animStartTime;
                // effect
                canvasGroup.alpha = Anim.Quint.Out(currentAlpha, 0f, animDeltaTime, animationDuration);
                yield return new WaitForFixedUpdate();
            }
            // finish
            guiMain.SetActive(false);
        }

        Vector3 scaleEffect = new Vector3(3, 3, 3);
        Vector3 currentScale;
        protected virtual IEnumerator RoutineZoomIn(SwipeEffect effect)
        {
            animStartTime = Time.realtimeSinceStartup;
            animDeltaTime = 0;
            guiMain.SetActive(true);
            currentAlpha = 0;
            canvasGroup.alpha = 0;
            guiMain.transform.localScale = scaleEffect;
            while (animDeltaTime <= animationDuration)
            {
                animDeltaTime = Time.realtimeSinceStartup - animStartTime;
                // effect
                canvasGroup.alpha = Anim.Quint.In(currentAlpha, 1f, animDeltaTime, animationDuration);
                guiMain.transform.localScale = Anim.Quint.In(currentScale, Vector3.one, animDeltaTime, animationDuration);
                yield return new WaitForFixedUpdate();
            }
            // finish
            canvasGroup.alpha = 1;
            guiMain.transform.localScale = Vector3.one;
        }
        protected virtual IEnumerator RoutineZoomOut(SwipeEffect effect)
        {
            animStartTime = Time.realtimeSinceStartup;
            animDeltaTime = 0;
            currentAlpha = 1;
            canvasGroup.alpha = 1;
            guiMain.transform.localScale = Vector3.one;
            while (animDeltaTime <= animationDuration)
            {
                animDeltaTime = Time.realtimeSinceStartup - animStartTime;
                // effect
                canvasGroup.alpha = Anim.Quint.Out(currentAlpha, 0f, animDeltaTime, animationDuration);
                guiMain.transform.localScale = Anim.Quint.Out(currentScale, scaleEffect, animDeltaTime, animationDuration);
                yield return new WaitForFixedUpdate();
            }
            // finish
            guiMain.SetActive(false);
        }

        private Vector2 tempVector2;
        private float currentPivotX;
        private float currentAnchorMinX;
        private float currentAnchorMaxX;
        protected virtual IEnumerator RoutineSlideIn(SwipeEffect effect)
        {
            animStartTime = Time.realtimeSinceStartup;
            animDeltaTime = 0;
            guiMain.SetActive(true);

            currentPivotX = 0;
            currentAnchorMinX = 1;
            currentAnchorMaxX = 1;

            while (animDeltaTime <= animationDuration)
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
                yield return new WaitForFixedUpdate();
            }
            // finish
            rect.pivot = new Vector2(0.5f, rect.pivot.y );
            rect.anchorMin = new Vector2(0.5f, rect.anchorMin.y);
            rect.anchorMax = new Vector2(0.5f, rect.anchorMax.y);
        }
        protected virtual IEnumerator RoutineSlideOut(SwipeEffect effect)
        {
            animStartTime = Time.realtimeSinceStartup;
            animDeltaTime = 0;
            canvasGroup.alpha = 1;
            currentPivotX = 0.5f;
            currentAnchorMinX = 0.5f;
            currentAnchorMaxX = 0.5f;

            while (animDeltaTime <= animationDuration)
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
                yield return new WaitForFixedUpdate();
            }
            // finish
            guiMain.SetActive(false);
        }
        #endregion
    }
}