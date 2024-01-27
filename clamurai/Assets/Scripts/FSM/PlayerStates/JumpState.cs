using UnityEngine;

public class JumpState : AirState
{
	public JumpState(Player player, StateMachine<Player> stateMachine) : base(player, stateMachine) { }

    public override int HandleInput()
    {
        if(Input.GetButtonUp("Jump") || owner.rb.velocity.y <= 0)
        {
            return (int)PlayerStates.FALL;
        }

        return base.HandleInput();
    }

    public override void Enter()
    {
        owner.GetComponent<SpriteRenderer>().color = Color.green;
        owner.rb.velocity = new Vector2(owner.rb.velocity.x, owner.rb.velocity.y + Player.JUMP_SPEED);
        base.Enter();
    }
}