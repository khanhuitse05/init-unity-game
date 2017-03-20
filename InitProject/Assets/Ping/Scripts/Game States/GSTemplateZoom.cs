using System.Collections;
using UnityEngine;

////////////////////////////////////////////////////////
//Author:
//TODO: a game state sample
////////////////////////////////////////////////////////

public class GSTemplateZoom : GSTemplate
{
    protected CanvasGroup canvasGroup;
    protected Vector3 scaleStart = new Vector3(0.7f, 0.7f, 0.7f);
    protected Vector3 scaleEnd = new Vector3(0.9f, 0.9f, 0.9f);
    protected float timeIn = 0.3f;
    protected float timeOut = 0.2f;
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
        guiMain.transform.localScale = scaleStart;
        iTween.ScaleTo(guiMain, Vector3.one, timeIn);
        // alpha effect
        canvasGroup.alpha = 1;
        canvasGroup.interactable = true;
        FadeInFinish();
    }
    protected virtual void FadeOut()
    {
        canvasGroup.interactable = false;
        iTween.ScaleTo(guiMain, scaleEnd, timeOut);
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
