using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendlyUnitCreator : MonoBehaviour
{
    public static FriendlyUnitCreator Instance { get; set; }
    [SerializeField] private GameObject hive;
    [SerializeField] private GameObject workerBeePreFab;
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
        if(Input.GetKeyDown(KeyCode.B) && ResourceCounter.Instance.getHoney() >= 15) // Will require resources at some point
        {
            ResourceCounter.Instance.changeHoney(-15);
            CreateWorker(hive.transform.position + new Vector3(0, -2, -hive.transform.position.z));
        }
    }
    private void CreateWorker(Vector3 location)
    {
        var newWorker = Instantiate(workerBeePreFab, location, Quaternion.identity);
        // Debug.Log("made worker at " + (newWorker.transform.position));
        newWorker.GetComponent<Unit>().health = 100;
        // newWorker.AddComponent<WorkerBeeLogic>();
        // newWorker.AddComponent<Unit>();
        // newWorker.AddComponent<UnitMovement>();
    }
}
