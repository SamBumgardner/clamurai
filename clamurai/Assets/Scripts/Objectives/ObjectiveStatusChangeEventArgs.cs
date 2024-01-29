using System;

public class ObjectiveStatusChangeEventArgs : EventArgs
{
    public int ObjectiveID { get; set; }
    public bool IsComplete { get; set; }
    public float Progress { get; set; }
}