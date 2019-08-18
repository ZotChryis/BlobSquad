using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] Spawnables;

    [SerializeField]
    private float Rate;

    [SerializeField]
    private int Capacity;

    private float tickTime = 0;
    private List<GameObject> spawned = new List<GameObject>();

    public void Update()
    {
        if (spawned.Count >= Capacity)
        {
            return;
        }

        // only tick spawn rate if we have room
        tickTime += Time.deltaTime;

        if (tickTime >= Rate)
        {
            tickTime = 0;
            GameObject spawn = GameObject.Instantiate(Spawnables[Random.Range(0, Spawnables.Length)]);
            spawn.transform.position = this.transform.position;
            spawned.Add(spawn);

            spawn.GetComponent<Entity>().SetSpawner(this);
        }
    }

    public void OnEntityDeath(Entity e)
    {
        this.spawned.Remove(e.gameObject);
    }
}
