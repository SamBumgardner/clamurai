using UnityEngine;

public class JumpState : AirState
{
	public JumpState(Player player, StateMachine stateMachine) : base(player, stateMachine) { }

    public override PlayerStates HandleInput()
    {
        if(Input.GetButtonUp("Jump") || player.rb.velocity.y <= 0)
        {
            return PlayerStates.FALL;
        }

        return base.HandleInput();
    }

    public override void Enter()
    {
        player.GetComponent<SpriteRenderer>().color = Color.green;
        player.rb.velocity = new Vector2(player.rb.velocity.x, player.rb.velocity.y + Player.JUMP_SPEED);
        base.Enter();
    }
}