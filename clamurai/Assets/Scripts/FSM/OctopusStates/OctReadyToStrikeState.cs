public class OctReadyToStrikeState : State<Octopus>
{
    public OctReadyToStrikeState(Octopus octopus, StateMachine<Octopus> stateMachine) : base(octopus, stateMachine) { }

    public override int HandleInput()
    {
        // Check internal timer to decide when to move to Strike state. 
        // Check for hurt-ed-ness, if so, set variables for "counter attack energy" or some such so it'll retaliate more quickly after recovery.
        return base.HandleInput();
    }

    public override void PhysicsUpdate()
    {
        // No movement here, stillness shows danger.
        base.PhysicsUpdate();
    }

    public override void Enter()
    {
        base.Enter();
    }
}