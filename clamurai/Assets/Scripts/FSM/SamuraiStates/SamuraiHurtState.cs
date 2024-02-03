using UnityEngine;

public class SamuraiHurtState : State<Samurai>
{
    public float hurtTimerMax = .5f;
    public float hurtTimer = 0;

    public SamuraiHurtState(Samurai samurai, StateMachine<Samurai> stateMachine) : base(samurai, stateMachine) {}

    public override int HandleInput()
    {
        if (hurtTimer < hurtTimerMax)
        {
            hurtTimer += Time.deltaTime;
        }
        else
        {
            return (int)SamuraiStates.CHASE;
        }

        return base.HandleInput();
    }

    public override void Enter()
    {
        owner.tookDamage = false;

        hurtTimer = 0;
        owner.animationToPlay = "hitdamage";
        owner.rb.velocity = owner.knockbackToApply;

        var newXScale = owner.knockbackToApply.x > 0 ? -1 : 1;
        var scale = owner.transform.localScale;
        scale.x = newXScale;
        owner.transform.localScale = scale;

        base.Enter();
    }
}