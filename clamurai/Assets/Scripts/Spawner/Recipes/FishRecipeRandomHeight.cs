using UnityEngine;

public class FishRecipeRandomHeight : SpawnerRecipe<Fish> 
{
    public float maxYDistance = 6;
    public Directions xDirection = Directions.RIGHT;

    protected override object[] BuildInitParams()
    {
        return new object[] { 
            new Vector2(transform.position.x, transform.position.y + generateStartOffsetY()),
            (int)xDirection
        };
    }

    private float generateStartOffsetY()
    {
        return Random.value * maxYDistance * 2 - maxYDistance;
    }

    public enum Directions
    {
        LEFT = -1,
        RIGHT = 1,
    }
}