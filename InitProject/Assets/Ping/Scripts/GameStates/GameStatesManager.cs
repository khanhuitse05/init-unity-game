using UnityEngine;
using System;
using System.Collections;

namespace Ping
{
    public class GameStatesManager : MonoBehaviour
    {
        public static GameStatesManager Instance { get; private set; }
        public static bool enableBackKey = true;
        public static Action onBackKey { get; set; }
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

        public static void PushState(IState state, SwipeEffect effect = SwipeEffect.Active) { Instance.stateMachine.PushState(state, effect); }
        public static void SwitchState(IState state, SwipeEffect effect = SwipeEffect.Active) { Instance.stateMachine.SwitchState(state, effect); }
        public static void PopState(IState stateDefault = null, SwipeEffect effect = SwipeEffect.Active) { Instance.stateMachine.PopState(stateDefault, effect); }
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
        }

        public IEnumerator checkInternetConnection(Action<bool> action)
        {
            WWW www = new WWW("http://google.com");
            yield return www;
            if (www.error != null)
                action(false);
            else
                action(true);
        }
    }
}