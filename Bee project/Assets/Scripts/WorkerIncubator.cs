using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerIncubator : MonoBehaviour
{
    public bool hasEgg;
    private float incubationTime = 20f;
    private float timeLeft = 20f;
    // Start is called before the first frame update
    void Awake()
    {
        hasEgg = false;
    }
    
    // Update is called once per frame
    void Update()
    {
        if (hasEgg)
        {
            timeLeft -= Time.deltaTime;

            if (timeLeft <= 0.0f)
            {
                spawnBee();
                hasEgg = false;
            }
        }

    }

    private void spawnBee()
    {
        Vector3 pos = gameObject.transform.position + new Vector3(0, 0, -gameObject.transform.position.z);
        FriendlyUnitCreator.Instance.CreateWorker(pos);
    }

    public void startIncubation()
    {
        timeLeft = incubationTime;
        hasEgg = true;
    }
}
