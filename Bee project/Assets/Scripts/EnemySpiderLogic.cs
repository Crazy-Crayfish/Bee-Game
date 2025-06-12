using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpiderLogic : MonoBehaviour
{
    Camera cam;
    NavMeshAgent agent;
    GameObject hive;
    private SfxManager SfxManager;

    
    void Awake() 
    {
        cam = Camera.main;
		agent = GetComponent<NavMeshAgent>();
		agent.updateRotation = false;
		agent.updateUpAxis = false;
        agent.speed = 2.4f;
        // SPIDERS HAVE 2/3X HEALTH AND 1.5X SPEED
        this.GetComponent<EnemyUnit>().health = 100;
        hive = GameObject.Find("Hive");
        // hive = FindObjectOfType<SceneSwapButton>().gameObject;
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
            // GetComponent<Animator>().SetBool("antAttacking", true);
        }   
        // else if (attacking == true)
        // {   
        //     moveTowards(currentTarget);
        //     GetComponent<Animator>().setBool("attacking", false)
        // }   
        else
        {   
            moveTowards(currentTarget);
            // GetComponent<Animator>().SetBool("antAttacking", false);
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
            
            SfxManager.Instance.playSpiderAttackSound();
            
            // Debug.Log ("damaging to " + target.GetComponent<Unit>().health);
        }
    }

    private GameObject getTarget()
    {
        GameObject target = null;
        foreach (var unit in UnitSelectionManager.Instance.allUnitsList) 
        {
            // SPIDERS ONLY ATTACK HONEY BEES
            if (unit.GetComponent<HoneyBeeLogic> () && Vector3.Distance(unit.transform.position, this.transform.position) < 16 &&
                (target == null || Vector3.Distance(unit.transform.position, this.transform.position) < 
                Vector3.Distance(target.transform.position, this.transform.position)))
            {
                
                target = unit;
                // Debug.Log ("Attacking!! " + target.transform.position);
            }
        }
        
        return target;   
        
    }


}
