using UnityEngine;

public class OctStrikeState : OctBaseState
{
    public OctStrikeState(Octopus octopus, StateMachine<Octopus> stateMachine) : base(octopus, stateMachine) { }

    public Vector2 strikeDirection;
    public float strikeVelocity = 15;
    
    public float strikeDurationMax = .5f;
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
        owner.animationToPlay = "strike";

        owner.spriteRenderer.color = Color.red;
        owner.transform.localScale = new Vector3(owner.directionX, 1, 1);
        strikeDirection = owner.GetVectorToPlayer().normalized;
        owner.rb.velocity = strikeDirection * strikeVelocity;

        strikeDurationCurrent = strikeDurationMax;
        base.Enter();
    }
}