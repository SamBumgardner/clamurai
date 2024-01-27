using System;

public interface ISpawnable
{
    public event EventHandler GettingDestroyed;
    public void initialize(params object[] args);
}