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

    [SerializeField]
    private Entity Castle;

    [SerializeField]
    private string[] ContactLines;

    [SerializeField]
    private GameObject[] TargetLocations;

    private float tickTime = 0;
    private bool initialPlayerContact = false;
    private List<GameObject> spawned = new List<GameObject>();

    public void Update()
    {
        if (!initialPlayerContact)
        {
            Player player = Player.Get();
            if (player != null)
            {
                float dist = Vector3.Distance(player.transform.position, this.transform.position);
                if (dist <= 15)
                {
                    initialPlayerContact = true;
                }
                else
                {
                    return;
                }
            }

        }

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
            spawn.GetComponent<Entity>().target = TargetLocations[Random.RandomRange(0, TargetLocations.Length)].transform.position;

            Castle.Speak(ContactLines[Random.RandomRange(0, ContactLines.Length)], 1.5f);
        }
    }

    public void OnEntityDeath(Entity e)
    {
        this.spawned.Remove(e.gameObject);
    }
}
