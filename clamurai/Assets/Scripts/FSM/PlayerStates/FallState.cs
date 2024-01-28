using UnityEngine;

public class FallState : AirState
{
	public FallState(Player player, StateMachine<Player> stateMachine) : base(player, stateMachine) { }

    public override void Enter()
    {
        owner.animationToPlay = "fall";
        if (owner.rb.velocity.y > Player.FALL_YSPEED_CUTOFF) 
        {
            owner.rb.velocity = new Vector2(owner.rb.velocity.x, Player.FALL_YSPEED_CUTOFF);
        }
        base.Enter();
    }
}