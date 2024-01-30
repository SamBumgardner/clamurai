using UnityEngine;

public class DyingState : State<Player>
{
    public float hurtTimerMax = 1;
    public float hurtTimer = 0;

	public DyingState(Player player, StateMachine<Player> stateMachine) : base(player, stateMachine) { }

    public override void PhysicsUpdate()
    {
        if (owner.IsOnGround())
        {
            owner.animationToPlay = "dead";
            owner.rb.velocity = new Vector2(0, 0);
            base.PhysicsUpdate();
        }
    }
}