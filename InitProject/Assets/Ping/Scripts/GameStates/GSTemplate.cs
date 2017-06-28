using System.Collections;
using UnityEngine;

////////////////////////////////////////////////////////
//Author:
//TODO: a game state sample
////////////////////////////////////////////////////////

public class GSTemplate : IState
{
    public GameObject guiMain;
    protected bool isFirst;

    protected override void Awake()
    {
        base.Awake();
        guiMain.SetActive(false);
        isFirst = true;
    }

    /// <summary>
    ///     One time when start
    /// </summary>
    protected virtual void init()
    {
    }

    /// <summary>
    /// Back key
    /// </summary>
    public virtual void onBackKey()
    {
    }

    protected virtual void onCheatState()
    {
    }

    public override void onSuspend()
    {
        base.onSuspend();
        GameStatesManager.onBackKey = null;
        GameStatesManager.onCheatState = null;
        guiMain.SetActive(false);
    }

    public override void onResume()
    {
        base.onResume();
        GameStatesManager.onBackKey = onBackKey;
        GameStatesManager.onCheatState = onCheatState;
        guiMain.SetActive(true);
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