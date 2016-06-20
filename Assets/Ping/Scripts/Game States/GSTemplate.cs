using UnityEngine;

////////////////////////////////////////////////////////
//Author:
//TODO: a game state sample
////////////////////////////////////////////////////////

public class GSTemplate : IState
{
    public GameObject guiMain;
    public AudioClip musicClip;
    protected override void Awake()
    {
        base.Awake();
        Utils.setActive(guiMain, false);
    }
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
        GameStatesManager.OnBackKey = null;
    }
    public override void onResume()
    {
        GameStatesManager.OnBackKey = onBackKey;
        if (musicClip != null)
        {
            AudioManager.PlayMusic(musicClip, true);
        }
    }
    public override void onEnter()
    {
        base.onEnter();
        guiMain.SetActive(true);
        GameStatesManager.Instance.InputProcessor = guiMain;
        init();
        onResume();
    }
    public override void onExit()
    {
        base.onExit();
        guiMain.SetActive(false);
    }
}
