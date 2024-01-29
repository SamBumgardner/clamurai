using System;
using UnityEngine;

public class RunState : GroundState
{
	public RunState(Player player, StateMachine<Player> stateMachine) : base(player, stateMachine) { }

    public override int HandleInput()
    {
        if(Input.GetButtonDown("Jump"))
        {
            return (int)PlayerStates.JUMP;
        }

        var horizontalAxis = Input.GetAxis("Horizontal");
		if (horizontalAxis == 0)
        {
            return (int)PlayerStates.STAND;
        }
        else // attempt facing switch
        {
            var newXScale = horizontalAxis > 0 ? 1 : -1;
            var scale = owner.transform.localScale;
            scale.x = newXScale;
            owner.transform.localScale = scale;
        }

        return base.HandleInput();
    }

    public override void PhysicsUpdate()
    {
        
        var horizontalAxis = Input.GetAxis("Horizontal");
        float newXVelocity = owner.rb.velocity.x + (owner.groundAccel * horizontalAxis);
        if (horizontalAxis > 0)
        {
            newXVelocity = Math.Min(newXVelocity, owner.runSpeedMax);
        }
        else if (horizontalAxis < 0)
        {
            newXVelocity = Math.Max(newXVelocity, -owner.runSpeedMax);
        }

        owner.rb.velocity = new Vector2(newXVelocity, 0);

        base.PhysicsUpdate();
    }

    public override void Enter()
    {
        owner.animationToPlay = "run";
        base.Enter();
    }
}