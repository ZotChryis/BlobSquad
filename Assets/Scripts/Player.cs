using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    [SerializeField]
    private ArmyManager Army;

    private Vector2 movement;
    private Vector2 direction;
    private Animator animator;

    public void Start()
    {
        base.Start();
        movement = Vector2.zero;
        direction = Vector2.zero;
        animator = this.GetComponent<Animator>();
    }

    public void Update()
    {
        base.Update();

        // Player movement is hard coded to the update loop of player
        // Up/Down is locked together. One or the other only
        direction = Vector2.zero;
        animator.SetBool("Walking", false);
        if (Input.GetKey(KeyCode.W))
        {
            direction.y = 1;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            direction.y = -1;
        }

        // Similar locking between to Left/Right
        if (Input.GetKey(KeyCode.A))
        {
            direction.x = -1;
            animator.SetInteger("Direction", 1);
            animator.SetBool("Walking", true);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            direction.x = 1;
            animator.SetInteger("Direction", 3);
            animator.SetBool("Walking", true);
        }

        RigidBody.velocity = direction * gSpeed;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        // Did we collide with an entity?
        Entity entity = collision.collider.gameObject.GetComponent<Entity>();
        if (entity != null)
        {
            // Add them to our army!
            Army.AddUnit(entity);
        }
    }
}
