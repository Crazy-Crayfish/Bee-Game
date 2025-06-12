using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointAtTarget : MonoBehaviour
{

    public Animator anim;
    public GameObject targetObject;
    private RectTransform UItargetObject;
    public float baseHeight = 1.2f;     // Base vertical offset
    public float amplitude = 0.2f;      // How far it moves up/down
    public float frequency = 4.0f;      // Speed of oscillation
    public GameObject Hive;
    private GameObject target;
    public float x;
    public float y;
    public RectTransform BlueChamber;
    public RectTransform HoneyRefinery;
    public RectTransform GreenChamber;
    public RectTransform WorkerIncubator;
    private bool UItarget;
    private bool horizontal;
    
    // Start is called before the first frame update
    void Start()
    {
        target = new GameObject();
    }

    void Update()
    {
        horizontal = false;
        UItarget = false;
        if (anim.GetInteger("Change") == 4)
        {
            targetObject = FriendlyUnitCreator.Instance.firstBee;
            Debug.Log("FOLLOWING FIRST BEE");
        }
        // Nectar Count
        else if (anim.GetInteger("Change") == 6 || anim.GetInteger("Change") == 7)
        {
            targetObject = target;
            target.transform.position = Camera.main.transform.TransformPoint(new Vector3((float) -1.8, (float) 3.8, 0));
            baseHeight = 1.2f;
        }
        // Hive
        else if (anim.GetInteger("Change") == 9 || anim.GetInteger("Change") == 10)
        {
            targetObject = Hive;
            baseHeight = 50f;
        }
        // Build Button
        else if (anim.GetInteger("Change") == 12)
        {
            targetObject = target;
            target.transform.position = Camera.main.transform.TransformPoint(new Vector3(-7.4f, 4.3f, 0));
            baseHeight = 1.2f;
            horizontal = true;
        }
        // Blue Chamber
        else if (anim.GetInteger("Change") == 13 || anim.GetInteger("Change") == 14 || anim.GetInteger("Change") == 15 || anim.GetInteger("Change") == 16)
        {
            UItargetObject = BlueChamber;
            baseHeight = 1.2f;
            UItarget = true;
        }
        // Honey Refinery
        else if (anim.GetInteger("Change") == 20 || anim.GetInteger("Change") == 21)
        {
            UItargetObject = HoneyRefinery;
            baseHeight = 1.2f;
            UItarget = true;
        }
        else if (anim.GetInteger("Change") == 23)
        {
            targetObject = FriendlyUnitCreator.Instance.firstBee;
            Debug.Log("FOLLOWING FIRST BEE");
        }
        // Hive
        else if (anim.GetInteger("Change") == 24)
        {
            targetObject = Hive;
            baseHeight = 50f;
        }
        // Blue Chamber
        else if (anim.GetInteger("Change") == 26 || anim.GetInteger("Change") == 27)
        {
            UItargetObject = GreenChamber;
            baseHeight = 1.2f;
            UItarget = true;
        }
        // Worker Incubator
        else if (anim.GetInteger("Change") == 28 || anim.GetInteger("Change") == 29 || anim.GetInteger("Change") == 31)
        {
            UItargetObject = WorkerIncubator;
            baseHeight = 1.2f;
            UItarget = true;
        }
        else if (anim.GetInteger("Change") == 30)
        {
            targetObject = FriendlyUnitCreator.Instance.firstBee;
            Debug.Log("FOLLOWING FIRST BEE");
        }

        if (targetObject != null)
        {
            Vector3 basePosition;
            if (UItarget)
            { 
                basePosition = UItargetObject.position;
            }
            else
            {
                basePosition = Camera.main.WorldToScreenPoint(targetObject.transform.position);
            }
            
            float oscillation = Mathf.Sin((Time.time) * frequency) * amplitude;
            if (!horizontal)
            {
                basePosition.y += baseHeight + oscillation;
            }
            else
            {
                basePosition.x += baseHeight + oscillation;
            }

            transform.position = basePosition;
        }

        
    }
}
