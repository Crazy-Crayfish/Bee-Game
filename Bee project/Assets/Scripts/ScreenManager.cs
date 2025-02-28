using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenManager : MonoBehaviour
{
    public List<GameObject> hiveUiList = new List<GameObject>();
    public static ScreenManager Instance { get; set; }
    public bool inHive;

    // Start is called before the first frame update
    void Awake()
    {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
        } else {
            Instance = this;
        }
        inHive = false;
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
        Camera.main.gameObject.GetComponent<Camera_Controller>().teleport(new Vector3(25,25,-20));
        // Camera.main.gameObject.GetComponent<Camera_Controller>().enabled = true;

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
        
        Camera.main.gameObject.GetComponent<Camera_Controller>().teleport(new Vector3(4027,7,-20));
        // Camera.main..gameObject.GetComponent<Camera>.Size = 132f;
        // Camera.main.gameObject.GetComponent<Camera_Controller>().enabled = true;

        BackgroundMusicController.Instance.WorldToHive();
    }
    // Update is called once per frame

}
