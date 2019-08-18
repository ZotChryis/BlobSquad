﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ArmyManager : MonoBehaviour
{
    public static ArmyManager Get()
    {
        return m_instance;
    }
    private static ArmyManager m_instance;

    // Start is called before the first frame update
    [SerializeField]
    private int armySize;

    public GameObject player;

    [SerializeField]
    private float spacing;

    [SerializeField]
    private float tightSpacing;

    [SerializeField]
    public GameObject[] ClassToPrefabMap;

    [SerializeField]
    private List<Entity> Castles;

    private float currentSpacing;

    public enum Troop
    {
        Knight = 0,
        Archer = 1,
        // etc

        // This has to be the last one
        Castle,
    }

    public void Start()
    {
        currentSpacing = spacing;
        m_instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        // Change spacing depending on input?!!
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            currentSpacing = tightSpacing;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            currentSpacing = spacing;
        }

        // get current player location
        Vector2 playerPos = player.transform.position;
        if (Input.GetMouseButton(1))
        {
            playerPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        Entity[] army = this.gameObject.GetComponentsInChildren<Entity>();
        // get current army size
        armySize = army.Length;
        
        // tell army which positions to march to
        int entityIndex = 0;
        // sort army

        // then update positions
        foreach (Entity ally in army)
        {
            ally.target = playerPos + Offset(armySize,entityIndex, currentSpacing, 1);
            entityIndex++;
        }
    }

    public Vector2 Offset(int numUnits, int index, float spacingLimit, float baseRadius)
    {
        if (spacingLimit == 0) throw new System.Exception("need a spacing limit set");
        float circumference = 2 * Mathf.PI * baseRadius;
        int unitCap = 0;
        float totalArc = 0f;
        while(totalArc <= circumference)
        {
            unitCap++;
            totalArc += spacingLimit;
        }
        if(numUnits > unitCap)
        {
            if(index < unitCap)
            {
                return Offset(unitCap, index, spacingLimit, baseRadius);
            }
            else
            {
                return Offset(numUnits - unitCap, index - unitCap, spacingLimit, baseRadius + spacingLimit);
            }
        }
        else
        {
            float theta = 2 * Mathf.PI * index / numUnits;
            float x = baseRadius * Mathf.Sin(theta);
            float y = baseRadius * Mathf.Cos(theta);
            return new Vector3(x, y, 0.0f);
        }
            //Mathf.Max(baseRadius,circumfrance / (2 * Mathf.PI));
     }

    // Adds a specific game object entity to the army
    public void AddUnit(Entity entity)
    {
        entity.gameObject.transform.SetParent(this.gameObject.transform);
    }

    // Adds a new game object of the given troop type to the army
    public void AddUnit(Troop type)
    {
        // TODO: Check to see if this mapping exists first. #YOLO
        GameObject entity = GameObject.Instantiate(ClassToPrefabMap[(int)type], this.gameObject.transform);
        entity.transform.SetParent(this.gameObject.transform);
    }

    public void CastleDeath(Entity e)
    {
        Castles.Remove(e);
        Debug.Log("CASTLE DESTROYED!");
        if (Castles.Count == 0)
        {
            Debug.Log("YOU WIN!");
            SceneManager.LoadScene("Victory", LoadSceneMode.Single);
        }
    }
}
