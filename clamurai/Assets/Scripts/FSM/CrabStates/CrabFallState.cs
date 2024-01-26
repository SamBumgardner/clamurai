using UnityEngine;

public class CrabFallState : State<Crab>
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
        owner.GetComponent<SpriteRenderer>().color = Color.yellow;
        base.Enter();
    }
}