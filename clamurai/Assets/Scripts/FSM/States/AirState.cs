public class AirState : State
{
	protected AirState(Player player, StateMachine stateMachine) : base(player, stateMachine) { }

    public override PlayerStates HandleInput()
    {
        // Velocity check makes sure this doesn't glue player back to ground when starting jump
        // Value doesn't matter too much, just check its positive and somewhat larger than 0.
        if (player.IsOnGround() && player.rb.velocity.y <= 1) 
        {
            return PlayerStates.STAND;
        }

        return base.HandleInput();
    }
}