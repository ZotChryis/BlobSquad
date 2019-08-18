using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBar : MonoBehaviour
{
    [SerializeField]
    private GameObject empty;

    [SerializeField]
    private GameObject full;

    private Vector2 size = new Vector2(100, 20);

    private float percent;

    public void Start()
    {
        SetPercent(1f);
    }

    public void SetPercent(float percent)
    {
        this.percent = percent;
        Vector3 newScale = Vector3.one;
        newScale.x = percent;
        full.transform.localScale = newScale;
    }
}
