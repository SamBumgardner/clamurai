using System;
using Unity.Mathematics;
using UnityEngine;

public class SamuraiChaseState : SamuraiBaseState
{
    public SamuraiChaseState(Samurai samurai, StateMachine<Samurai> stateMachine) : base(samurai, stateMachine) { }

    const float BACKSTEP_CHANCE = .5f;
    const float BACKSTEP_DISTANCE_CHECK = 4f;
    public float moveDecayRate = .85f;

    public float strikeCooldownMax = 3;
    public float strikeCooldownCurrent = 2;
    public float strikeMinDistance = 6;

    public float chaseVelocityMax = 10;

    public override int HandleInput()
    {
        // Check distance to player to see if it should return to idle
        var distanceToPlayer = owner.GetVectorToPlayer().magnitude;
        if (distanceToPlayer > owner.chaseVisionDistance)
        {
            // Reset aggression
            return (int)SamuraiStates.PATROL;
        }

        // Check cooldown to see if it can do a lunging strike - begins charging if so
        strikeCooldownCurrent -= Time.deltaTime;
        if (strikeCooldownCurrent <= 0 && distanceToPlayer < strikeMinDistance)
        {
            strikeCooldownCurrent = strikeCooldownMax * owner.health / owner.maxHealth;
            return (int)SamuraiStates.READY_TO_STRIKE;
        }
        return base.HandleInput();
    }

    public override void PhysicsUpdate()
    {
        if (owner.rb.velocity.magnitude <= .2)
        {
            var vectorToPlayer = owner.GetVectorToPlayer();
            var stepDirection = vectorToPlayer.normalized.x;
            if (vectorToPlayer.magnitude < BACKSTEP_DISTANCE_CHECK && UnityEngine.Random.value <= BACKSTEP_CHANCE)
            {
                stepDirection *= -1;
            }
            owner.rb.velocity = new Vector2(stepDirection * chaseVelocityMax, owner.rb.velocity.y);

            if (vectorToPlayer.x < 0)
            {
                owner.directionX = -1;
            }
            if (vectorToPlayer.x > 0)
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
        owner.directionX = Math.Sign(vectorToPlayer.x);

        owner.animationToPlay = "chase";
        base.Enter();
    }
}