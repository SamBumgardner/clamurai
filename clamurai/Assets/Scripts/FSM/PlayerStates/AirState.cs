public class AirState : State<Player>
{
	protected AirState(Player player, StateMachine<Player> stateMachine) : base(player, stateMachine) { }

    public override int HandleInput()
    {
        // Velocity check makes sure this doesn't glue player back to ground when starting jump
        // Value doesn't matter too much, just check its positive and somewhat larger than 0.
        if (owner.IsOnGround() && owner.rb.velocity.y <= 1) 
        {
            return (int)PlayerStates.STAND;
        }

        return base.HandleInput();
    }
}