using UnityEngine;
using System;

public class GameStatesManager : MonoBehaviour
{
    static GameStatesManager _instance;
    public static GameStatesManager Instance { get { return _instance; } }
    public static bool enableBackKey = true;
    public StateMachine stateMachine { get; set; }
    public GameObject InputProcessor { get; set; }
    public static Action onBackKey { get; set; }
    void Awake()
    {
        _instance = this;
    }
    // Use this for initialization
    void Start()
    {
        GamePreferences.Instance.LoadSetting();
        AudioManager.SetSFXVolume(GamePreferences.Instance.setting.soundVolume);
        GameConstants.Instance.Init();
        stateMachine.PushState(GSHome.Instance);
    }
    // Update is called once per frame
    void Update()
    {
        if (onBackKey != null && Input.GetKeyDown(KeyCode.Escape))
        {
            if (enableBackKey)
            {
                onBackKey();
            }
        }
    }
}