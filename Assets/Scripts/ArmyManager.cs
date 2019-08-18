using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public GameObject[] ClassToPrefabMap;

    [SerializeField]
    private List<Entity> Castles;

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
        m_instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        Entity[] army = this.gameObject.GetComponentsInChildren<Entity>();
        // get current army size
        armySize = army.Length;
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

    public Vector2 Offset(int numUnits, int index, float spacing)
    {
        float circumfrance = spacing * numUnits;
        float radius = circumfrance / (2 * Mathf.PI);
        float theta = 2 * Mathf.PI * index / numUnits;
        float x = radius * Mathf.Sin(theta);
        float y = radius * Mathf.Cos(theta);
        return new Vector3(x, y, 0.0f);
    }

    // Adds a specific game object entity to the army
    public void AddUnit(Entity entity)
    {
        entity.gameObject.transform.SetParent(this.gameObject.transform);
    }

    // Adds a new game object of the given troop type to the army
    public void AddUnit(Troop type)
    {
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
        }
    }
}
