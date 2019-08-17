using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spacer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    Vector3 Offset(int numUnits,int index, float spacing)
    {
        float circumfrance = spacing * numUnits;
        float radius = circumfrance / (2 * Mathf.PI);
        float theta = 2 * Mathf.PI * index / numUnits;
        float x = radius * Mathf.Sin(theta);
        float y = radius * Mathf.Cos(theta);
        return new Vector3(x, y, 0.0f);
    }

}
