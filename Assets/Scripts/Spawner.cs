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

    private float lastSpawn = 0;
    private List<GameObject> spawned = new List<GameObject>();

    public void Update()
    {
        if (spawned.Count >= Capacity)
        {
            return;
        }

        if (Time.time - lastSpawn >= Rate)
        {
            lastSpawn = Time.time;
            GameObject spawn = GameObject.Instantiate(Spawnables[Random.Range(0, Spawnables.Length)]);
            spawn.transform.position = this.transform.position;
            spawned.Add(spawn);
        }
    }
}
