using UnityEngine;

public class OctReadyToStrikeState : State<Octopus>
{
    public OctReadyToStrikeState(Octopus octopus, StateMachine<Octopus> stateMachine) : base(octopus, stateMachine) { }

    float strikeDelayMax = .5f;
    float strikeDelayCurrent = 0;

    public override int HandleInput()
    {
        // Check internal timer to decide when to move to Strike state. 
        // Check for hurt-ed-ness, if so, set variables for "counter attack energy" or some such so it'll retaliate more quickly after recovery.
        strikeDelayCurrent -= Time.deltaTime;
        if (strikeDelayCurrent <= 0)
        {
            return (int)OctStates.STRIKE;
        }
        return base.HandleInput();
    }

    public override void Enter()
    {
        // maybe set warning color
        owner.transform.localScale = owner.transform.localScale * .8f;
        owner.rb.velocity = Vector2.zero;
        strikeDelayCurrent = strikeDelayMax;
        base.Enter();
    }
}