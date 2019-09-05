using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AggroDetector : MonoBehaviour
{
    [SerializeField]
    private Entity parent;
    [SerializeField]
    private bool parentEntityAllignment;
    [SerializeField]
    private bool parentIsAggro;
    [SerializeField]
    private GameObject target;
    // if the entity is scanning for a target
    private bool isScanning;
    // Start is called before the first frame update
    void Start()
    {
        parent = this.gameObject.GetComponentInParent<Entity>();
        parentEntityAllignment = parent.isFriendly;
        parentIsAggro = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!target)
        {
            Deaggro();
        }
    }

    public void OnTriggerEnter2D(Collider2D collider)
    {
        // Did we collide with an entity?
        Entity entity = collider.gameObject.GetComponent<Entity>();
        if (entity == null) return;
        bool foreignEntityAllignment = entity.isFriendly;
        // is this a new thing we should attack?
        if (parentEntityAllignment ^ foreignEntityAllignment && target == null)
        {
            target=collider.gameObject;
            parent.target = target;

        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        Deaggro();
    }
    private void Deaggro()
    {
    }
}
