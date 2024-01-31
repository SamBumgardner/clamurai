using System;
using UnityEngine;

public class Octopus : BaseEnemy<Octopus>, ISpawnable
{
    public event EventHandler GettingDestroyed;

    public int direction = 1;

    Octopus()
    {
        states.Add(new OctIdleState(this, stateMachine));
        states.Add(new CrabFallState(this, stateMachine));
        states.Add(new CrabHurtState(this, stateMachine));
    }

    public void initialize(params object[] args)
    {
        direction = (int)args[0];
    }

    public void OnDestroy()
    {
        GettingDestroyed(this, null);
    }
}