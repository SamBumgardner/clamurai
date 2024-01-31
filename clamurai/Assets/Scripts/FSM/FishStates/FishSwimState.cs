using UnityEngine;

public class FishSwimState : State<Fish>
{
	public FishSwimState(Fish fish, StateMachine<Fish> stateMachine) : base(fish, stateMachine) { }

    public override void PhysicsUpdate()
    {
        // add to Y velocity a static amount until hitting target speed, then reverse direction
        var currentVelocity = owner.rb.velocity;
        if (owner.rb.velocity.y > owner.maxVelocity.y)
        {
            owner.directionY = -1;
        } 
        else if (owner.rb.velocity.y < -owner.maxVelocity.y)
        {
            owner.directionY = 1;
        }

        owner.rb.velocity = new Vector2(owner.maxVelocity.x * owner.directionX, 
            owner.rb.velocity.y + owner.verticalSpeedChange * owner.directionY);

        base.PhysicsUpdate();
    }

    public override void Enter()
    {
        owner.rb.velocity = new Vector2(owner.maxVelocity.x * owner.directionX, 0);
        base.Enter();
    }
}