using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenManager : MonoBehaviour
{
    public List<GameObject> hiveUiList = new List<GameObject>();
    public static ScreenManager Instance { get; set; }
    public bool inHive;
    [SerializeField] private GameObject worldCam;
    [SerializeField] private GameObject hiveCam;
    [SerializeField] private GameObject unitBox;
    private Vector3 hiveCamStart;
    private Vector3 worldCamStart;

    // Start is called before the first frame update
    void Awake()
    {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
        } else {
            Instance = this;
        }
        inHive = false;
        worldCam.GetComponent<Camera_Controller>().enabled = true;
        worldCam.GetComponent<Camera>().enabled = true;
        worldCam.GetComponent<AudioListener>().enabled = true;
        hiveCam.GetComponent<Camera_Controller>().enabled = false;
        hiveCam.GetComponent<Camera>().enabled = false;
        hiveCam.GetComponent<AudioListener>().enabled = false;

        hiveCamStart = hiveCam.transform.position;
        worldCamStart = worldCam.transform.position;
    }

    public void newState(){
        if(inHive) 
        {
            world();
            return;
        }
        else 
        {
            hive();
            return;
        }
    }
    public void world() 
    {
        inHive = false;

        foreach (var element in hiveUiList) 
        {
            element.GetComponent<Canvas>().enabled = false;
        }
        // GetComponent<Camera>().transform.position = new Vector3(20, 20, -20);
        // Camera.main.gameObject.GetComponent<Camera_Controller>().enabled = false;
        // Camera.main.gameObject.transform.position = new Vector3(20, 20, -20);
        // Camera.main.gameObject.GetComponent<Camera_Controller>().enabled = true;

        ////Camera.main.gameObject.GetComponent<Camera_Controller>().teleport(new Vector3(25,25,-20));
        
        worldCam.GetComponent<Camera_Controller>().enabled = true;

        worldCam.GetComponent<Camera_Controller>().teleport(worldCamStart);

        worldCam.GetComponent<Camera>().enabled = true;
        worldCam.GetComponent<AudioListener>().enabled = true;

        
        
        hiveCam.GetComponent<Camera_Controller>().enabled = false;
        hiveCam.GetComponent<Camera>().enabled = false;
        hiveCam.GetComponent<AudioListener>().enabled = false;

        UnitSelectionManager.Instance.cam = worldCam.GetComponent<Camera>();
        foreach (var unit in UnitSelectionManager.Instance.allUnitsList) 
        {
            if (unit.GetComponent<UnitMovement>() != null) 
            {
                unit.GetComponent<UnitMovement>().cam = worldCam.GetComponent<Camera>();
            }
            // if (unit.GetComponent<WorkerBeeLogic>() != null) 
            // {
            //     unit.GetComponent<WorkerBeeLogic>().cam = worldCam.GetComponent<Camera>();
            //     unit.GetComponent<UnitMovement>().cam = worldCam.GetComponent<Camera>();
            // }
            // if (unit.GetComponent<SoldierBeeLogic>() != null) 
            // {
            //     unit.GetComponent<SoldierBeeLogic>().cam = worldCam.GetComponent<Camera>();
            //     unit.GetComponent<UnitMovement>().cam = worldCam.GetComponent<Camera>();
            // }
            // if (unit.GetComponent<HoneyBeeLogic>() != null) 
            // {
            //     unit.GetComponent<HoneyBeeLogic>().cam = worldCam.GetComponent<Camera>();
            //     unit.GetComponent<UnitMovement>().cam = worldCam.GetComponent<Camera>();
            // }
        } 
        unitBox.GetComponent<UnitSelectionBox>().cam = worldCam.GetComponent<Camera>();
        
        BackgroundMusicController.Instance.HiveToWorld();

    }
    public void hive()
    {
        inHive = true;
        
        foreach (var element in hiveUiList) 
        {
            element.GetComponent<Canvas>().enabled = true;
        }
        // GetComponent<Camera>().transform.position = new Vector3(4020, 20, -20);
        // Camera.main.gameObject.GetComponent<Camera_Controller>().enabled = false;
        // Camera.main.gameObject.transform.position = new Vector3(4020, 20, -20);
        
        
        // Camera.main..gameObject.GetComponent<Camera>.Size = 132f;
        // Camera.main.gameObject.GetComponent<Camera_Controller>().enabled = true;
        //////Camera.main.gameObject.GetComponent<Camera_Controller>().teleport(new Vector3(4027,7,-20));
        
        hiveCam.GetComponent<Camera_Controller>().enabled = true;

        hiveCam.GetComponent<Camera_Controller>().teleport(hiveCamStart);
        
        hiveCam.GetComponent<Camera>().enabled = true;
        hiveCam.GetComponent<AudioListener>().enabled = true;

        
        
        worldCam.GetComponent<Camera_Controller>().enabled = false;
        worldCam.GetComponent<Camera>().enabled = false;
        worldCam.GetComponent<AudioListener>().enabled = false;

        UnitSelectionManager.Instance.cam = hiveCam.GetComponent<Camera>();
        foreach (var unit in UnitSelectionManager.Instance.allUnitsList) 
        {
            if (unit.GetComponent<UnitMovement>() != null) 
            {
                unit.GetComponent<UnitMovement>().cam = hiveCam.GetComponent<Camera>();
            }
            // if (unit.GetComponent<WorkerBeeLogic>() != null) 
            // {
            //     unit.GetComponent<WorkerBeeLogic>().cam = hiveCam.GetComponent<Camera>();
            //     unit.GetComponent<UnitMovement>().cam = hiveCam.GetComponent<Camera>();
            // }
            // if (unit.GetComponent<SoldierBeeLogic>() != null) 
            // {
            //     unit.GetComponent<SoldierBeeLogic>().cam = worldCam.GetComponent<Camera>();
            //     unit.GetComponent<UnitMovement>().cam = worldCam.GetComponent<Camera>();
            // }
            // if (unit.GetComponent<HoneyBeeLogic>() != null) 
            // {
            //     unit.GetComponent<HoneyBeeLogic>().cam = worldCam.GetComponent<Camera>();
            //     unit.GetComponent<UnitMovement>().cam = worldCam.GetComponent<Camera>();
            // }
        } 
        unitBox.GetComponent<UnitSelectionBox>().cam = hiveCam.GetComponent<Camera>();
        
        BackgroundMusicController.Instance.WorldToHive();
    }
    // Update is called once per frame

}
