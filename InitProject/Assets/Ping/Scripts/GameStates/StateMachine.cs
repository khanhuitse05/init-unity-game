using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Ping
{
    public class StateMachine : MonoBehaviour
    {
        Stack<IState> stateStack = new Stack<IState>();
        public void PushState(IState state, SwipeEffect effect = SwipeEffect.Active)
        {
            IState prevState = null;
            if (stateStack.Count > 0)
            {
                prevState = stateStack.Peek();
                prevState.onSuspend(effect);
            }
            stateStack.Push(state);
            state.onEnter(effect);
        }

        public void SwitchState(IState state, SwipeEffect effect = SwipeEffect.Active)
        {
            IState prevState = null;
            while (stateStack.Count > 0)
            {
                prevState = stateStack.Pop();
                prevState.onExit(effect);
            }
            stateStack.Push(state);
            state.onEnter(effect);
        }

        public void PopState(IState stateDefault = null, SwipeEffect effect = SwipeEffect.Active)
        {
            IState prevState = null;
            if (stateStack.Count > 0)
            {
                prevState = stateStack.Pop();
                prevState.onExit(effect);
            }
            if (stateStack.Count > 0)
            {
                IState thisState = stateStack.Peek();
                thisState.onResume(effect);
            }
            else
            {
                stateStack.Push(stateDefault);
                stateDefault.onEnter(effect);
            }
        }

        public IState currentState
        {
            get
            {
                if (stateStack.Count > 0) return stateStack.Peek();
                else return null;
            }
        }
    }
    public abstract class IState : MonoBehaviour
    {
        public virtual void onResume(SwipeEffect effect)
        {
        }
        public virtual void onSuspend(SwipeEffect effect)
        {
        }
        public virtual void onEnter(SwipeEffect effect)
        {
        }
        public virtual void onExit(SwipeEffect effect)
        {
        }
        protected virtual void Awake() { }
        protected virtual void Update() { }
    }
    public enum SwipeEffect
    {
        Active,
        SlideLeft,
        SlideRight,
        Fade,
        Zome
    }
}