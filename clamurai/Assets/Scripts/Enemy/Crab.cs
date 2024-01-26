using UnityEngine;

public class Crab : BaseEnemy<Crab>
{
    public int direction = 1;

    Crab()
    {
        states.Add(new CrabWalkState(this, stateMachine));
        states.Add(new CrabFallState(this, stateMachine));
    }

    void OnCollisionStay2D(Collision2D other)
    {
        if (rb.velocity.x == 0)
        {
            direction *= -1;
        }
    }
}