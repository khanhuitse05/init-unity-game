using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class IState : MonoBehaviour
{
    public ParameterWrapper parameters = new ParameterWrapper();
    protected virtual void Awake()
    {
        this.gameObject.SetActive(false);
    }
    public virtual void onSuspend()
    { }
    public virtual void onResume()
    { }
    public virtual void onEnter()
    {
        this.gameObject.SetActive(true);
    }
    public virtual void onExit()
    {
        this.gameObject.SetActive(false);
        parameters.Clear();
    }
}

public class StateMachine : MonoBehaviour
{
    Stack<IState> stateStack = new Stack<IState>();
    public AudioClip clip;
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

    public void SwitchState(IState state, bool sound = true)
    {
        IState prevState = null;
        if (sound && clip)
        {
            AudioSource.PlayClipAtPoint(clip, Vector3.zero);
        }
        while (stateStack.Count > 0)
        {
            prevState = stateStack.Pop();
            prevState.onSuspend();
            prevState.onExit();
        }
        stateStack.Push(state);
        state.onEnter();
    }

    public void PopState()
    {
        IState prevState = null;
        if (stateStack.Count > 0)
        {
            prevState = stateStack.Pop();
            prevState.onSuspend();
            prevState.onExit();
        }
        IState thisState = stateStack.Peek();
        thisState.onResume();
    }

    public void StartState(IState state)
    {
        state.onEnter();
    }

    public void EndState(IState state)
    {
        state.onSuspend();
        state.onExit();
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