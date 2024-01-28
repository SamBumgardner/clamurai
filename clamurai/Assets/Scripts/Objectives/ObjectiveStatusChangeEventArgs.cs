using System;

public class ObjectiveStatusChangeEventArgs : EventArgs
{
    public bool IsComplete { get; set; }
    public float Progress { get; set; }
}