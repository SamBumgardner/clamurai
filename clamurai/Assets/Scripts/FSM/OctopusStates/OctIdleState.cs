using UnityEngine;

public class OctIdleState : State<Octopus>
{
    public OctIdleState(Octopus octopus, StateMachine<Octopus> stateMachine) : base(octopus, stateMachine) {}

    public override int HandleInput()
    {
        // Check distance to player to see if it should enter chase state
        return base.HandleInput();
    }

    public override void PhysicsUpdate()
    {
        // passively bob vertically, patrol short distance left & right
        base.PhysicsUpdate();
    }

    public override void Enter()
    {
        base.Enter();
    }
}