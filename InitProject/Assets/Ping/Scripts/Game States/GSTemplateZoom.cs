using System.Collections;
using UnityEngine;

////////////////////////////////////////////////////////
//Author:
//TODO: a game state sample
////////////////////////////////////////////////////////

public class GSTemplateZoom : IState
{
    public GameObject guiMain;
    public AudioClip musicClip;
    bool isFirst;
    CanvasGroup canvasGroup;
    protected Vector3 scaleEffect = new Vector3(3, 3, 3);
    protected float timeIn = 0.4f;
    protected float timeOut = 0.4f;
    protected override void Awake()
    {
        base.Awake();
        canvasGroup = guiMain.GetComponent<CanvasGroup>();
        guiMain.SetActive(false);
        isFirst = true;
    }
    /// <summary>
    /// One time when start
    /// </summary>
    protected virtual void init()
    {
    }
    protected virtual void onBackKey()
    {
    }
    public override void onSuspend()
    {
        base.onSuspend();
        GameStatesManager.onBackKey = null;
        StartCoroutine(FadeOut());
    }
    public override void onResume()
    {
        base.onResume();
        GameStatesManager.Instance.InputProcessor = guiMain;
        GameStatesManager.onBackKey = onBackKey;
        if (musicClip != null)
        {
            AudioManager.PlayMusic(musicClip, true);
        }
        StartCoroutine(FadeIn());
    }
    public override void onEnter()
    {
        base.onEnter();
        if (isFirst)
        {
            isFirst = false;
            init();
        }
        onResume();
    }
    public override void onExit()
    {
        base.onExit();
        onSuspend();
    }
    public virtual void ShowContent()
    {
    }

    public virtual void HideContent()
    {
    }
    IEnumerator FadeIn()
    {
        yield return new WaitForSeconds(timeOut);
        guiMain.SetActive(true);
        canvasGroup.interactable = false;
        guiMain.transform.localScale = scaleEffect;
        iTween.ScaleTo(guiMain, Vector3.one, timeIn);
        while (canvasGroup.alpha < 1)
        {
            canvasGroup.alpha += Time.fixedDeltaTime / timeIn;
            yield return null;
        }
        canvasGroup.alpha = 1;
        canvasGroup.interactable = true;
        ShowContent();
    }
    IEnumerator FadeOut()
    {
        HideContent();
        canvasGroup.interactable = false;
        iTween.ScaleTo(guiMain, scaleEffect, timeIn);
        while (canvasGroup.alpha > 0)
        {
            canvasGroup.alpha -= Time.fixedDeltaTime / timeOut;
            yield return null;
        }
        guiMain.SetActive(false);
    }
}
