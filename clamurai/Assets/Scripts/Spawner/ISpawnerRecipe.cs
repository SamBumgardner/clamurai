using System;
using UnityEngine;

public interface ISpawnerRecipe
{
    public GameObject objectToSpawn {  get; }
    public ISpawnable InitializeSpawnableComponent(GameObject objectContainingSpawnable);
}