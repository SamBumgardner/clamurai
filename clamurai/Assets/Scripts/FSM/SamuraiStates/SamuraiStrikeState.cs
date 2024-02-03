using UnityEngine;

public class SamuraiStrikeState : SamuraiBaseState
{
    public SamuraiStrikeState(Samurai samurai, StateMachine<Samurai> stateMachine) : base(samurai, stateMachine) { }

    public Vector2 strikeDirection;
    public float strikeVelocity = 15;
    
    public float strikeDurationMax = .2f;
    public float strikeDurationCurrent;

    public override int HandleInput()
    {
        strikeDurationCurrent -= Time.deltaTime;
        if (strikeDurationCurrent <= 0)
        {
            return (int)OctStates.CHASE;
        }

        return base.HandleInput();
    }

    public override void Enter()
    {
        owner.animationToPlay = "attack";

        strikeDirection = new Vector2(owner.directionX, 0);
        owner.rb.velocity = strikeDirection * strikeVelocity;

        strikeDurationCurrent = strikeDurationMax;
        base.Enter();
    }
}