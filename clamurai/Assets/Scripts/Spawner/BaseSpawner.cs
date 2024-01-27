using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseSpawner : MonoBehaviour 
{
    public ISpawnerRecipe recipe;
    // if -1, no limit to spawning. Configure at your own risk!
    public int max_spawn_count = 3;
    public float spawn_cooldown = 2;

    private float remaining_cooldown = 0;
    private List<GameObject> spawned_objects = new List<GameObject>();

    //private GameObject playerRef;

    // Start is called before the first frame update
    void Start()
    {
        //playerRef = GameObject.FindWithTag("Player");
        recipe = GetComponent<CrabRecipe>();
    }

    // Update is called once per frame
    void Update()
    {
        if (remaining_cooldown > 0)
        {
            remaining_cooldown -= Time.deltaTime;
        }
        else if (can_spawn())
        {
            spawn();
        }      
    }

    protected virtual bool can_spawn()
    {
        return (max_spawn_count > -1 && spawned_objects.Count < max_spawn_count) 
            && remaining_cooldown <= 0;
    }

    private void spawn()
    {
        var newlySpawnedObj = Instantiate(recipe.objectToSpawn, transform.position, Quaternion.identity);
        recipe.InitializeSpawnableComponent(newlySpawnedObj);
        spawned_objects.Add(newlySpawnedObj);
        remaining_cooldown += spawn_cooldown;
    }
}
