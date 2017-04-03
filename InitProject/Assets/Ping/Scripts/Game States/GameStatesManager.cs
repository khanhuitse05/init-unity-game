using UnityEngine;
using System;
using System.Collections;

public class GameStatesManager : MonoBehaviour
{
    static GameStatesManager _instance;
    public static GameStatesManager Instance { get { return _instance; } }
    public static bool enableBackKey = true;
    public GameObject InputProcessor { get; set; }
    public static Action onBackKey { get; set; }
    public static Action onCheatState { get; set; }
    public StateMachine stateMachine;
    public IState defaultState;
    void Awake()
    {
        _instance = this;
    }
    void Start()
    {
        stateMachine.PushState(defaultState);
    }
    void Update()
    {
        if (onBackKey != null && Input.GetKeyDown(KeyCode.Escape))
        {
            if (enableBackKey)
            {
                onBackKey();
            }
        }
#if !LIVE
#if UNITY_EDITOR
        if (onCheatState != null && Input.GetKeyDown(KeyCode.F2))
        {
            onCheatState();
        }
        if (Input.GetKeyDown(KeyCode.F1))
        {
            OnCheat();
        }
#else
        if (onCheatState != null && Input.touches.Length == 3)
        {
            onCheatState();
        }
        if (Input.touches.Length == 4)
        {
            OnCheat();
        }
#endif
#endif
    }

    public IEnumerator checkInternetConnection(Action<bool> action)
    {
        WWW www = new WWW("http://google.com");
        yield return www;
        if (www.error != null)
        {
            action(false);
        }
        else {
            action(true);
        }
    }
    public void OnCheat()
    {
        PopupManager.Instance.InitMessage("On Cheat()");
    }
}