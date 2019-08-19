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
    public Vector2 target;
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
    protected GameObject SpeechBubble;
    [SerializeField]
    protected TextMeshPro SpeechText;
    [SerializeField]
    protected string[] RecruitMessages;

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

    public void Start()
    {
        canAttack = true;
        isAttacking = false;
        gHealth = Health;
        BarHealth.SetPercent(gHealth / Health);
        gCharisma = 0;
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
        if (gHealth <= 0) Die();
        // Tell everything if they can attack now
        if (!canAttack && Time.time - lastAttackTime >= AttackRate)
        {
            canAttack = true;
        }

        if (true && canAttack)
        {
            Attack();
        }
        // The player class will handle player movement.
        if (this is Player || this.TroopType == ArmyManager.Troop.Castle)
        {
            return;
        }  

        // Standard movement logic for all entities
        Vector2 position = this.transform.position;
        float dist = 0;
        direction = Vector2.zero;
        if (target != null)
        {
            // we only use facing for attack direction for now
            // we want this to be opposite the angle to player
            facing = Vector3.Normalize(target - position);

            dist = Vector3.Distance(target, position);
            if (dist > MinDistance) direction = target - position;
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

    public void setAttacking(bool aggro)
    {
        isAttacking = aggro;
    }

    public bool IsMelee()
    {
        return TroopType == ArmyManager.Troop.Knight;
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

    public void Attack()
    {
        canAttack = false;
        lastAttackTime = Time.time;
        Attack attack = GameObject.Instantiate(this.AttackPrefab, transform).GetComponent<Attack>();
        attack.FriendlyAttack = this.isFriendly;
        attack.direction = direction;
        attack.facing = facing;
    }

    public void Die()
    {
        // play death noise
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
            SceneManager.LoadScene("Victory", LoadSceneMode.Single);
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
    }

    public IEnumerator StopSpeak(float duration)
    {
        yield return new WaitForSeconds(duration);
        SpeechBubble.SetActive(false);
    }
}
