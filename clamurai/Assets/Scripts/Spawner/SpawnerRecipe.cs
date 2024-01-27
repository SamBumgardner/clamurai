using System;
using UnityEngine;

public class SpawnerRecipe<T> : MonoBehaviour, ISpawnerRecipe where T : ISpawnable
{
    static public Type SPAWNABLE_COMPONENT_TYPE = typeof(T);
    public GameObject objectToSpawn;

    GameObject ISpawnerRecipe.objectToSpawn { get => objectToSpawn; }

    public void InitializeSpawnableComponent(GameObject objectContainingSpawnable)
    {
        var spawnableComponent = objectContainingSpawnable.GetComponent<T>();
        spawnableComponent.initialize(BuildInitParams());
    }

    protected virtual object[] BuildInitParams()
    {
        return new object[] { };
    }
}