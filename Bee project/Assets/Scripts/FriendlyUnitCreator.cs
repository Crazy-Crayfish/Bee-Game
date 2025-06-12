using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendlyUnitCreator : MonoBehaviour, IDataPersistence
{
    public static FriendlyUnitCreator Instance { get; set; }
    [SerializeField] public GameObject hive;
    [SerializeField] private GameObject workerBeePreFab;
    [SerializeField] private GameObject soldierBeePreFab;
    [SerializeField] private GameObject honeyBeePreFab;
    public tutorialManagerScript tutManager;
    public GameObject firstBee;
    private void Awake() 
    {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
        } else {
            Instance = this;
        }
    }


 public void SaveData(ref GameData data)
    {
        data.allFriendlyUnits = new List<UnitSaveData>();

        foreach (GameObject unit in UnitSelectionManager.Instance.allUnitsList)
        {
            if (unit == null) continue;

            Unit u = unit.GetComponent<Unit>();
            if (u == null) continue;

            string type = "";

            if (unit.name.Contains("Worker")) type = "Worker";
            else if (unit.name.Contains("Soldier")) type = "Soldier";
            else if (unit.name.Contains("Honey")) type = "Honey";
            else continue; // Unknown type

            UnitSaveData unitData = new UnitSaveData {
                x = unit.transform.position.x,
                y = unit.transform.position.y,
                z = unit.transform.position.z,
                health = u.health,
                unitType = type
            };

            data.allFriendlyUnits.Add(unitData);
        }
    }

    public void LoadData(GameData data)
    {
        if (data.allFriendlyUnits == null) return;

        foreach (UnitSaveData unitData in data.allFriendlyUnits)
        {
            GameObject bee = null;
            Vector3 pos = new Vector3(unitData.x, unitData.y, unitData.z);

            switch (unitData.unitType)
            {
                case "Worker":
                    bee = CreateBee(workerBeePreFab, pos);
                    break;
                case "Soldier":
                    bee = CreateBee(soldierBeePreFab, pos);
                    break;
                case "Honey":
                    bee = CreateBee(honeyBeePreFab, pos);
                    break;
            }

            if (bee != null)
            {
                bee.GetComponent<Unit>().health = unitData.health;
            }
        }
    }

    // Start is called before the first frame update
    public void Start()
    {
         if (DataPersistenceManager.instance == null || !DataPersistenceManager.instance.IsNewGame)
    {
        // It's a load operation — skip new unit creation
        return;
    }
    
        // make starter workers  
        firstBee = CreateWorker();
        CreateHoney();
        CreateSoldier();
        
    }

    // Update is called once per frame
     void Update()
     {
        // Honey Bee Hotkey
         if(Input.GetKeyDown(KeyCode.N) && ResourceCounter.Instance.getHoney() >= 15) // Will require resources at some point
         {
             // ResourceCounter.Instance.changeHoney(-15);
             CreateHoney();
         }
         // Worker Bee Hotkey
         if(Input.GetKeyDown(KeyCode.B) && ResourceCounter.Instance.getHoney() >= 15) // Will require resources at some point
         {
             // ResourceCounter.Instance.changeHoney(-15);
             CreateWorker();
         }
         // Soldier Bee Hotkey
         if(Input.GetKeyDown(KeyCode.M) && ResourceCounter.Instance.getHoney() >= 15) // Will require resources at some point
         {
             // ResourceCounter.Instance.changeHoney(-15);
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

        // Set correct name based on prefab
        if (beePrefab == workerBeePreFab) newBee.name = "Worker";
        else if (beePrefab == soldierBeePreFab) newBee.name = "Soldier";
        else if (beePrefab == honeyBeePreFab) newBee.name = "Honey";
        else newBee.name = "Unknown";

        
        return newBee;
    }

    public GameObject CreateBee(GameObject beePrefab)
    {
        // Spawn at hive
        Vector3 hivePos = (hive.transform.position + new Vector3(0, -3, -hive.transform.position.z));
        
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

    public GameObject CreateWorker(Vector3 location)
    {
        GameObject newWorker = CreateBee(workerBeePreFab, location);
        WorkerBeeLogic workerScript = newWorker.GetComponent<WorkerBeeLogic>();
        if (workerScript != null)
        {
            workerScript.TM = tutManager;
        }
        
        return newWorker;
    }
    public GameObject CreateWorker()
    {
        GameObject newWorker = CreateBee(workerBeePreFab);
        WorkerBeeLogic workerScript = newWorker.GetComponent<WorkerBeeLogic>();
        if (workerScript != null)
        {
            workerScript.TM = tutManager;
        }
        return newWorker;
    }

    public GameObject CreateSoldier(Vector3 location)
    {
        GameObject newBee = CreateBee(soldierBeePreFab, location);
        // HP is currently set to 1.5x worker bee (150 hardcoded)
        newBee.GetComponent<Unit>().health = 150;
        // Speed in navMesh is set to 2x (7.0 hardcoded currently)
        return newBee;
    }
    public GameObject CreateSoldier()
    {
        var newBee = CreateBee(soldierBeePreFab);
        // HP is currently set to 1.5x worker bee (150 hardcoded)
        newBee.GetComponent<Unit>().health = 150;
        // Speed in navMesh is set to 2x (7.0 hardcoded currently)
        return newBee;
    }

    public GameObject CreateHoney(Vector3 location)
    {
        var newBee = CreateBee(honeyBeePreFab, location);
        // HP is currently set to 1.0x worker bee (100 hardcoded)
        newBee.GetComponent<Unit>().health = 100;
        // Speed in navMesh is set to 2x (7.0 hardcoded currently)
        return newBee;
    }
    public GameObject CreateHoney()
    {
        var newBee = CreateBee(honeyBeePreFab);
        // HP is currently set to 1.0x worker bee (100 hardcoded)
        newBee.GetComponent<Unit>().health = 100;
        // Speed in navMesh is set to 2x (7.0 hardcoded currently)
        return newBee;
    }


}
[System.Serializable]
public class UnitSaveData
{
    public float x, y, z;
    public int health;
    public string unitType; // "Worker", "Soldier", "Honey"
}