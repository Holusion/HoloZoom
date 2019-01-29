using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomStateMachine : StateMachine
{
    public ZoomStateMachine() : base()
    {
        State initState = new State("Idle");
        State zoom = new State("Zoom");
        State unzoom = new State("Unzoom");
        State rotation = new State("Rotation");

        Transition select = new Transition(zoom, "select");
        Transition unselect = new Transition(unzoom, "unselect");
        Transition rotate = new Transition(rotation, "rotate");

        initState.AddTransition(select);
        initState.AddTransition(new Transition(initState, ""));
        zoom.AddTransition(unselect);
        zoom.AddTransition(rotate);
        unzoom.AddTransition(new Transition(initState, ""));
        rotation.AddTransition(rotate);
        rotation.AddTransition(unselect);

        this.currentState = initState;
    }
}
