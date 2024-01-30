using UnityEngine;

public class HurtState : State<Player>
{
    public float hurtTimerMax = 1;
    public float hurtTimer = 0;

	public HurtState(Player player, StateMachine<Player> stateMachine) : base(player, stateMachine) { }

    public override int HandleInput()
    {
        if (owner.health <= 0)
        {
            return (int)PlayerStates.DYING;
        }

        if (hurtTimer < hurtTimerMax)
        {
            hurtTimer += Time.deltaTime;
        }
        else
        {
            return (int)PlayerStates.FALL;
        }

        return base.HandleInput();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void Enter()
    {
        // Consume 'took damage' bool so we won't re-enter it again automatically later.
        owner.tookDamage = false;

        hurtTimer = 0;
        owner.animationToPlay = "hurt";

        var newXScale = owner.hurtKnockback.x > 0 ? -1 : 1;
        var scale = owner.transform.localScale;
        scale.x = newXScale;
        owner.transform.localScale = scale;

        if (owner.health > 0)
        {
            owner.rb.velocity = owner.hurtKnockback;
        }

        base.Enter();
    }
}