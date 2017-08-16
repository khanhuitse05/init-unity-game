using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GSSetting : GSTemplateFade
{
    public static GSSetting Instance { get; private set; }
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