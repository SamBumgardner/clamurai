using UnityEngine;

public class OverlapDetector : MonoBehaviour
{
    public ITriggerOwner owner;
    public TriggerBoxType triggerBoxType;

    private Rigidbody2D rb;

    public void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if (gameObject.layer == 7 || gameObject.layer == 9)
        {
            triggerBoxType = TriggerBoxType.HURTBOX;
        } 
        else
        {
            triggerBoxType = TriggerBoxType.HITBOX;
        }

        owner = gameObject.GetComponentInParent<ITriggerOwner>();
    }

    public float GetDamageInflicted()
    {
        if(triggerBoxType == TriggerBoxType.HITBOX)
        {
            return owner.GetCurrentDamageInflicted();
        }
        return 0;
    }

    public void DisableCollision()
    {
        rb.simulated = false;
    }

    public void EnableCollision()
    {
        rb.simulated = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        owner.TriggerOverlapOccurred(triggerBoxType, other);
    }
}

public enum TriggerBoxType
{
    HITBOX = 0,
    HURTBOX = 1,
}