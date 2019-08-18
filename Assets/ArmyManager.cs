using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmyManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private int ArmySize;

    public GameObject player;

    

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
            ally.target = playerPos;
        }
    }
}
