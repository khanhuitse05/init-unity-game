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
        score = parameters[GameConstants.key_result_score] as ResultScore;
        if (score != null)
        {
            txtCurrentScore.text = score.curentScore.ToString();
            txtHighScore.text = score.highScore.ToString();
        }
        else
        {
            txtCurrentScore.text = "";
            txtHighScore.text = GamePreferences.setting.highScore.ToString();
        }
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
    ResultScore score;
    public Text txtCurrentScore;
    public Text txtHighScore;
    public void OnClickPlay()
    {
        GameStatesManager.SwitchState(GSGamePlay.Instance);
    }
    public void OnClickHome()
    {
        GameStatesManager.SwitchState(GSHome.Instance);
    }
}
public class ResultScore
{
    public int curentScore;
    public int highScore;
}
