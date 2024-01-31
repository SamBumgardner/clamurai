public class OctChaseState : OctBaseState
{
    public OctChaseState(Octopus octopus, StateMachine<Octopus> stateMachine) : base(octopus, stateMachine) { }

    public override int HandleInput()
    {
        // Check distance to player to see if it should return to idle
        // Check cooldown to see if it can do a lunging strike - begins charging if so
        return base.HandleInput();
    }

    public override void PhysicsUpdate()
    {
        // bursts of decaying movement, when velocity reaches 0 do another burst. Only track on player with initial movement
        base.PhysicsUpdate();
    }

    public override void Enter()
    {
        base.Enter();
    }
}