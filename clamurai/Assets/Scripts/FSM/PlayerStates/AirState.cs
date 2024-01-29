using System;
using UnityEngine;

public class AirState : State<Player>
{
	protected AirState(Player player, StateMachine<Player> stateMachine) : base(player, stateMachine) { }

    public override int HandleInput()
    {
        float horizontalAxis = Input.GetAxis("Horizontal");
        if (horizontalAxis != 0.0f)
        {
            var newXScale = horizontalAxis > 0 ? 1 : -1;
            var scale = owner.transform.localScale;
            scale.x = newXScale;
            owner.transform.localScale = scale;
        }
        
        if (owner.tookDamage)
        {
            return (int)PlayerStates.HURT;
        }

        // Velocity check makes sure this doesn't glue player back to ground when starting jump
        // Value doesn't matter too much, just check its positive and somewhat larger than 0.
        if (owner.IsOnGround() && owner.rb.velocity.y <= 1) 
        {
            return (int)PlayerStates.STAND;
        }

        return base.HandleInput();
    }

    public override void PhysicsUpdate()
    {
        var horizontalAxis = Input.GetAxis("Horizontal");
        float newXVelocity = owner.rb.velocity.x + (owner.jumpDriftAccel * horizontalAxis);
        if (horizontalAxis > 0)
        {
            newXVelocity = Math.Min(newXVelocity, owner.runSpeedMax);
        }
        else if (horizontalAxis < 0)
        {
            newXVelocity = Math.Max(newXVelocity, -owner.runSpeedMax);
        }

        owner.rb.velocity = new Vector2(newXVelocity, owner.rb.velocity.y);

        base.PhysicsUpdate();
    }
}