using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaxRefinery : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("HoneytoWax", 4.0f, 4.0f);
    }

    private void HoneytoWax()
    {
        if (ResourceCounter.Instance.getHoney() >= 5)
        {
            ResourceCounter.Instance.changeHoney(-2);
            ResourceCounter.Instance.changeWax(1);
        }
    }
}
