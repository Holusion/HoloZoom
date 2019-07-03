using System.Collections.Generic;

public class State {

    public string name { get; private set; }
    private List<Transition> transitions;

    public State(string name)
    {
        this.name = name;
        this.transitions = new List<Transition>();
    }

    public void AddTransition(Transition t)
    {
        foreach(Transition transition in this.transitions)
        {
            if(transition.trigger == t.trigger)
            {
                throw new System.Exception("A trigger for this action already exist");
            }
        }
        this.transitions.Add(t);
    }

    public State Step(string trigger)
    {
        Transition transition = null;
        foreach(Transition t in this.transitions)
        {
            if(t.trigger == trigger)
            {
                transition = t;
                break;
            }
        }
        if (transition == null) throw new System.Exception("No transition found");
        return transition.targetState;
    }
}
