using System;

public class JustOffScreenSpawner : BaseSpawner
{
    public float minDistanceFromCamera = 5;
    public float okBufferDistance = 2;
    public CheckDirection checkDirection = CheckDirection.LEFT;


    protected override bool CanSpawn()
    {
        var horizontalDistanceFromCamera = (mainCameraRef.transform.position - transform.position).x;
        if (checkDirection == CheckDirection.LEFT)
        {
            horizontalDistanceFromCamera *= -1;
        }
        else if (checkDirection == CheckDirection.BOTH)
        {
            horizontalDistanceFromCamera = Math.Abs(horizontalDistanceFromCamera);
        }

        if ( horizontalDistanceFromCamera > minDistanceFromCamera && horizontalDistanceFromCamera < minDistanceFromCamera + okBufferDistance)
        {
            return base.CanSpawn();
        }
        else return false;
    }

    public enum CheckDirection
    {
        LEFT, 
        RIGHT,
        BOTH
    }
}