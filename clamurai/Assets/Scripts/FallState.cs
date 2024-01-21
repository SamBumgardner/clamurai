using UnityEngine;

public class FallState : AirState
{
	public FallState(Player player, StateMachine stateMachine) : base(player, stateMachine) { }

    public override void Enter()
    {
        player.GetComponent<SpriteRenderer>().color = Color.yellow;
        if (player.rb.velocity.y > 3)
        {
            player.rb.velocity = new Vector2(player.rb.velocity.x, 3);
        }
        base.Enter();
    }
}