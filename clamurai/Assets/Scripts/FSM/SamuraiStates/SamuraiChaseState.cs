using UnityEngine;

public class SamuraiChaseState : SamuraiBaseState
{
    public SamuraiChaseState(Samurai samurai, StateMachine<Samurai> stateMachine) : base(samurai, stateMachine) { }

    const float MOVE_COOLDOWN_MAX = 3;
    public float moveCoolDownReductionRate = .7f;
    public float moveCooldownMax = 3;
    public float moveCooldownCurrent = 1;
    public float moveDecayRate = .95f;

    public float strikeCooldownMax = 5;
    public float strikeCooldownCurrent = 2;
    public float strikeMinDistance = 4;

    public float chaseVelocityMax = 8;

    public override int HandleInput()
    {
        // Check distance to player to see if it should return to idle
        var distanceToPlayer = owner.GetVectorToPlayer().magnitude;
        if (distanceToPlayer > owner.chaseVisionDistance)
        {
            // Reset aggression
            return (int)SamuraiStates.IDLE;
        }

        // Check cooldown to see if it can do a lunging strike - begins charging if so
        strikeCooldownCurrent -= Time.deltaTime;
        if (strikeCooldownCurrent <= 0 && distanceToPlayer < strikeMinDistance)
        {
            return (int)SamuraiStates.READY_TO_STRIKE;
        }
        return base.HandleInput();
    }

    public override void PhysicsUpdate()
    {
        moveCooldownCurrent -= Time.fixedDeltaTime;
        if (moveCooldownCurrent <= 0)
        {
            owner.rb.velocity = owner.GetVectorToPlayer().normalized * chaseVelocityMax;
            moveCooldownMax *= moveCoolDownReductionRate;
            moveCooldownCurrent = moveCooldownMax;

            if (owner.rb.velocity.x < 0)
            {
                owner.directionX = -1;
            }
            if (owner.rb.velocity.x > 0)
            {
                owner.directionX = 1;
            }
        }
        else
        {
            owner.rb.velocity = owner.rb.velocity * moveDecayRate;
        }
        base.PhysicsUpdate();
    }

    public override void Enter()
    {
        // turn to face player
        var vectorToPlayer = owner.GetVectorToPlayer();
        if (vectorToPlayer.x > 0)
        {
            owner.directionX = 1;
        }
        else
        {
            owner.directionX = -1;
        }
        owner.rb.velocity = Vector2.zero;

        owner.animationToPlay = "chase";
        moveCooldownMax = MOVE_COOLDOWN_MAX;
        moveCooldownCurrent = 1;
        strikeCooldownCurrent = strikeCooldownMax;
        base.Enter();
    }
}