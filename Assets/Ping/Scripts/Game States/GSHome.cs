using UnityEngine;
public class GSHome : GSTemplate
{
    static IState _instance;
    public static IState Instance { get { return _instance; } }
    bool isNewLaunch = true;

    protected override void Awake()
    {
        base.Awake();
        _instance = this;
    }
    protected override void init()
    {
        if (GamePreferences.profile.EnableTutorial)
        {
            GameStatesManager.Instance.stateMachine.SwitchState(GSTutorial.Instance);
        }
        if (isNewLaunch)
        {
            isNewLaunch = false;
            GamePreferences.submitScore(GamePreferences.profile.HighScore);
        }
    }
    public void onBtnPlayClick()
    {
        GameStatesManager.Instance.stateMachine.SwitchState(GSGamePlay.Instance);
    }
    public void onBtnLikeClick()
    {
        Application.OpenURL("https://www.facebook.com/");
    }
    public void onBtnHowToPlay()
    {
        GameStatesManager.Instance.stateMachine.SwitchState(GSTutorial.Instance);
    }
    public void onBtnRateClick()
    {
#if UNITY_IOS
            Application.OpenURL("");
#elif UNITY_ANDROID
            Application.OpenURL("https://play.google.com/");
#endif
    }
    public void onBtnLearderBoardClick()
    {
        PopupManager.Instance.InitMesage("not available");
    }
    public void onBtnCustomizeClick()
    {
        GameStatesManager.Instance.stateMachine.SwitchState(GSShop.Instance);
    }
}