using UnityEngine;
using UnityEngine.UI;

public class GSResult : GSTemplate
{
    static IState _instance;
    public static IState Instance { get { return _instance; } }
    public Text lbCurrentScore;
    public Text lbBestScore;
    public Text lbStar;

    protected override void Awake()
    {
        base.Awake();
        _instance = this;
    }
    protected override void init()
    {
        showResult(100);
        if (GSGamePlay.countPlaygame == 7 && GamePreferences.profile.rate < 2)
        {
            GamePreferences.profile.rate++;
            GamePreferences.saveProfile();
            PopupManager.Instance.InitYesNoPopUp("Love Block Dash?\nTell other how you fell.", onBtnRateClick, null, "RATE", "LATER");
        }
    }
    void showResult(int _score)
    {
        lbBestScore.text = "BEST   " + GamePreferences.profile.highScore.ToString();
        lbCurrentScore.text = _score.ToString();
        lbStar.text = GamePreferences.profile.star.ToString();
    }
    protected override void onBackKey()
    {
        onBtnHomeClick();
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
    public void onBtnBoardClick()
    {
    }
    public void onBtnHomeClick()
    {
        GameStatesManager.Instance.stateMachine.SwitchState(GSHome.Instance);
    }
    public void onBtnShopClick()
    {
        GameStatesManager.Instance.stateMachine.SwitchState(GSShop.Instance);
    }

}