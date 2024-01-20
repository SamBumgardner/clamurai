using UnityEngine;

public class StandState : GroundState
{
	public StandState(Player player, StateMachine stateMachine) : base(player, stateMachine) { }

    public override PlayerStates HandleInput()
    {
        if(Input.GetButtonDown("Jump"))
        {
            //return PlayerStates.JUMP;
        }

        var horizontalAxis = Input.GetAxis("Horizontal");
		if (horizontalAxis != 0)
        {
            return PlayerStates.RUN;
        }

        return base.HandleInput();
    }

    public override void Enter()
    {
        player.GetComponent<SpriteRenderer>().color = Color.red;
        player.rb.velocity = new Vector2(0, 0);
        base.Enter();
    }
}