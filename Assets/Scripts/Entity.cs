﻿using System.Collections;
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
    [SerializeField]
    private float DampenDistance;
    public Vector3 target;

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
        RigidBody.freezeRotation = true;
    }

    public void Update()
    {
        // The player class will handle player movement.
        if (this is Player)
        {
            return;
        }

        // Standard movement logic for all entities
        Vector3 position = this.transform.position;
        float dist = 0;
        Vector2 direction = Vector2.zero;
        if (target != null)
        {
            dist = Vector3.Distance(target, position);
            //Debug.Log("Updating Direction");
            direction = target - position;
        }
        //Debug.Log("Direction is:");
        //Debug.Log(direction.ToString());
        float speed = gSpeed;
        if (dist < DampenDistance)
        {
            speed /= 2;
        }
        RigidBody.velocity = direction.normalized * speed;
    }
}
