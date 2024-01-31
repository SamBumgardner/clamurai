using UnityEngine;

public class OctChaseState : OctBaseState
{
    public OctChaseState(Octopus octopus, StateMachine<Octopus> stateMachine) : base(octopus, stateMachine) { }

    const float MOVE_COOLDOWN_MAX = 3;
    public float moveCoolDownReductionRate = .8f;
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
            return (int)OctStates.IDLE;
        }

        // Check cooldown to see if it can do a lunging strike - begins charging if so
        strikeCooldownCurrent -= Time.deltaTime;
        if (strikeCooldownCurrent <= 0 && distanceToPlayer < strikeMinDistance)
        {
            return (int)OctStates.READY_TO_STRIKE;
        }
        return base.HandleInput();
    }

    public override void PhysicsUpdate()
    {
        // bursts of decaying movement, when velocity reaches 0 do another burst. Only track on player with initial movement
        moveCooldownCurrent -= Time.fixedDeltaTime;
        if (moveCooldownCurrent <= 0)
        {
            owner.rb.velocity = owner.GetVectorToPlayer().normalized * chaseVelocityMax;
            moveCooldownMax *= moveCoolDownReductionRate;
            moveCooldownCurrent = moveCooldownMax;
        }
        else
        {
            owner.rb.velocity = owner.rb.velocity * moveDecayRate;
        }
        base.PhysicsUpdate();
    }

    public override void Enter()
    {
        Debug.Log("entered chase state");
        moveCooldownMax = MOVE_COOLDOWN_MAX;
        moveCooldownCurrent = 1;
        strikeCooldownCurrent = strikeCooldownMax;
        base.Enter();
    }
}