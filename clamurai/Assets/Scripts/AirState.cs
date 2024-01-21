public class AirState : State
{
	protected AirState(Player player, StateMachine stateMachine) : base(player, stateMachine) { }

    public override PlayerStates HandleInput()
    {
        if (player.IsOnGround() && player.rb.velocity.y <= 1)
        {
            return PlayerStates.STAND;
        }

        return base.HandleInput();
    }
}