using System.Collections;
using UnityEngine;

////////////////////////////////////////////////////////
//Author:
//TODO: a game state sample
////////////////////////////////////////////////////////

public class GSTemplate : IState
{
    public GameObject guiMain;
    public AudioClip musicClip;
    CanvasGroup canvasGroup;
    bool isFirst;
    protected float timeIn = 0.5f;
    protected float timeOut = 0.5f;
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
        if (canvasGroup != null)
        {
            StartCoroutine("FadeOut");
        }
        else
        {
            guiMain.SetActive(false);
        }
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
        if (canvasGroup != null)
        {
            guiMain.SetActive(true);
            canvasGroup.alpha = 0;
            StartCoroutine("FadeIn");
        }
        else
        {
            guiMain.SetActive(true);
        }
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
    IEnumerator FadeIn()
    {
        canvasGroup.interactable = false;
        while (canvasGroup.alpha < 1)
        {
            canvasGroup.alpha += Time.deltaTime / timeIn;
            yield return null;
        }
        canvasGroup.interactable = true;
    }
    IEnumerator FadeOut()
    {
        canvasGroup.interactable = false;
        while (canvasGroup.alpha > 0)
        {
            canvasGroup.alpha -= Time.deltaTime / timeOut;
            yield return null;
        }
        guiMain.SetActive(false);
    }
}
