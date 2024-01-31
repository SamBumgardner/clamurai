using System;
using UnityEngine;

public class Fish : BaseEnemy<Fish>, ISpawnable
{
    public event EventHandler GettingDestroyed;

    public int directionX = 1;
    public int directionY = 1;
    public Vector2 maxVelocity = new Vector2(5, 3);
    public float verticalSpeedChange = .5f;

    Fish()
    {
        states.Add(new FishSwimState(this, stateMachine));
    }

    public void initialize(params object[] args)
    {
        transform.position = (Vector2)args[0];
        directionX = (int)args[1];
    }

    public void OnDestroy()
    {
        GettingDestroyed(this, null);
    }
}