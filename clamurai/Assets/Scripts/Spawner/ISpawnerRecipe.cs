using System;
using UnityEngine;

public interface ISpawnerRecipe
{
    public GameObject objectToSpawn {  get; }
    public void InitializeSpawnableComponent(GameObject objectContainingSpawnable);
}