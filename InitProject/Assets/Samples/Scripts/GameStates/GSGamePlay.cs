using UnityEngine;
using UnityEngine.UI;

public class GSGamePlay : GSTemplateZoom
{
    public static GSGamePlay Instance { get; private set; }
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
        count = 0;
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

    //
    public Text txtTime;
    float count;
    private void Update()
    {
        count += Time.deltaTime;
        txtTime.text = "" + (int)count + "s";
    }
    public void ClickFinish()
    {
        GamePreferences.setting.updateHighScore((int)count);
        ResultScore _score = new ResultScore();
        _score.curentScore = (int)count;
        _score.highScore = GamePreferences.setting.highScore;
        GSResult.Instance.parameters[GameConstants.key_result_score] = _score;
        GameStatesManager.SwitchState(GSResult.Instance);
    }
}
