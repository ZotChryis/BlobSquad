using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    /* 
    *   The starting values. These should be defined through
    *   the editor for the given entity.
    */
    [SerializeField]
    private float Health;
    [SerializeField]
    private float Energy;
    [SerializeField]
    private float Speed;

    /* 
    *   The current in-game values of their staring counterparts.
    */
    private float gHealth;
    private float gEnergy;
    private float gSpeed;

    public void Start()
    {
        
    }

    public  void Update()
    {
        // The player class will handle player movement.
        if (this is Player)
        {
            return;
        }

        // TODO: Standard movement logic for all entities
        
    }
}
