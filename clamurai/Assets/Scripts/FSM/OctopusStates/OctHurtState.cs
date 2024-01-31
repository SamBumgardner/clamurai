using UnityEngine;

public class OctHurtState : State<Octopus>
{
    public float hurtTimerMax = .5f;
    public float hurtTimer = 0;

    public OctHurtState(Octopus octopus, StateMachine<Octopus> stateMachine) : base(octopus, stateMachine) {}

    public override int HandleInput()
    {
        if (hurtTimer < hurtTimerMax)
        {
            hurtTimer += Time.deltaTime;
        }
        else
        {
            return (int)CrabStates.FALL;
        }

        return base.HandleInput();
    }

    public override void Enter()
    {
        owner.tookDamage = false;

        hurtTimer = 0;
        //owner.animationToPlay = "hurt";
        owner.rb.velocity = owner.knockbackToApply;

        var newXScale = owner.knockbackToApply.x > 0 ? -1 : 1;
        var scale = owner.transform.localScale;
        scale.x = newXScale;
        owner.transform.localScale = scale;

        base.Enter();
    }
}