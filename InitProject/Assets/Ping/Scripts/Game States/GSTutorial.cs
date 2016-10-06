using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GSTutorial : GSTemplate
{
    static IState _instance;
    public static IState Instance { get { return _instance; } }
    protected override void Awake()
    {
        base.Awake();
        _instance = this;
    }
    protected override void init()
    {
    }
    public override void onEnter()
    {
        base.onEnter();
    }
    public override void onResume()
    {
        base.onResume();
    }
    public override void onSuspend()
    {
        base.onSuspend();
    }
    public override void onExit()
    {
        base.onExit();
    }
    protected override void onBackKey()
    {
    }
}

