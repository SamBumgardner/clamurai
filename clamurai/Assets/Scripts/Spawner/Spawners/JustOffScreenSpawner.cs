using System;
using System.Linq;
using UnityEngine;

public class JustOffScreenSpawner : BaseSpawner
{
    public Vector2 minDistanceFromCamera = new Vector2(5, 20);
    public Vector2 maxDistanceFromCamera = new Vector2(10, 20);
    public CheckDirection checkDirection = CheckDirection.LEFT;

    

    protected override bool CanSpawn()
    {
        var distanceFromCamera = mainCameraRef.transform.position - transform.position;
        if (checkDirection == CheckDirection.LEFT)
        {
            distanceFromCamera.x *= -1;
        }
        else if (checkDirection == CheckDirection.HORIZONTAL)
        {
            distanceFromCamera.x = Math.Abs(distanceFromCamera.x);
        }

        if (checkDirection == CheckDirection.DOWN)
        {
            distanceFromCamera.y *= -1;
        }
        else if (checkDirection == CheckDirection.VERTICAL)
        {
            distanceFromCamera.y = Math.Abs(distanceFromCamera.y);
        }

        var okHorizontal = (distanceFromCamera.x > minDistanceFromCamera.x && distanceFromCamera.x < maxDistanceFromCamera.x);
        var okVertical = (distanceFromCamera.y > minDistanceFromCamera.y && distanceFromCamera.y < maxDistanceFromCamera.y);
        if (((!HorizontalDirections.Contains(checkDirection) || okHorizontal) // If only checking horizontally...
            && (!VerticalDirections.Contains(checkDirection) || okVertical))) // If only checking vertically...
            
        {
            return base.CanSpawn();
        }
        else return false;
    }

    public enum CheckDirection
    {
        LEFT, 
        RIGHT,
        UP,
        DOWN,
        HORIZONTAL,
        VERTICAL,
    }

    static CheckDirection[] HorizontalDirections =
    {
        CheckDirection.LEFT,
        CheckDirection.RIGHT,
        CheckDirection.HORIZONTAL,
    };

    static CheckDirection[] VerticalDirections =
    {
        CheckDirection.UP,
        CheckDirection.DOWN,
        CheckDirection.VERTICAL,
    };

    private void OnDrawGizmosSelected()
    {
        var outerBoxSize = new Vector3(maxDistanceFromCamera.x * 2, maxDistanceFromCamera.y * 2);
        var outerBoxPosition = transform.position;
        var innerBoxSize = new Vector3(minDistanceFromCamera.x * 2, minDistanceFromCamera.y * 2);
        var innerBoxPosition = transform.position;

        if (HorizontalDirections.Contains(checkDirection))
        {
            innerBoxSize.y = outerBoxSize.y;
            if (checkDirection != CheckDirection.HORIZONTAL)
            {
                innerBoxSize.x /= 2;
                outerBoxSize.x /= 2;
                if (checkDirection == CheckDirection.LEFT)
                {
                    innerBoxPosition.x -= innerBoxSize.x / 2;
                    outerBoxPosition.x -= outerBoxSize.x / 2;
                }
                else
                {
                    innerBoxPosition.x += innerBoxSize.x / 2;
                    outerBoxPosition.x += outerBoxSize.x / 2;
                }
            }
        }
        else
        {
            innerBoxSize.x = outerBoxSize.x;
            if (checkDirection != CheckDirection.VERTICAL)
            {
                innerBoxSize.y /= 2;
                outerBoxSize.y /= 2;
                if (checkDirection == CheckDirection.DOWN)
                {
                    innerBoxPosition.y -= innerBoxSize.y / 2;
                    outerBoxPosition.y -= outerBoxSize.y / 2;
                }
                else
                {
                    innerBoxPosition.y += innerBoxSize.y / 2;
                    outerBoxPosition.y += outerBoxSize.y / 2;
                }
            }
        }

        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(outerBoxPosition, outerBoxSize);
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(innerBoxPosition, innerBoxSize);
    }
}