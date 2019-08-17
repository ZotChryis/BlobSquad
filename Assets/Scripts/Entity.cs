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
    [SerializeField]
    protected Rigidbody2D RigidBody;

    /* 
    *   The current in-game values of their staring counterparts.
    */
    protected float gHealth;
    protected float gEnergy;
    protected float gSpeed;

    public void Start()
    {
        gHealth = Health;
        gEnergy = Energy;
        gSpeed = Speed;

        // No gravity in our sim
        RigidBody.gravityScale = 0;
    }

    public void Update()
    {
        // The player class will handle player movement.
        if (this is Player)
        {
            return;
        }

        // TODO: Standard movement logic for all entities

    }
}
