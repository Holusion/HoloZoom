public class Transition {

    public State targetState { get; private set; }
    public string trigger { get; private set; }

	public Transition(State targetState, string trigger)
    {
        this.targetState = targetState;
        this.trigger = trigger;
    }
}
