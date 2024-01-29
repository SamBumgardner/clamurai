using System;
using UnityEngine;

public class StandState : GroundState
{
	public StandState(Player player, StateMachine<Player> stateMachine) : base(player, stateMachine) { }

    public override int HandleInput()
    {
        if(Input.GetButtonDown("Jump"))
        {
            return (int)PlayerStates.JUMP;
        }

        var horizontalAxis = Input.GetAxis("Horizontal");
		if (horizontalAxis != 0)
        {
            return (int)PlayerStates.RUN;
        }

        return base.HandleInput();
    }

    public override void PhysicsUpdate()
    {
        float currentVelocityX = owner.rb.velocity.x;
        float newVelocityX = currentVelocityX;
        if (currentVelocityX > 0)
        {
            newVelocityX = Math.Max(newVelocityX - owner.groundAccel, 0);
        }
        else if (currentVelocityX < 0)
        {
            newVelocityX = Math.Min(newVelocityX + owner.groundAccel, 0);
        }

        owner.rb.velocity = new Vector2(newVelocityX, 0);
        base.PhysicsUpdate();
    }

    public override void Enter()
    {
        owner.animationToPlay = "stand";
        base.Enter();
    }
}