public class OctDyingState : OctBaseState
{
    public OctDyingState(Octopus octopus, StateMachine<Octopus> stateMachine) : base(octopus, stateMachine) { }

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
        // Need to make sure all stuff gets disabled so it can't damage anything
        // Play animation for getting defeated. 
        base.Enter();
    }
}