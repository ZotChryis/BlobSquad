using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{

    public enum AttackType { Demo, Bow, Sword };

    [SerializeField]
    public bool FriendlyAttack;
    [SerializeField]
    private float AttackDuration;
    public int Damage;
    [SerializeField]
    protected AttackType atktype;
    [SerializeField]
    private float elapsedTime;
    // Start is called before the first frame update
    void Start()
    {
        elapsedTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;
        if (elapsedTime > AttackDuration)
        {
            GameObject.Destroy(this.gameObject);
        }
        else
        {
            
            this.transform.Rotate(Vector3.forward*6);
            this.transform.Translate(Vector3.left/12);
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
