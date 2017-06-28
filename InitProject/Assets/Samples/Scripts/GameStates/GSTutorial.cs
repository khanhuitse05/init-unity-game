using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GSTutorial : GSTemplate
{
    public static GSTutorial Instance { get; private set; }
    protected override void Awake()
    {
        base.Awake();
        Instance = this;
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
    public override void onBackKey()
    {
    }
}

