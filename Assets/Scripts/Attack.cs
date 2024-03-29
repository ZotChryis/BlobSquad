﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{

    public enum AttackType { Chakram, Bow, Sword };

    [SerializeField]
    public bool FriendlyAttack;
    public Vector2 direction;
    public Vector2 facing;
    [SerializeField]
    private float AttackDuration;
    public int Damage;
    [SerializeField]
    protected AttackType atktype;
    [SerializeField]
    private float elapsedTime;

    void Start()
    {
        SoundManager soundMgr = SoundManager.Get();

        // Ensure we have a facing
        if (facing == Vector2.zero)
        {
            facing = Vector2.one;
        }

        elapsedTime = 0;
        if (atktype == AttackType.Chakram)
        {
            float angle = Vector2.SignedAngle(Vector2.up, direction);
            angle = Mathf.Deg2Rad*angle; //+ Mathf.PI/2;
            direction = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
            soundMgr.PlaySoundEffect(SoundManager.SoundEffect.AttackChakram);
        }
        if (atktype == AttackType.Bow)
        {
            this.transform.SetParent(null);
            float angle = Vector2.SignedAngle(Vector2.up, facing);
            this.transform.Rotate(0,0,angle);
            soundMgr.PlaySoundEffect(SoundManager.SoundEffect.AttackArrow);
        }
        if (atktype == AttackType.Sword)
        {   
            soundMgr.PlaySoundEffect(SoundManager.SoundEffect.AttackSword);
            float angle = Vector2.SignedAngle(Vector2.up, facing);
            if (angle < 0) {
                SpriteRenderer temp = gameObject.GetComponent<SpriteRenderer>();
                temp.flipX = true;
            }
            this.transform.Rotate(0,0,angle);
        }
        else
        {
        // Add color for friendly attack/enemy so we can tell for now
        GetComponent<SpriteRenderer>().color = FriendlyAttack
            ? Color.green
            : Color.red;
        }
        this.gameObject.layer = FriendlyAttack
            ? 13
            : 14;
    }

    void Update()
    {
        // Make sure we kill ourselves if the attack duration expires
        elapsedTime += Time.deltaTime;
        if (elapsedTime > AttackDuration)
        {
            GameObject.Destroy(this.gameObject);
            return;
        }

        // Otherwise, update logic specific to our type
        switch(atktype)
        {
            case AttackType.Chakram:
                UpdateChakram();
                break;
            case AttackType.Sword:
                UpdateSword();
                break;
            case AttackType.Bow:
                UpdateBow();
                break;
        }
    }

    public void UpdateChakram()
    {
        this.transform.Rotate(Vector3.forward * 6);
        this.transform.Translate(direction / 12);
    }

    public void UpdateSword()
    {
        // Rotate the swipe a little
        this.transform.Rotate(Vector3.forward * 10);

        // Move the sword swipe forward a bit
        this.transform.Translate(facing / 100);
    }

    public void UpdateBow()
    {
        this.transform.Translate(Vector2.up / 6);
    }

    public void OnTriggerEnter2D(Collider2D collider)
    {
        // Did we collide with an entity?
        Entity entity = collider.gameObject.GetComponent<Entity>();
        if (entity == null) return;

        bool entityAllignment = entity.isFriendly;
        // Is this attack valid?
        if (FriendlyAttack ^ entityAllignment)
        {
            if (atktype == AttackType.Bow)
            {
                GameObject.Destroy(this.gameObject);
                if (entity.GetTroopType() != ArmyManager.Troop.Castle) entity.Wound(Damage);
            }
            else
            {
            entity.Wound(Damage);
            }
        }
    }
}
