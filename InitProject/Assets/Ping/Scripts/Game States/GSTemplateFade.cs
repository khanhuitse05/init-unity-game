using System.Collections;
using UnityEngine;

////////////////////////////////////////////////////////
//Author:
//TODO: a game state sample
////////////////////////////////////////////////////////

public class GSTemplateFade : GSTemplate
{
    CanvasGroup canvasGroup;
    protected float timeIn = 0.2f;
    protected float timeOut = 0.2f;
    protected override void Awake()
    {
        base.Awake();
        canvasGroup = guiMain.GetComponent<CanvasGroup>();
    }
    public override void onSuspend()
    {
        GameStatesManager.onBackKey = null;
        if (canvasGroup != null)
        {
            FadeOut();
        }
        else
        {
            guiMain.SetActive(false);
        }
    }
    public override void onResume()
    {
        base.onResume();
        if (canvasGroup != null)
        {
            FadeIn();
        }
    }
    void FadeIn()
    {
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        Hashtable ht = iTween.Hash("from", 0, "to", 1, "time", timeIn, "onupdate", "UpdateAlphaCanvas", "onComplete", "FadeInFinish");
        iTween.ValueTo(gameObject, ht);
    }
    void FadeOut()
    {
        canvasGroup.interactable = false;
        Hashtable ht = iTween.Hash("from", 1, "to", 0, "time", timeOut, "onupdate", "UpdateAlphaCanvas", "onComplete", "FadeOutFinish");
        iTween.ValueTo(gameObject, ht);
    }
    void UpdateAlphaCanvas(float value)
    {
        canvasGroup.alpha = value;
    }
    protected virtual void FadeInFinish()
    {
        canvasGroup.alpha = 1;
        canvasGroup.interactable = true;
    }
    protected virtual void FadeOutFinish()
    {
        guiMain.SetActive(false);
    }
}
