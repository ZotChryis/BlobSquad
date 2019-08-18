using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{

    public enum AttackType { Chakram, Bow, Sword };

    [SerializeField]
    public bool FriendlyAttack;
    public Vector2 direction;
    [SerializeField]
    private float AttackDuration;
    public int Damage;
    [SerializeField]
    protected AttackType atktype;
    [SerializeField]
    private float elapsedTime;

    void Start()
    {
        elapsedTime = 0;
        if (atktype == AttackType.Chakram)
        {
            float angle = Vector2.SignedAngle(Vector2.up, direction);
            angle = Mathf.Deg2Rad*angle; //+ Mathf.PI/2;
            direction = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
        }
    }

    void Update()
    {
        elapsedTime += Time.deltaTime;
        if (elapsedTime > AttackDuration)
        {
            GameObject.Destroy(this.gameObject);
        }
        else
        {
            if (atktype == AttackType.Chakram)
            {
                this.transform.Rotate(Vector3.forward * 6);
                this.transform.Translate(direction / 12);
            }
        }
    }

    public void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log("Collision");
        // Did we collide with an entity?
        Entity entity = collider.gameObject.GetComponent<Entity>();
        if (entity == null) return;

        Debug.Log("Collision with entity");
        bool entityAllignment = entity.isFriendly;
        // Is this attack valid?
        if (FriendlyAttack && !entityAllignment || !FriendlyAttack && entityAllignment)
        {
            Debug.Log("Gotem!");
            entity.Wound(Damage);
        }
    }
}
