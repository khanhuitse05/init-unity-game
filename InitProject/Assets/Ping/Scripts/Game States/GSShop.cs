using UnityEngine;
using UnityEngine.UI;

public class GSShop : GSTemplate
{
    static IState _instance;
    public static IState Instance { get { return _instance; } }
    public GameObject pfItem;
    public Text txtStar;


    protected override void Awake()
    {
        base.Awake();
        _instance = this;
    }
    protected override void init()
    {
        txtStar.text = GamePreferences.profile.star.ToString();
        LoadShop();
    }
    void LoadShop()
    {
    }
    protected override void onBackKey()
    {
        onBtnOkClick();
    }

    public void onBtnOkClick()
    {
        GamePreferences.saveProfile();
        GameStatesManager.Instance.stateMachine.SwitchState(GSGamePlay.Instance);
    }

}