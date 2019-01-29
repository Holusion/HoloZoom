using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine {

    protected State currentState;

    public StateMachine()
    {
    }

    public void Step(string trigger, Action func)
    {
        try
        {
            this.currentState = currentState.Step(trigger);
            Debug.Log(trigger);
            func();
        } catch(System.Exception e)
        {
            Debug.Log(e.Message);
        }
    }
}
