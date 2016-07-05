using UnityEngine;

public class GSGamePlay : GSTemplate
{
    static IState _instance;
    public static IState Instance { get { return _instance; } }
    public static int countPlaygame = 0;
    public GameObject guiHUD;
    
    protected override void Awake()
    {
        base.Awake();
        _instance = this;
        guiHUD.gameObject.SetActive(false);
    }

    protected override void init()
    {
    }

    protected override void onBackKey()
    {
    }
}
