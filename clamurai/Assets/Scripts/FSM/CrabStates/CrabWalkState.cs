using UnityEngine;

public class CrabWalkState : State<Crab>
{
    public CrabWalkState(Crab crab, StateMachine<Crab> stateMachine) : base(crab, stateMachine) { }

    private const float WALK_SPEED = 3;
    private int direction = 1;

    public override int HandleInput()
    {
        // If crab isn't touching the ground, change state to falling.
        if (!owner.IsOnGround())
        {
            return (int)CrabStates.FALL;
        }

        return base.HandleInput();
    }

    public override void PhysicsUpdate()
    {
        owner.rb.velocity = new Vector2(WALK_SPEED * owner.direction, 0);
        base.PhysicsUpdate();
    }

    public override void Enter()
    {
        owner.GetComponent<SpriteRenderer>().color = Color.blue;
        base.Enter();
    }
}