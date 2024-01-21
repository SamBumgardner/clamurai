using UnityEngine;

public class RunState : GroundState
{
	public RunState(Player player, StateMachine stateMachine) : base(player, stateMachine) { }

    public override PlayerStates HandleInput()
    {
        if(Input.GetButtonDown("Jump"))
        {
            return PlayerStates.JUMP;
        }

        var horizontalAxis = Input.GetAxis("Horizontal");
		if (horizontalAxis == 0)
        {
            return PlayerStates.STAND;
        }
        else // attempt facing switch
        {
            var newXScale = horizontalAxis > 0 ? 1 : -1;
            var scale = player.transform.localScale;
            scale.x = newXScale;
            player.transform.localScale = scale;
        }

        return base.HandleInput();
    }

    public override void PhysicsUpdate()
    {
        player.rb.velocity = new Vector2(Player.RUN_SPEED * Input.GetAxis("Horizontal"), 0);
        base.PhysicsUpdate();
    }

    public override void Enter()
    {
        player.GetComponent<SpriteRenderer>().color = Color.blue;
        base.Enter();
    }
}