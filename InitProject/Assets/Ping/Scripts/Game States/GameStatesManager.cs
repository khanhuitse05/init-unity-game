using UnityEngine;
using System;

public class GameStatesManager : MonoBehaviour
{
    static GameStatesManager _instance;
    public static GameStatesManager Instance { get { return _instance; } }
    public StateMachine stateMachine;
    public static bool enableBackKey = true;
    public StateMachine StateMachine
    {
        get { return stateMachine; }
        set { stateMachine = value; }
    }
    GameObject inputProcessor;
    public UnityEngine.GameObject InputProcessor
    {
        get { return inputProcessor; }
        set { inputProcessor = value; }
    }
    static Action onBackKey;
    public static Action OnBackKey
    {
        get { return onBackKey; }
        set { onBackKey = value; }
    }
    void Awake()
    {
        _instance = this;
    }
    // Use this for initialization
    void Start()
    {
        Profile gameProfile = null;
        gameProfile = GamePreferences.initProfile();
        AudioManager.SetSFXVolume(gameProfile.soundVolume);
        GameConstants.Instance.Init();
        stateMachine.PushState(GSHome.Instance);
    }
    // Update is called once per frame
    void Update()
    {
        if (OnBackKey != null && Input.GetKeyDown(KeyCode.Escape))
        {
            if (enableBackKey)
            {
                OnBackKey();
            }
        }
    }
}