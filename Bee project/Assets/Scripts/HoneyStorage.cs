using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoneyStorage : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        ResourceCounter.Instance.changeMaxHoney(50);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
