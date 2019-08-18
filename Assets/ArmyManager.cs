using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmyManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private int ArmySize;

    public GameObject player;

    [SerializeField]
    private float spacing;
    

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Entity[] army = this.gameObject.GetComponentsInChildren<Entity>();
        // get current army size
        int armySize = army.Length;
        // get current player location
        Vector2 playerPos = player.transform.position;
        // tell army which positions to march to
        int entityIndex = 0;
        foreach (Entity ally in army)
        {
            ally.target = playerPos + Offset(armySize,entityIndex,spacing);
            entityIndex++;
        }
    }

    Vector2 Offset(int numUnits, int index, float spacing)
    {
        float circumfrance = spacing * numUnits;
        float radius = circumfrance / (2 * Mathf.PI);
        float theta = 2 * Mathf.PI * index / numUnits;
        float x = radius * Mathf.Sin(theta);
        float y = radius * Mathf.Cos(theta);
        return new Vector3(x, y, 0.0f);
    }

}
