using UnityEngine;
using System.Collections;

namespace Ping
{
    public class Popup : MonoBehaviour
    {
        CanvasGroup canvasGroup;
        const float animationDuration = 0.5f;
        private float animStartTime;
        private float animDeltaTime;
        float currentAlpha;
        void Start()
        {
            canvasGroup = gameObject.GetComponent<CanvasGroup>();
            canvasGroup.alpha = 0;
            StartCoroutine(FadeIn());

        }
        protected IEnumerator FadeIn()
        {
            animStartTime = Time.realtimeSinceStartup;
            animDeltaTime = 0;
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
        protected IEnumerator FadeOut()
        {
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
            Destroy(gameObject);
        }
    }
}