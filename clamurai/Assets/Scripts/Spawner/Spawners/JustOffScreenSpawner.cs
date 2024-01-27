using System;
using UnityEngine;

public class JustOffScreenSpawner : BaseSpawner
{
    public Vector2 minDistanceFromCamera = new Vector2(5, 20);
    public Vector2 maxDistanceFromCamera = new Vector2(10, 20);
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

        if ( horizontalDistanceFromCamera > minDistanceFromCamera.x && horizontalDistanceFromCamera < maxDistanceFromCamera.x)
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

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, new Vector3(maxDistanceFromCamera.x * 2, maxDistanceFromCamera.y * 2));
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(minDistanceFromCamera.x * 2, minDistanceFromCamera.y * 2));
    }
}