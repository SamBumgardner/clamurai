public class CrabRecipe : SpawnerRecipe<Crab> {

    public Directions startingDirection = Directions.RIGHT;

    protected override object[] BuildInitParams()
    {
        return new object[] { (int)startingDirection };
    }

    public enum Directions
    {
        LEFT = -1,
        RIGHT = 1,
    }
}