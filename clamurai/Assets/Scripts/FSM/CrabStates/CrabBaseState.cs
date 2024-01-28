public class CrabBaseState : State<Crab>
{
	public CrabBaseState(Crab crab, StateMachine<Crab> stateMachine) : base(crab, stateMachine) { }

    public override int HandleInput()
    {
        if (owner.tookDamage)
        {
            return (int)CrabStates.HURT;
        }

        return base.HandleInput();
    }
}