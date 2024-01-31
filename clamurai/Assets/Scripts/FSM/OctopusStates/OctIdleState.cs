using UnityEngine;

public class OctIdleState : State<Octopus>
{
    public OctIdleState(Octopus octopus, StateMachine<Octopus> stateMachine) : base(octopus, stateMachine) {}

    float maxBobVelocityY = .5f;
    float bobAccelY = .025f;
    float directionY = 1;

    float patrolTimer = 0;
    bool isWaiting = false;

    public override int HandleInput()
    {
        // It'd be cool to have angles of valid vision, but instead we'll just settle for distance.
        if (owner.GetVectorToPlayer().magnitude <= owner.patrolVisionDistance)
        {
            return (int)OctStates.CHASE;
        }

        return base.HandleInput();
    }

    public override void PhysicsUpdate()
    {
        // passively bob vertically, patrol short distance left & right
        var currentVelocity = owner.rb.velocity;
        var newVelocityY = GetBobbingVelocityY(currentVelocity.y);
        var newVelocityX = GetPatrolVelocityX();

        owner.rb.velocity = new Vector2(newVelocityX, newVelocityY);
        base.PhysicsUpdate();
    }

    private float GetBobbingVelocityY(float currentVelocityY)
    {
        // add to Y velocity a static amount until hitting target speed, then reverse direction
        if (currentVelocityY > maxBobVelocityY)
        {
            currentVelocityY = maxBobVelocityY; 
            directionY = -1;
        }
        else if (currentVelocityY < -maxBobVelocityY)
        {
            currentVelocityY = -maxBobVelocityY;
            directionY = 1;
        }
        
        return currentVelocityY + bobAccelY * directionY;
    }

    private float GetPatrolVelocityX()
    {
        patrolTimer -= Time.deltaTime;
        if (patrolTimer <= 0)
        {
            if (isWaiting)
            {
                owner.directionX *= -1;
            }
            isWaiting = !isWaiting;
            patrolTimer = owner.patrolTime;
        }

        return isWaiting ? 0 : owner.patrolVelocityX * owner.directionX;
    }

    public override void Enter()
    {
        owner.animator.Play("idle");
        owner.rb.velocity = Vector2.zero;
        base.Enter();
    }
}