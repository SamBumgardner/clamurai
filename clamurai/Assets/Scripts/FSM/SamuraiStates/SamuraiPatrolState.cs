using UnityEngine;

public class SamuraiPatrolState : SamuraiBaseState
{
    public SamuraiPatrolState(Samurai samurai, StateMachine<Samurai> stateMachine) : base(samurai, stateMachine) {}

    float patrolTimer = 0;
    bool isWaiting = false;

    public override int HandleInput()
    {
        // It'd be cool to have angles of valid vision, but instead we'll just settle for distance.
        var vectorToPlayer = owner.GetVectorToPlayer();
        if (vectorToPlayer.magnitude <= owner.patrolVisionDistance
            && System.Math.Sign(owner.directionX) == System.Math.Sign(vectorToPlayer.x))
        {
            return (int)SamuraiStates.CHASE;
        }

        return base.HandleInput();
    }

    public override void PhysicsUpdate()
    {
        // patrol short distance left & right
        var newVelocityX = GetPatrolVelocityX();

        owner.rb.velocity = new Vector2(newVelocityX, owner.rb.velocity.y);
        base.PhysicsUpdate();
    }

    private float GetPatrolVelocityX()
    {
        patrolTimer -= Time.deltaTime;
        if (patrolTimer <= 0)
        {
            if (isWaiting)
            {
                owner.animationToPlay = "walk";
                owner.directionX *= -1;
            }
            else
            {
                owner.animationToPlay = "stop";
            }
            isWaiting = !isWaiting;
            patrolTimer = owner.patrolTime;
        }

        return isWaiting ? 0 : owner.patrolVelocityX * owner.directionX;
    }

    public override void Enter()
    {
        owner.animationToPlay = "walk";
        owner.rb.velocity = Vector2.zero;
        patrolTimer = owner.patrolTime;
        base.Enter();
    }
}