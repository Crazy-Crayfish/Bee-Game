using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WorkerBeeLogic : MonoBehaviour {
    public Camera cam;
    public NavMeshAgent agent;
    private SpriteRenderer renderer;
    private Animator animator;
    private string currentTask;
    private GameObject destinationTile; 
    private GameObject targetEnemy;
    private GameObject hive;
    private GameObject queen;
    public string carriedObject;
    private SfxManager SfxManager; 
    
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

                    if (ScreenManager.Instance.inHive) {
                        SfxManager.Instance.playHivePopSound();
                    }


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

                    if (!ScreenManager.Instance.inHive) {
                        SfxManager.Instance.playHivePopSound();
                    }


                    agent.enabled = (true);
                    agent.destination = (queen.transform.position + new Vector3(0, -2, -queen.transform.position.z));
                    Debug.Log(gameObject.transform.position);                    
                }
                // else
                // {
                //     Debug.Log(Vector2.Distance(hive.transform.position, this.transform.position));
                // }
            }
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
        else if (currentTask == "moveEgg")
        {
            GameObject incubator = destinationTile.GetComponent<HexTile>().structure;// HiveGridManager.Instance.tileList[destinationTile.]

            TaskMoveEgg(incubator);
        }

    }

    public void setDestinationTile(GameObject tile) {
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
        if (destinationTile != null && destinationTile.GetComponent<Tile>() != null && destinationTile.GetComponent<Tile>().value > 0) {
            // Debug.Log("getting nectar from " + destinationTile.value);
            return "collectNectar";
        }
        else if (destinationTile != null 
              && destinationTile.GetComponent<HexTile>() != null 
              && destinationTile.GetComponent<HexTile>().isBuiltOn 
              && destinationTile.GetComponent<HexTile>().tileType == "nursery")
        {
            return "moveEgg";
        }
        else if (agent.hasPath) {
            // Debug.Log("normal move");
            return "moving";
        }
        else return "idle";
    }

    private void TaskMoveEgg(GameObject targetIncubator)
    {
        MovementAnimationUpdate();
        // if we dont have egg
        if (carriedObject != "egg")
        {
            //determine target egg and target egg position
            GameObject targetEgg = null;
            int targetEggPos = 0;
            bool[] eggList = HexGridManager.Instance.getEggList();
            // Debug.Log(eggList.Length);
            for (int i = 0; i < eggList.Length; i++)
            {
                //Debug.Log(eggList[i]);
                if (eggList[i])
                {
                    targetEggPos = i;
                }
            }
            targetEgg = HexGridManager.Instance.getEggAtPos(targetEggPos);

            // pick up egg if close
            // If bee xy is close to egg xy
            if (targetEgg != null)
            {
                if (Vector2.Distance(targetEgg.transform.position, 
                                    this.gameObject.transform.position) < 0.05)
                {
                    // pick up egg
                    // Debug.Log("picking up egg");
                    HexGridManager.Instance.removeEggAtPos(targetEggPos);   
                    carriedObject = "egg";
                }
                else // if far from egg go closer
                {
                    agent.destination = targetEgg.transform.position - new Vector3(0,0,targetEgg.transform.position.z);
                }                
            }

        }
        else // if we DO have egg
        {
            // Debug.Log(targetIncubator.GetComponent<WorkerIncubator>());//.hasEgg == false);
            // drop egg if close to incubator
            
            
            if (((targetIncubator.GetComponent<WorkerIncubator>() != null && targetIncubator.GetComponent<WorkerIncubator>().hasEgg == false)
              || (targetIncubator.GetComponent<SoldierIncubator>() != null && targetIncubator.GetComponent<SoldierIncubator>().hasEgg == false)
              || (targetIncubator.GetComponent<HoneyBeeIncubator>() != null && targetIncubator.GetComponent<HoneyBeeIncubator>().hasEgg == false))
                && Vector2.Distance(targetIncubator.transform.position, 
                                 this.gameObject.transform.position) < 0.05)
            {
                // drop egg
                // Debug.Log("dropping egg");
                carriedObject = "";
                if (targetIncubator.GetComponent<WorkerIncubator>() != null)
                {
                    targetIncubator.GetComponent<WorkerIncubator>().startIncubation();
                }
                else if (targetIncubator.GetComponent<SoldierIncubator>() != null)
                {
                    targetIncubator.GetComponent<SoldierIncubator>().startIncubation();
                }
                else if (targetIncubator.GetComponent<HoneyBeeIncubator>() != null)
                {
                    targetIncubator.GetComponent<HoneyBeeIncubator>().startIncubation();
                }
            }
            else // if far from incubator go closer
            {
                agent.destination = targetIncubator.transform.position - new Vector3(0,0,targetIncubator.transform.position.z);
            }
        }

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
                SfxManager.Instance.playWorkerAttackSound();
                // Debug.Log ("damaging enemy to " + target.GetComponent<EnemyUnit>().health);
            }
        } 
        else 
        {   
            // Debug.Log("Moving to attack " + (targetEnemy.transform.position - new Vector3(0,0,targetEnemy.transform.position.z)));
            agent.destination = targetEnemy.transform.position - new Vector3(0,0,targetEnemy.transform.position.z);
        }  

    }
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
                destinationTile.GetComponent<Tile>().value = destinationTile.GetComponent<Tile>().value - 1;
                ResourceCounter.Instance.changeNectar(1);

                if (!ScreenManager.Instance.inHive) {
                    SfxManager.Instance.playSlurpSound();
                    //weird bug that constantly played when inside hive
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
            SfxManager.Instance.playBuzzSound(); 
        } else if (agent.destination.x > gameObject.transform.position.x && !renderer.flipX) {
            renderer.flipX = true; 
            SfxManager.Instance.playBuzzSound();      
        }
        if (carriedObject == "egg" && !animator.GetBool("holdingEgg")) 
        {
            animator.SetBool("holdingEgg", true);

            if (ScreenManager.Instance.inHive) {
                SfxManager.Instance.playPopSound();
            }
        }
        else if (carriedObject != "egg" && animator.GetBool("holdingEgg")) 
        {
            animator.SetBool("holdingEgg", false);
            if (ScreenManager.Instance.inHive) {
                SfxManager.Instance.playPopSound();
            }
        }      
        if (agent.velocity.magnitude > 2 && !animator.GetBool("isMoving")) 
        {
            animator.SetBool("isMoving", true);
            SfxManager.Instance.playBuzzSound();
            // Debug.Log (gameObject + " is moving");
        }
        else if (agent.velocity.magnitude < 1 && animator.GetBool("isMoving"))
        {
            animator.SetBool("isMoving", false);
            SfxManager.Instance.playBuzzSound();
        }
    }

    private void TaskIdle() 
    {

    }
}