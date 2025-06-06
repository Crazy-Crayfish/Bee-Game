using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HoneyBeeLogic : MonoBehaviour {
    public Camera cam;
    public NavMeshAgent agent;
    private SpriteRenderer renderer;
    private Animator animator;
    private string currentTask;
    private GameObject destinationTile; 
    // private GameObject targetEnemy;
    private GameObject hive;
    private GameObject queen;

    void Awake() 
    {
        animator = GetComponent<Animator>();
        cam = Camera.main;
        renderer = GetComponent<SpriteRenderer>();
		agent = GetComponent<NavMeshAgent>();
        currentTask = "idle";
        destinationTile = null;
        this.GetComponent<Unit>().health = 100;
        // targetEnemy = null;
        hive = FriendlyUnitCreator.Instance.hive;
        queen = HexGridManager.Instance.GridQueen;
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

        // if selected (highlight active)                                    
        if (gameObject.transform.GetChild(0).gameObject.activeSelf) // && currentTask == "moving" || currentTask == "idle")
        {
            if (ScreenManager.Instance.inHive)
            {
                // press H to leave hive
                if(Input.GetKeyDown(KeyCode.H))
                {
                    // GameObject hive = FriendlyUnitCreator.Instance.hive;
                    agent.enabled = (false);
                    gameObject.transform.position = (hive.transform.position + new Vector3(0, -1, -hive.transform.position.z));
                    agent.enabled = (true);
                    agent.destination = (hive.transform.position + new Vector3(0, -2, -hive.transform.position.z));
                }
            }
            else
            {
                
                // if close to hive object tp to queen                      ///moved to hive object and 
                if (Vector2.Distance(hive.transform.position, this.transform.position) < 1.5
                 && Vector2.Distance(hive.transform.position, agent.destination) < 1)
                {
                    
                    agent.enabled = (false);
                    gameObject.transform.position = (queen.transform.position + new Vector3(0, -1, -queen.transform.position.z));
                    agent.enabled = (true);
                    agent.destination = (queen.transform.position + new Vector3(0, -2, -queen.transform.position.z));                    
                }
                // else
                // {
                //     Debug.Log(Vector2.Distance(hive.transform.position, this.transform.position));
                // }
            }
        }
/* HONEY BEES DON'T ATTACK

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

*/

        if (currentTask == "moving") 
        {
            TaskMoving();
        }
        else if (currentTask == "collectNectar") 
        {
            TaskCollectNectar(destinationTile);
        } 

    }

    public void setDestinationTile(GameObject tile) {
        destinationTile = tile;
    }
    private string FindTask()
    {

/* HONEY BEES DON'T ATTACK

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

*/

        // else if (targetEnemy != null) // clear targetEnemy if no enemies exist and targetEnemy still has a value
        // {
        //     targetEnemy = null;
        // }
        if (destinationTile != null && destinationTile.GetComponent<Tile>() != null && destinationTile.GetComponent<Tile>().value > 0) {
            // Debug.Log("getting nectar from " + destinationTile.value);
            return "collectNectar";
        }
        else if (agent.hasPath) {
            // Debug.Log("normal move");
            return "moving";
        }
        else return "idle";
    }

/* HONEY BEES DON'T ATTACK

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

*/

    private void TaskCollectNectar(GameObject tile)
    {
        MovementAnimationUpdate();
        // If bee xy is close to tile xy
        if (Vector2.Distance(destinationTile.gameObject.transform.position, 
                             this.gameObject.transform.position) < 0.05)
        {
            // Debug.Log("collecting!!");
            if (destinationTile.GetComponent<Tile>() != null && destinationTile.GetComponent<Tile>().value > 0 && Time.frameCount % 60 == 0) // bad time shortcut
            {
                // FINISH FLOWER
                if (destinationTile.GetComponent<Tile>().value < 3)
                {
                    ResourceCounter.Instance.changeNectar(destinationTile.GetComponent<Tile>().value);
                    destinationTile.GetComponent<Tile>().value = 0;
                }
                
                else
                {
                    // HONEY BEES COLLECT AT 3X SPEED
                    destinationTile.GetComponent<Tile>().value = destinationTile.GetComponent<Tile>().value - 3;
                    ResourceCounter.Instance.changeNectar(3);
                }
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