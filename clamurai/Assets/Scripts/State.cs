public abstract class State
{
    protected Player player;
    protected StateMachine stateMachine;

    protected State(Player player, StateMachine stateMachine)
    {
        this.player = player;
        this.stateMachine = stateMachine;
    }

    public virtual void Enter()
    {

    }

    public virtual PlayerStates HandleInput()
    {
        return PlayerStates.NO_CHANGE;
    }

    public virtual void LogicUpdate()
    {

    }

    public virtual void PhysicsUpdate()
    {

    }

    public virtual void Exit()
    {

    }

}