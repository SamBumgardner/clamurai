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
        owner.rb.velocity = new Vector2(Player.RUN_SPEED * Input.GetAxis("Horizontal"), 0);
        base.PhysicsUpdate();
    }

    public override void Enter()
    {
        owner.GetComponent<SpriteRenderer>().color = Color.blue;
        base.Enter();
    }
}