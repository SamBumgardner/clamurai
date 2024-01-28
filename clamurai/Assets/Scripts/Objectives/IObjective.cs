using System;

public interface IObjective
{
    public event EventHandler<ObjectiveStatusChangeEventArgs> ObjectiveStatusChange;

    // Get a picture that symbolizes what the player has to do. can be displayed on an objectives bar, darkened if incomplete.
    // public getObjectiveIcon 
}