using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NectarStorage : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        ResourceCounter.Instance.changeMaxNectar(50);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
