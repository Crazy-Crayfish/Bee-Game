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
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        if (targetObject != null)
        {
            Vector3 basePosition = targetObject.transform.position;
            float oscillation = Mathf.Sin((Time.time) * frequency) * amplitude;
            basePosition.y += baseHeight + oscillation;

            transform.position = basePosition;
        }

        if (anim.GetInteger("Change") == 4)
        {
            targetObject = FriendlyUnitCreator.Instance.firstBee;
        }
    }
}
