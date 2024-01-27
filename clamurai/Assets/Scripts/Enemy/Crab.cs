using System;
using UnityEngine;

public class Crab : BaseEnemy<Crab>, ISpawnable
{
    public event EventHandler GettingDestroyed;

    public int direction = 1;

    Crab()
    {
        states.Add(new CrabWalkState(this, stateMachine));
        states.Add(new CrabFallState(this, stateMachine));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var otherOverlapDetector = collision.GetComponent<OverlapDetector>();
        var damage = otherOverlapDetector.owner.GetCurrentDamageInflicted();
        Debug.Log($"{gameObject.name}: Ouch, I'm going to take {damage} damage");
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