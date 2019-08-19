using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackDetection : MonoBehaviour
{
    [SerializeField]
    private Entity parent;
    [SerializeField]
    private bool parentEntityAllignment;
    [SerializeField]
    private bool parentIsAttacking;
    [SerializeField]
    private List<Collider2D> enemies;
    // Start is called before the first frame update
    void Start()
    {
        parent = this.gameObject.GetComponentInParent<Entity>();
        parentEntityAllignment = parent.isFriendly;
        parentIsAttacking = false;
        enemies = new List<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // remove absent enemies
        for (int i = enemies.Count - 1; i >= 0; i--)
        {
            Collider2D enemy = enemies[i];
            if (!enemy)
            {
                enemies.RemoveAt(i);
            }
        }

        // update parent behavior
        if (enemies.Count > 0 && !parentIsAttacking)
        {
            parent.SetAttacking(true);
            parentIsAttacking = true;
        }

        if (enemies.Count == 0 && parentIsAttacking)
        {
            parent.SetAttacking(false);
            parentIsAttacking = false;
        }
    }

    public void OnTriggerEnter2D(Collider2D collider)
    {
        // Did we collide with an entity?
        Entity entity = collider.gameObject.GetComponent<Entity>();
        if (entity == null) return;
        bool foreignEntityAllignment = entity.isFriendly;
        // is this a new thing we should attack?
        if (parentEntityAllignment ^ foreignEntityAllignment && !enemies.Contains(collider))
        {
            enemies.Add(collider);
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        enemies.Remove(other);
    }
}
