using UnityEngine;
using System.Collections;

public class Popup : MonoBehaviour
{
    CanvasGroup canvasGroup;
    protected float timeDelay = 0.4f;
    void Start()
    {
        canvasGroup = gameObject.GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0;
        StartCoroutine(FadeIn());

    }
    protected IEnumerator FadeIn()
    {
        while (canvasGroup.alpha < 1)
        {
            canvasGroup.alpha += Time.deltaTime / timeDelay;
            yield return null;
        }
    }
    protected IEnumerator FadeOut()
    {
        while (canvasGroup.alpha > 0)
        {
            canvasGroup.alpha -= Time.deltaTime / timeDelay;
            yield return null;
        }
        Destroy(gameObject);
    }
}
