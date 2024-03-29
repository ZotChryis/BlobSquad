﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Entity : MonoBehaviour
{
    /* 
    *   The starting values. These should be defined through
    *   the editor for the given entity.
    */
    [SerializeField]
    private float Health;
    [SerializeField]
    protected float Charisma;
    [SerializeField]
    private float Speed;
    [SerializeField]
    protected Rigidbody2D RigidBody;
    [SerializeField]
    private float DampenDistance;
    public GameObject target;
    public Vector2 destination;
    [SerializeField]
    private float MinDistance;
    [SerializeField]
    private UIBar BarHealth;
    [SerializeField]
    protected UIBar BarCharisma;
    [SerializeField]
    private ArmyManager.Troop TroopType;
    [SerializeField]
    protected GameObject AttackPrefab;
    [SerializeField]
    protected bool isAttacking;
    [SerializeField]
    protected bool isAggro;
    [SerializeField]
    protected GameObject SpeechBubble;
    [SerializeField]
    protected TextMeshPro SpeechText;
    [SerializeField]
    protected string[] RecruitMessages;
    [SerializeField]
    protected float dist;
    [SerializeField]
    protected float attackRange;

    [SerializeField]
    protected float AttackRate;

    public bool isFriendly;

    /* 
    *   The current in-game values of their staring counterparts.
    */
    protected float gHealth;
    protected float gCharisma;
    protected float gSpeed;
    protected Spawner gSpawner;
    protected Vector2 direction;
    protected Vector2 facing;
    protected float lastAttackTime;
    protected bool canAttack;
    protected Animator animator;
    public float lastSpeachTime;

    public void Start()
    {
        animator = this.GetComponent<Animator>();
        canAttack = true;
        isAttacking = false;
        gHealth = Health;
        BarHealth.SetPercent(gHealth / Health);
        gCharisma = 0;
        lastSpeachTime = float.MaxValue;
        if (BarCharisma != null)
        {
            BarCharisma.SetPercent(gCharisma / Charisma);
        }
        gSpeed = Speed;
        // No gravity in our sim
        RigidBody.gravityScale = 0;

        // If we aren't a castle, we must freeze rot
        if (TroopType != ArmyManager.Troop.Castle)
        {
            RigidBody.freezeRotation = true;
        }
    }

    public void Update()
    {
        lastSpeachTime += Time.deltaTime;
        if (gHealth <= 0) Die();
        // Tell everything if they can attack now
        if (!canAttack && Time.time - lastAttackTime >= AttackRate)
        {
            canAttack = true;
        }
        // The player class will handle player movement.
        if (this is Player || this.TroopType == ArmyManager.Troop.Castle)
        {
            return;
        }

        if (isAttacking && canAttack)
        {
            Attack();
        }

        // Standard movement logic for all entities
        Vector2 position = this.transform.position;
        dist = 0;
        direction = Vector2.zero;

        // targeting logic
        if (!target)
        {
        }
        else
        {
            Vector2 targetLocation = target.transform.position;
            float angle = Vector2.SignedAngle(Vector2.up, targetLocation - position);
            //using dist here but it gets reset later
            float offsetDist = attackRange - dist;
            Vector2 offsetDirection = (targetLocation - position);
            Vector2 offset = offsetDirection.normalized * -1 * offsetDist;

            // if(lastSpeachTime > 2) Speak(""
            //     // + "atkRange" + attackRange+"\n"
            //     // + Mathf.Round(angle)+"\n"
            //     + Mathf.Round(dist)+" away\n"
            //     + "dist: " + Mathf.Round(offsetDist)+"\n"
            //     ,1.5f);
            destination = targetLocation + offset;
            // Debug.Log(offset);
        }
        // if you have a target that is alive, set destination to it's position

        // move to destination Logic
        if (destination != null)
        {
            // we only use facing for attack direction for now
            // we want this to be opposite the angle to player
            facing = Vector3.Normalize(destination - position);
            dist = Vector3.Distance(destination, position);
            if (dist > MinDistance)
            {
                direction = destination - position; 
            }
            else
            {
                animator.SetBool("Walking", false);
            }
        }
        //Debug.Log("Direction is:");
        //Debug.Log(direction.ToString());
        float speed = gSpeed;
        if (dist < DampenDistance)
        {
            speed /= 2;
        }
        RigidBody.velocity = direction.normalized * speed;
        if(speed > 0.0001f && Vector2.SignedAngle(direction,Vector2.up) < 0)
        {
            animator.SetInteger("Direction", 1);
            animator.SetBool("Walking", true);
        }
        else if(speed > 0.0001f && Vector2.SignedAngle(direction,Vector2.up) > 0)
        {
            animator.SetInteger("Direction",3);
            animator.SetBool("Walking", true);
        }
    }

    public void SetAttacking(bool atk)
    {
        isAttacking = atk;
    }

    public void SetAggro(bool aggro, GameObject destination)
    {
        if (!aggro) 
        {
            isAggro = false;
        }
        else
        {
            // set destination
            // set isAggro to true
            Debug.Log("fix the aggro set true script");
        }
    }

    public bool IsMelee()
    {
        return TroopType == ArmyManager.Troop.Knight || TroopType == ArmyManager.Troop.Assassin;
    }

    public bool IsRanged()
    {
        return TroopType == ArmyManager.Troop.Archer;
    }

    public void Wound(int damage)
    {
        gHealth -= damage;
        BarHealth.SetPercent(gHealth / Health);
        if (gHealth <= 0) Die();
    }

    public ArmyManager.Troop GetTroopType()
    {
        return this.TroopType;
    }

    public void Attack()
    {
        canAttack = false;
        lastAttackTime = Time.time;
        Attack attack = GameObject.Instantiate(this.AttackPrefab, transform).GetComponent<Attack>();
        attack.FriendlyAttack = this.isFriendly;
        attack.direction = this.isFriendly ? ArmyManager.Get().cursorDirection: direction;
        attack.facing = this.isFriendly ? ArmyManager.Get().cursorDirection : facing;
    }

    public void Die()
    {
        // play death noise
        SoundManager.Get().PlaySoundEffect(SoundManager.SoundEffect.Death);

        // play death poof animation

        // If it's a castle, check to see they win
        if (TroopType == ArmyManager.Troop.Castle)
        {
            ArmyManager.Get().CastleDeath(this);
        }
        // if this is an enemy
        else if (!isFriendly)
        {
            Player.Get().AddCharisma(1, TroopType);
        }

        if (this is Player)
        {
            // TODO: Game over
            SceneManager.LoadScene("Loss", LoadSceneMode.Single);
        }

        // Inform the spawner we came from that we perished!
        if (gSpawner != null)
        {
            this.gSpawner.OnEntityDeath(this);
        }   

        Destroy(this.gameObject);
    }

    public void SetSpawner(Spawner s)
    {
        this.gSpawner = s;
    }

    public void Speak(string text, float duration)
    {
        SpeechBubble.SetActive(true);
        SpeechText.text = text;
        StartCoroutine(StopSpeak(duration));
        lastSpeachTime = 0;
    }

    public IEnumerator StopSpeak(float duration)
    {
        yield return new WaitForSeconds(duration);
        SpeechBubble.SetActive(false);
    }
}
