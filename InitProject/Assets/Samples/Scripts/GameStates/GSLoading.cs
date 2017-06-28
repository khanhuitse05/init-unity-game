using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GSLoading : GSTemplate
{
    public static GSLoading Instance { get; private set; }
    protected override void Awake()
    {
        base.Awake();
        Instance = this;
    }
    protected override void init()
    {
        GamePreferences.LoadSetting();
        LocalizationData.LoadLocalization();
        Invoke("Finish", 2);
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

    void Finish()
    {
        GameStatesManager.SwitchState(GSHome.Instance);
        Destroy(gameObject);
    }
}

