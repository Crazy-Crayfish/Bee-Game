using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendlyUnitCreator : MonoBehaviour
{
    public static FriendlyUnitCreator Instance { get; set; }
    [SerializeField] private GameObject hive;
    [SerializeField] private GameObject workerBeePreFab;
    [SerializeField] private GameObject soldierBeePreFab;
    [SerializeField] private GameObject honeyBeePreFab;
    private void Awake() 
    {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
        } else {
            Instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
     void Update()
     {
        // Honey Bee Hotkey
         if(Input.GetKeyDown(KeyCode.H) && ResourceCounter.Instance.getHoney() >= 15) // Will require resources at some point
         {
             ResourceCounter.Instance.changeHoney(-15);
             CreateHoney();
         }
         // Worker Bee Hotkey
         if(Input.GetKeyDown(KeyCode.B) && ResourceCounter.Instance.getHoney() >= 15) // Will require resources at some point
         {
             ResourceCounter.Instance.changeHoney(-15);
             CreateWorker();
         }
         // Soldier Bee Hotkey
         if(Input.GetKeyDown(KeyCode.X) && ResourceCounter.Instance.getHoney() >= 15) // Will require resources at some point
         {
             ResourceCounter.Instance.changeHoney(-15);
             CreateSoldier();
         }
    }
    
    public GameObject CreateBee(GameObject beePrefab, Vector3 location)
    {
        var newBee = Instantiate(beePrefab, location, Quaternion.identity);
        // Debug.Log("made worker at " + (newBee.transform.position));
        newBee.GetComponent<Unit>().health = 100;
        // newBee.AddComponent<WorkerBeeLogic>();
        // newBee.AddComponent<Unit>();
        // newBee.AddComponent<UnitMovement>();
        return newBee;
    }

    public GameObject CreateBee(GameObject beePrefab)
    {
        // Spawn at hive
        Vector3 hivePos = (hive.transform.position + new Vector3(0, -1, -hive.transform.position.z));
        
        return CreateBee(beePrefab, hivePos);
    }

    /*

    // If you don't have a specified location, spawn at hive
    public GameObject CreateBee(GameObject beePrefab)
    {
        var newBee = Instantiate(beePrefab, pos, Quaternion.identity);
        
        // Debug.Log("made worker at " + (newBee.transform.position));

        // 100 HP is default
        newBee.GetComponent<Unit>().health = 100;

        // newBee.AddComponent<WorkerBeeLogic>();
        // newBee.AddComponent<Unit>();
        // newBee.AddComponent<UnitMovement>();
        return newBee;
    }

    */

    public void CreateWorker(Vector3 location)
    {
        CreateBee(workerBeePreFab, location);
    }
    public void CreateWorker()
    {
        CreateBee(workerBeePreFab);
    }

    public void CreateSoldier(Vector3 location)
    {
        var newBee = CreateBee(soldierBeePreFab, location);
        // HP is currently set to 1.5x worker bee (150 hardcoded)
        newBee.GetComponent<Unit>().health = 150;
        // Speed in navMesh is set to 2x (7.0 hardcoded currently)
    }
    public void CreateSoldier()
    {
        var newBee = CreateBee(soldierBeePreFab);
        // HP is currently set to 1.5x worker bee (150 hardcoded)
        newBee.GetComponent<Unit>().health = 150;
        // Speed in navMesh is set to 2x (7.0 hardcoded currently)
    }

    public void CreateHoney(Vector3 location)
    {
        var newBee = CreateBee(honeyBeePreFab, location);
        // HP is currently set to 1.0x worker bee (100 hardcoded)
        newBee.GetComponent<Unit>().health = 100;
        // Speed in navMesh is set to 2x (7.0 hardcoded currently)
    }
    public void CreateHoney()
    {
        var newBee = CreateBee(honeyBeePreFab);
        // HP is currently set to 1.0x worker bee (100 hardcoded)
        newBee.GetComponent<Unit>().health = 100;
        // Speed in navMesh is set to 2x (7.0 hardcoded currently)
    }
}
