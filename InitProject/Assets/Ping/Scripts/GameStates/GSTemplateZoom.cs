using System.Collections;
using UnityEngine;

////////////////////////////////////////////////////////
//Author:
//TODO: a game state sample
////////////////////////////////////////////////////////

public class GSTemplateZoom : GSTemplate
{
    protected CanvasGroup canvasGroup;
    protected Vector3 scaleEffect = new Vector3(3, 3, 3);
    protected float timeIn = 0.4f;
    protected float timeOut = 0.4f;
    protected override void Awake()
    {
        base.Awake();
        canvasGroup = guiMain.GetComponent<CanvasGroup>();
    }
    public override void onSuspend()
    {
        GameStatesManager.onBackKey = null;
        FadeOut();
    }
    public override void onResume()
    {
        base.onResume();
        FadeIn();
    }
    protected virtual void FadeIn()
    {
        guiMain.SetActive(true);
        canvasGroup.interactable = false;
        // scale effect
        guiMain.transform.localScale = scaleEffect;
        iTween.ScaleTo(guiMain, Vector3.one, timeIn);
        // alpha effect
        canvasGroup.alpha = 1;
        canvasGroup.interactable = true;
        FadeInFinish();
    }
    protected virtual void FadeOut()
    {
        canvasGroup.interactable = false;
        iTween.ScaleTo(guiMain, scaleEffect, timeOut);
        Hashtable ht = iTween.Hash("from", 1, "to", 0, "time", timeOut / 2, "onupdate", "UpdateAlphaCanvas", "onComplete", "FadeOutFinish");
        iTween.ValueTo(gameObject, ht);
    }
    void UpdateAlphaCanvas(float value)
    {
        canvasGroup.alpha = value;
    }
    protected virtual void FadeInFinish()
    {
    }
    protected virtual void FadeOutFinish()
    {
        guiMain.SetActive(false);
    }
}
