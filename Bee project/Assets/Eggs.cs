using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eggs : MonoBehaviour
{
    private float targetTime = 15.0f;
    // Start is called before the first frame update
    void Start()
    {   
    }

    // Update is called once per frame
    void Update()
    {
        targetTime -= Time.deltaTime;

        if (targetTime <= 0.0f)
        {
            spawnBees();
        }
    }

    void spawnBees()
    {
        FriendlyUnitCreator.Instance.CreateWorker();
        Destroy(gameObject);
    }
}
