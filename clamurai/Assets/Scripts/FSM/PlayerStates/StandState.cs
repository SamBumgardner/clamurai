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
        owner.rb.velocity = new Vector2(0, 0);
        base.PhysicsUpdate();
    }

    public override void Enter()
    {
        owner.animationToPlay = "stand";
        base.Enter();
    }
}