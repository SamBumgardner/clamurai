public class WalkState : State<Crab>
{
    protected WalkState(Crab crab, StateMachine<Crab> stateMachine) : base(crab, stateMachine) { }

    public override PlayerStates HandleInput()
    {
        // If crab isn't touching the ground, change state to falling.
        if (false)//!owner.IsOnGround()) -- good evidence that we've got room for shared 'actor' functionality or some such.
        {
            return PlayerStates.FALL;
        }

        return base.HandleInput();
    }
}