using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoneyRefinery : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("NectarToHoney", 2.0f, 2.0f);
    }

    private void NectarToHoney()
    {
        if (ResourceCounter.Instance.getNectar() >= 5)
        {
            ResourceCounter.Instance.changeNectar(-5);
            ResourceCounter.Instance.changeHoney(1);
        }
    }
}
