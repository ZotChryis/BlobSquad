using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField]
    private GameObject AttackSource;
    [SerializeField]
    private bool FriendlyAttack;
    public int Damage;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
