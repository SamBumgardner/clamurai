public class OctBaseState : State<Octopus>
{
    public OctBaseState(Octopus octopus, StateMachine<Octopus> stateMachine) : base(octopus, stateMachine) {}

    public override int HandleInput()
    {
        // check for transition to hurt state
        return base.HandleInput();
    }
}