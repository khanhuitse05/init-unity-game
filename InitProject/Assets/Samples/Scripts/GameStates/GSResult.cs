using UnityEngine;
using UnityEngine.UI;

public class GSResult : GSTemplateZoom
{
    public static GSResult Instance { get; private set; }
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
    public void OnClickPlay()
    {
        GameStatesManager.SwitchState(GSGamePlay.Instance);
    }
    public void OnClickHome()
    {
        GameStatesManager.SwitchState(GSHome.Instance);
    }
}
