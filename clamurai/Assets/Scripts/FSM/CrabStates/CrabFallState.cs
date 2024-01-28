using UnityEngine;

public class CrabFallState : CrabBaseState
{
    public CrabFallState(Crab crab, StateMachine<Crab> stateMachine) : base(crab, stateMachine) { }

    public override int HandleInput()
    {
        // If crab isn't touching the ground, change state to falling.
        if (owner.IsOnGround())
        {
            return (int)CrabStates.WALK;
        }

        return base.HandleInput();
    }

    public override void Enter()
    {
        base.Enter();
    }
}