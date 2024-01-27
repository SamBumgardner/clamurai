using UnityEngine;

public class OverlapDetector : MonoBehaviour
{
    public ITriggerOwner owner;
    public TriggerBoxType triggerBoxType;

    public void Start()
    {
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