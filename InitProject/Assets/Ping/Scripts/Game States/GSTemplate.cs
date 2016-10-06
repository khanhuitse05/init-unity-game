using UnityEngine;

////////////////////////////////////////////////////////
//Author:
//TODO: a game state sample
////////////////////////////////////////////////////////

public class GSTemplate : IState
{
    public GameObject guiMain;
    public AudioClip musicClip;
    bool isFirst;
    protected override void Awake()
    {
        base.Awake();
        Utils.setActive(guiMain, false);
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
        guiMain.SetActive(false);
        GameStatesManager.onBackKey = null;
    }
    public override void onResume()
    {
        base.onResume();
        GameStatesManager.Instance.InputProcessor = guiMain;
        GameStatesManager.onBackKey = onBackKey;
        guiMain.SetActive(true);
        if (musicClip != null)
        {
            AudioManager.PlayMusic(musicClip, true);
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
}
