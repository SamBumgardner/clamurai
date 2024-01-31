using UnityEngine;

public class OctStrikeState : State<Octopus>
{
    public OctStrikeState(Octopus octopus, StateMachine<Octopus> stateMachine) : base(octopus, stateMachine) { }

    public Vector2 strikeDirection;
    public float strikeVelocity = 10;
    
    public float strikeDurationMax = 1;
    public float strikeDurationCurrent;

    public override int HandleInput()
    {
        // if strike is ending, return to chase state
        strikeDurationCurrent -= Time.deltaTime;
        if (strikeDurationCurrent <= 0)
        {
            return (int)OctStates.CHASE;
        }

        return base.HandleInput();
    }

    public override void Enter()
    {
        // Set lunge velocity, change color to show invulnerability
        owner.transform.localScale = new Vector3(owner.directionX, 1, 1);
        strikeDirection = owner.GetVectorToPlayer().normalized;
        owner.rb.velocity = strikeDirection * strikeVelocity;

        strikeDurationCurrent = strikeDurationMax;
        base.Enter();
    }

    // Implement method to receive callback when strike animation's done?
}