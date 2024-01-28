using UnityEngine;

public class CrabWalkState : CrabBaseState
{
    public CrabWalkState(Crab crab, StateMachine<Crab> stateMachine) : base(crab, stateMachine) {}

    private const float WALK_SPEED = 3;
    private float WALL_BUMP_DISTANCE = Crab.DIST_SIDE + .05f;

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
        // Check if bumping wall and should turn around
        Debug.DrawRay(owner.transform.position, Vector2.right * owner.direction * WALL_BUMP_DISTANCE, Color.cyan);
        if (Physics2D.Raycast(owner.transform.position, Vector2.right * owner.direction, WALL_BUMP_DISTANCE, layerMask: owner.terrainMask).collider != null)
        {
            owner.direction *= -1;
        }

        owner.rb.velocity = new Vector2(WALK_SPEED * owner.direction, 0);
        base.PhysicsUpdate();
    }

    public override void Enter()
    {
        base.Enter();
    }
}