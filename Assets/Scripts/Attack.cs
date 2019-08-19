using System.Collections;
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
        // Ensure we have a facing
        if (facing == Vector2.zero)
        {
            facing = Vector2.one;
        }

        elapsedTime = 0;
        if (atktype == AttackType.Chakram || atktype == AttackType.Bow)
        {
            float angle = Vector2.SignedAngle(Vector2.up, direction);
            angle = Mathf.Deg2Rad*angle; //+ Mathf.PI/2;
            direction = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));

            if (atktype == AttackType.Bow)
            {
                this.transform.Rotate(0f, 0f, FacingToDegrees(facing));
            }
        }

        // Add color for friendly attack/enemy so we can tell for now
        GetComponent<SpriteRenderer>().color = FriendlyAttack
            ? Color.green
            : Color.red;
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
        this.transform.Rotate(Vector3.forward / 2);

        // Move the sword swipe forward a bit
        this.transform.Translate(facing / 100);
    }

    public void UpdateBow()
    {
        this.transform.Translate(facing / 15);
    }

    public void OnTriggerEnter2D(Collider2D collider)
    {
        // Did we collide with an entity?
        Entity entity = collider.gameObject.GetComponent<Entity>();
        if (entity == null) return;

        bool entityAllignment = entity.isFriendly;
        // Is this attack valid?
        if (FriendlyAttack && !entityAllignment || !FriendlyAttack && entityAllignment)
        {
            entity.Wound(Damage);
        }
    }

    private float FacingToDegrees(Vector2 facing)
    {
        if (facing.x == 1)
        {
            if (facing.y == 1)
            {
                return 45;
            }
            if (facing.y == 0)
            {
                return 90;
            }
            if (facing.y == -1)
            {
                return 135;
            }
        }

        if (facing.x == 0)
        {
            if (facing.y == 1)
            {
                return 0;
            }
            if (facing.y == 0)
            {
                // ??
                return 0;
            }
            if (facing.y == -1)
            {
                return 180;
            }
        }

        if (facing.x == -1)
        {
            if (facing.y == 1)
            {
                return 315;
            }
            if (facing.y == 0)
            {
                return 270;
            }
            if (facing.y == -1)
            {
                return 225;
            }
        }

        return 0f;
    }
}
