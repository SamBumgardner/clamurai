public class OctBaseState : State<Octopus>
{
    public OctBaseState(Octopus octopus, StateMachine<Octopus> stateMachine) : base(octopus, stateMachine) {}

    public override int HandleInput()
    {
        // check for transition to hurt state
        return base.HandleInput();
    }

    public override void LogicUpdate()
    {
        UpdateFacing();

        base.LogicUpdate();
    }

    private void UpdateFacing()
    {
        var scale = owner.transform.localScale;
        scale.x = owner.directionX;
        owner.transform.localScale = scale;
    }
}