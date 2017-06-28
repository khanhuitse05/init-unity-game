using UnityEngine;
using System;
using System.Collections;

public class GameStatesManager : MonoBehaviour
{
    public static GameStatesManager Instance { get; private set; }
    public static bool enableBackKey = true;
    public static Action onBackKey { get; set; }
    public static Action onCheatState { get; set; }
    public StateMachine stateMachine;
    public IState defaultState;
    void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        stateMachine.PushState(defaultState);
    }

    public static void PushState(IState state) { Instance.stateMachine.PushState(state); }
    public static void SwitchState(IState state) { Instance.stateMachine.SwitchState(state); }
    public static void PopState(IState stateDefault = null) { Instance.stateMachine.PopState(stateDefault); }
    public static IState currentState { get { return Instance.stateMachine.currentState; } }

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