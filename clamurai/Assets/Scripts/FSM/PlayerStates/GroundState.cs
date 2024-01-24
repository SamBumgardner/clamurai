public class GroundState : State<Player>
{
	protected GroundState(Player player, StateMachine<Player> stateMachine) : base(player, stateMachine) { }

    public override PlayerStates HandleInput()
    {
        // If player isn't touching the ground, change state to falling.
        if (!owner.IsOnGround())
        {
            return PlayerStates.FALL;
        }

        return base.HandleInput();
    }
}