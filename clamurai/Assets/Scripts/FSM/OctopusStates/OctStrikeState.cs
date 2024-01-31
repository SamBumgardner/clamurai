public class OctStrikeState : State<Octopus>
{
    public OctStrikeState(Octopus octopus, StateMachine<Octopus> stateMachine) : base(octopus, stateMachine) { }

    public override int HandleInput()
    {
        // if strike is ending, return to chase state
        return base.HandleInput();
    }

    public override void PhysicsUpdate()
    {
        // Just move in the initial selected velocity
        base.PhysicsUpdate();
    }

    public override void Enter()
    {
        // Set lunge velocity, change color to show invulnerability
        base.Enter();
    }

    // Implement method to receive callback when strike animation's done?
}