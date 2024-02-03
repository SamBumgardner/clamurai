using UnityEngine;

public class SamuraiReadyToStrikeState : SamuraiBaseState
{
    public SamuraiReadyToStrikeState(Samurai samurai, StateMachine<Samurai> stateMachine) : base(samurai, stateMachine) { }

    float strikeDelayMax = .5f;
    float strikeDelayCurrent = 0;

    public override int HandleInput()
    {
        // Check internal timer to decide when to move to Strike state. 
        strikeDelayCurrent -= Time.deltaTime;
        if (strikeDelayCurrent <= 0)
        {
            return (int)SamuraiStates.STRIKE;
        }
        return base.HandleInput();
    }

    public override void Enter()
    {
        // maybe set warning color
        owner.animationToPlay = "stop";
        owner.animator.StopPlayback();
        owner.rb.velocity = Vector2.zero;
        strikeDelayCurrent = strikeDelayMax;
        base.Enter();
    }
}