using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class IState : MonoBehaviour
{
    public ParameterWrapper parameters = new ParameterWrapper();
    protected virtual void Awake()
    { }
    public virtual void onSuspend()
    { }
    public virtual void onResume()
    { }
    public virtual void onEnter()
    { }
    public virtual void onExit()
    {
        parameters.Clear();
    }
}

public class StateMachine : MonoBehaviour
{
    Stack<IState> stateStack = new Stack<IState>();
    public void PushState(IState state)
    {
        IState prevState = null;
        if (stateStack.Count > 0)
        {
            prevState = stateStack.Peek();
            prevState.onSuspend();
        }
        stateStack.Push(state);
        state.onEnter();
    }

    public void SwitchState(IState state)
    {
        IState prevState = null;
        while (stateStack.Count > 0)
        {
            prevState = stateStack.Pop();
            prevState.onExit();
        }
        stateStack.Push(state);
        state.onEnter();
    }

    public void PopState(IState stateDefault = null)
    {
        IState prevState = null;
        if (stateStack.Count > 0)
        {
            prevState = stateStack.Pop();
            prevState.onExit();
        }
        if (stateStack.Count > 0)
        {
            IState thisState = stateStack.Peek();
            thisState.onResume();
        }
        else
        {
            stateStack.Push(stateDefault);
            stateDefault.onEnter();
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