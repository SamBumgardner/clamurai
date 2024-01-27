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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var otherOverlapDetector = collision.GetComponent<OverlapDetector>();
        var damage = otherOverlapDetector.owner.GetCurrentDamageInflicted();
        Debug.Log($"{gameObject.name}: Ouch, I'm going to take {damage} damage");
    }
}