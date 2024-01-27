using UnityEngine;

public interface ITriggerOwner
{
    public void TriggerOverlapOccurred(TriggerBoxType TriggerType, Collider2D other);

    public float GetCurrentDamageInflicted();
}