using System;
using Unity.Mathematics;
using UnityEngine;

public class BaseSpawner : MonoBehaviour 
{
    public ISpawnerRecipe recipe;
    // if -1, no limit to spawning. Configure at your own risk!
    public int max_spawn_count = 3;
    public float spawn_cooldown = 2;
    public float spawnCooldownVariation = 0;

    public float remaining_cooldown = 0;
    public int live_spawn_count = 0;

    protected GameObject playerRef;
    protected GameObject mainCameraRef;

    // Start is called before the first frame update
    void Start()
    {
        playerRef = GameObject.FindWithTag("Player");
        mainCameraRef = GameObject.FindWithTag("MainCamera");
        recipe = GetComponent<ISpawnerRecipe>();
    }

    // Update is called once per frame
    void Update()
    {
        if (remaining_cooldown > 0)
        {
            remaining_cooldown -= Time.deltaTime;
        }
        else if (CanSpawn())
        {
            Spawn();
        }      
    }

    protected virtual bool CanSpawn()
    {
        return (max_spawn_count == -1 || (max_spawn_count > -1 && live_spawn_count < max_spawn_count))
            && remaining_cooldown <= 0;
    }

    private void Spawn()
    {
        var newlySpawnedObj = Instantiate(recipe.objectToSpawn, transform.position, Quaternion.identity);
        var spawnableComponent = recipe.InitializeSpawnableComponent(newlySpawnedObj);
        live_spawn_count++;
        spawnableComponent.GettingDestroyed += onSpawnedComponentDestroyed;
        remaining_cooldown += spawn_cooldown + (spawnCooldownVariation * 2 * UnityEngine.Random.value - spawnCooldownVariation);
    }

    public void onSpawnedComponentDestroyed(object sender, EventArgs e)
    {
        live_spawn_count--;
        ((ISpawnable)sender).GettingDestroyed -= onSpawnedComponentDestroyed;
    }

    private void OnDrawGizmos()
    {
        if (Application.isPlaying && enabled) {
            if (CanSpawn())
            {
                Gizmos.color = Color.green;
            }
            else if (remaining_cooldown > 0)
            {
                Gizmos.color = Color.yellow;
            }
            else
            {
                Gizmos.color = Color.gray;
            }
        }
        else
        {
            Gizmos.color = Color.gray;
        }
        Gizmos.DrawSphere(transform.position, .25f);
        ChildDrawGizmos();
    }

    protected virtual void ChildDrawGizmos() {}
}