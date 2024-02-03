using System;
using UnityEngine;

public class Samurai : BaseEnemy<Samurai>, ISpawnable
{
    public event EventHandler GettingDestroyed;

    public GameObject player;

    public float maxHealth = 10;
    public float directionX = 1;

    public float patrolVelocityX = 1;
    public float patrolTime = 2;
    public float waitTime = 2;

    public float patrolVisionDistance = 10;
    public float chaseVisionDistance = 12;

    public bool isDying = false;

    Samurai()
    {
        states.Add(new SamuraiPatrolState(this, stateMachine));
        states.Add(new SamuraiChaseState(this, stateMachine));
        states.Add(new SamuraiReadyToStrikeState(this, stateMachine));
        states.Add(new SamuraiStrikeState(this, stateMachine));
        states.Add(new SamuraiHurtState(this, stateMachine));
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
        //GettingDestroyed(this, null);
    }
}