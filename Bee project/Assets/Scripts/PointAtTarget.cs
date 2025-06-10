using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointAtTarget : MonoBehaviour
{

    public Animator anim;
    public GameObject targetObject;
    public float baseHeight = 1.2f;     // Base vertical offset
    public float amplitude = 0.2f;      // How far it moves up/down
    public float frequency = 4.0f;      // Speed of oscillation
    public GameObject Hive;
    public GameObject BuildButton;
    public GameObject BlueChamber;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        if (anim.GetInteger("Change") == 4)
        {
            targetObject = FriendlyUnitCreator.Instance.firstBee;
        }
        else if (anim.GetInteger("Change") == 9 || anim.GetInteger("Change") == 10)
        {
            targetObject = Hive;
            baseHeight = 2.2f;
        }
        else if (anim.GetInteger("Change") == 12)
        {
            targetObject = BuildButton;
            baseHeight = 1.2f;
        }
        /*
        else if (anim.GetInteger("Change") == 13)
        {
            targetObject = BlueChamber;
            baseHeight = 1.2f;
        }
        */

        if (targetObject != null)
        {
            Vector3 basePosition = targetObject.transform.position;
            float oscillation = Mathf.Sin((Time.time) * frequency) * amplitude;
            basePosition.y += baseHeight + oscillation;

            transform.position = basePosition;
        }

        
    }
}
