using System;
using UnityEngine;

public class Octopus : BaseEnemy<Octopus>, ISpawnable
{
    public event EventHandler GettingDestroyed;

    public GameObject player;

    public float directionX = 1;

    public float patrolVelocityX = 1;
    public float patrolTime = 2;
    public float waitTime = 2;

    public float patrolVisionDistance = 4;
    public float chaseVisionDistance = 7;

    public bool isDying = false;

    Octopus()
    {
        states.Add(new OctIdleState(this, stateMachine));
        states.Add(new OctChaseState(this, stateMachine));
        states.Add(new OctReadyToStrikeState(this, stateMachine));
        states.Add(new OctStrikeState(this, stateMachine));
    }

    public void initialize(params object[] args)
    {
        directionX = (int)args[0];
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public override void Defeat()
    {
        animationToPlay = "dying";
        spriteRenderer.color = Color.white;
        rb.simulated = false;
        foreach (var overlapDetector in overlapDetectors)
        {
            overlapDetector.DisableCollision();
        }
    }

    public void OnDyingAnimationFinished()
    {
        Destroy(gameObject);
    }

    public Vector2 GetVectorToPlayer()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
        return player.transform.position - transform.position;
    }

    public void OnDestroy()
    {
        GettingDestroyed(this, null);
    }
}