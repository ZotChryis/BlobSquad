using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    [SerializeField]
    private ArmyManager Army;

    public static Player Get()
    {
        return m_instance;
    }
    private static Player m_instance;

    private Vector2 movement;
    private Vector2 direction;
    private Animator animator;

    public void Start()
    {
        m_instance = this;
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
            animator.SetBool("Walking", true);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            direction.y = -1;
            animator.SetBool("Walking", true);
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

        // do he atacc
        if (Input.GetButtonDown("Jump"))
        {
            Attack();
        }
        if (Input.GetButtonDown("Fire1"))
        {
            Army.AddUnit(ArmyManager.Troop.Knight);
        }
    }

    public override void Attack()
    {
        Attack attack = GameObject.Instantiate(this.AttackPrefab, transform).GetComponent<Attack>();
        attack.FriendlyAttack = this.isFriendly;
        attack.direction = direction;
    }

    public void AddCharisma(int amount, ArmyManager.Troop type)
    {
        gCharisma += amount;
        if (gCharisma >= Charisma)
        {
            // we have enough to recruit!
            gCharisma -= Charisma;
            ArmyManager.Get().AddUnit(type);
            Speak(RecruitMessages[Random.Range(0, RecruitMessages.Length)], 1.5f);
        }
        BarCharisma.SetPercent(gCharisma / Charisma);        
    }

    /*
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
    */
}
