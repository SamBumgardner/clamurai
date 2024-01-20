public class GroundState : State
{
	protected GroundState(Player player, StateMachine stateMachine) : base(player, stateMachine) { }

    public override PlayerStates HandleInput()
    {
        // If player isn't touching the ground, change state to falling.
        return base.HandleInput();
    }
}