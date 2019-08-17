using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityManager : MonoBehaviour
{
    public GameObject target;
    public float moveSpeed;
    public float dampenDist;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 position = this.transform.position;
        float dist;
        if (target != null)
        {
            dist = Vector3.Distance(target.transform.position,position);
            Debug.Log(dist);
            // move 
        }

        // move at move speed towards target
        // check to see if it is time to attack
        // attack
    }
}
