using UnityEngine;

public class LastStandHudUpdater : HUDUpdater
{
    private float timeSurvived = 0f;

    public override string GetObjectiveProgressString()
    {
        timeSurvived += Time.deltaTime;

        return ((int)timeSurvived).ToString();
    }
}
