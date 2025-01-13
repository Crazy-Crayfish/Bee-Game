using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyWaspLogic : MonoBehaviour
{
    Camera cam;
    NavMeshAgent agent;
    GameObject hive;
    void Awake() 
    {
        cam = Camera.main;
		agent = GetComponent<NavMeshAgent>();
		agent.updateRotation = false;
		agent.updateUpAxis = false;
        agent.speed = 1.7f;
        this.GetComponent<EnemyUnit>().health = 150;
        hive = FindObjectOfType<SceneSwapButton>().gameObject.transform.GetChild(0).gameObject;
	}
    // Update is called once per frame
    void Update()
    {
        if (agent.destination.x < gameObject.transform.position.x && GetComponent<SpriteRenderer>().flipX)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        } else if (agent.destination.x > gameObject.transform.position.x && !GetComponent<SpriteRenderer>().flipX) {
            GetComponent<SpriteRenderer>().flipX = true;       
        }

        GameObject currentTarget = getTarget();
        if (currentTarget != null && Vector3.Distance(currentTarget.transform.position, this.transform.position) < 1.5) 
        {   
            Attack(currentTarget);
        }   
        else 
        {   
            moveTowards(currentTarget);
        }   
            

    }       

    private void moveTowards(GameObject target)
    {       


        if (target != null)
        {
            agent.destination = target.transform.position - new Vector3(0,0,target.transform.position.z);
        }
        else
        {
            // Debug.Log ("Going to hive " + hive.transform.position);
            agent.destination = hive.transform.position - new Vector3(0,0,hive.transform.position.z);
            // testing
            // agent.destination = new Vector3(20,20,0);
        }
    }

    private void Attack(GameObject target)
    {
        if (target.GetComponent<Unit>().health > 0 && Time.frameCount % 60 == 0)
        {
            target.GetComponent<Unit>().health = target.GetComponent<Unit>().health - 25;
            // Debug.Log ("damaging to " + target.GetComponent<Unit>().health);
        }
    }

    private GameObject getTarget()
    {
        GameObject target = null;
        foreach (var unit in UnitSelectionManager.Instance.allUnitsList) 
        {
            if (Vector3.Distance(unit.transform.position, this.transform.position) < 8 &&
                (target == null || Vector3.Distance(unit.transform.position, this.transform.position) < 
                Vector3.Distance(target.transform.position, this.transform.position)))
            {
                
                target = unit;
                Debug.Log ("Attacking!! " + target.transform.position);
            }
        }
        
        return target;   
        
    }


}
