using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WorkerBeeLogic : MonoBehaviour {
    Camera cam;
    public NavMeshAgent agent;
    private SpriteRenderer renderer;
    private Animator animator;
    private string currentTask;
    private Tile destinationTile;
    private GameObject targetEnemy;
    void Awake() 
    {
        animator = GetComponent<Animator>();
        cam = Camera.main;
        renderer = GetComponent<SpriteRenderer>();
		agent = GetComponent<NavMeshAgent>();
        currentTask = "idle";
        destinationTile = null;
        this.GetComponent<Unit>().health = 100;
        targetEnemy = null;
	}

    // Update is called once per frame
    void Update() 
    {   
        // Determine task
        if (currentTask != FindTask()) 
        {
            currentTask = FindTask();
            // Debug.Log("currentTask is " + currentTask);
        }

        // Execute task
        if (currentTask == "attacking")
        {
            // Debug.Log("Attacking an enemy");
            TaskAttacking(targetEnemy);
            GetComponent<Animator>().SetBool("beeAttacking", true);
        }
        else 
        {
        GetComponent<Animator>().SetBool("beeAttacking", false);
        }
        if (currentTask == "moving") 
        {
            TaskMoving();
        }
        else if (currentTask == "collectNectar") 
        {
            TaskCollectNectar(destinationTile);
        } 

    }
    public void setDestinationTile(Tile tile) {
        destinationTile = tile;
    }
    private string FindTask()
    {
        if (UnitSelectionManager.Instance.allEnemiesList.Count > 0) // if any enemies exist, 
        {

            foreach (var enemy in UnitSelectionManager.Instance.allEnemiesList) // for each enemy,
            {
                // check if the enemy is close enough to be in range,
                if (Vector3.Distance(enemy.transform.position, this.transform.position) < 8 &&
                   (targetEnemy == null || Vector3.Distance(enemy.transform.position, this.transform.position) <= 
                    Vector3.Distance(targetEnemy.transform.position, this.transform.position))) 
                {
                    // and make the closest in-range enemy the target enemy  
                    targetEnemy = enemy;
                    // Debug.Log("Attacking an enemy");
                    return "attacking";
                }
            }
        }
        // else if (targetEnemy != null) // clear targetEnemy if no enemies exist and targetEnemy still has a value
        // {
        //     targetEnemy = null;
        // }
        if (destinationTile != null && destinationTile.value > 0) {
            // Debug.Log("getting nectar from " + destinationTile.value);
            return "collectNectar";
        }
        else if (agent.hasPath) {
            // Debug.Log("normal move");
            return "moving";
        }
        else return "idle";
    }
    private void TaskAttacking(GameObject target)
    {
        MovementAnimationUpdate();
        if (Vector3.Distance(target.transform.position, this.transform.position) < 1.5)
        {
            // attackTimer = attackTimer + 1f * Time.deltaTime;
            // attackTimer = Mathf.Clamp(attackTimer, 0f, attackCooldown);

            // deal damage
            if (target.GetComponent<EnemyUnit>().health > 0 && Time.frameCount % 60 == 0) // bad time shortcut
            {
                target.GetComponent<EnemyUnit>().health = target.GetComponent<EnemyUnit>().health - 20;
                // Debug.Log ("damaging enemy to " + target.GetComponent<EnemyUnit>().health);
            }
        } 
        else 
        {   
            // Debug.Log("Moving to attack " + (targetEnemy.transform.position - new Vector3(0,0,targetEnemy.transform.position.z)));
            agent.destination = targetEnemy.transform.position - new Vector3(0,0,targetEnemy.transform.position.z);
        }  

    }
    private void TaskCollectNectar(Tile tile)
    {
        MovementAnimationUpdate();
        // If bee xy is close to tile xy
        if (Vector2.Distance(destinationTile.gameObject.transform.position, 
                             this.gameObject.transform.position) < 0.05)
        {
            // Debug.Log("collecting!!");
            if (destinationTile.value > 0 && Time.frameCount % 60 == 0) // bad time shortcut
            {
                destinationTile.value = destinationTile.value - 1;
                ResourceCounter.Instance.changeNectar(1);
            }
        }
        // maybe make it auto seek out more flowers?
        
    }
    private void TaskMoving() 
    {
        MovementAnimationUpdate();
    }
    private void MovementAnimationUpdate() 
    {
        if (agent.destination.x < gameObject.transform.position.x && renderer.flipX)
        {
            renderer.flipX = false;
        } else if (agent.destination.x > gameObject.transform.position.x && !renderer.flipX) {
            renderer.flipX = true;       
        }

        if (agent.velocity.magnitude > 2 && !animator.GetBool("isMoving")) 
        {
            animator.SetBool("isMoving", true);
           // Debug.Log ("moving");
        }
        else if (agent.velocity.magnitude < 1 && animator.GetBool("isMoving"))
        {
            animator.SetBool("isMoving", false);
        }
    }

    private void TaskIdle() 
    {

    }
}