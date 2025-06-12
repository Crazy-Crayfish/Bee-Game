using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutorialManagerScript : MonoBehaviour, IDataPersistence
{

    
    public Animator anim;
    private bool beeSelected;
    private bool flowerPolenated;
    private GameObject firstBee;
    public GameObject arrow;
    public ChamberConstruct BlueChamber;
    public ChamberConstruct GreenChamber;
    public StructureConstruct HoneyRefinery;
    public StructureConstruct WorkerIncubator;
    public BuildButton buildButton;
    public bool beeInHive;
    public bool eggMoving;
    public bool beeLeftHive;
    private float startTime;
    
    public void LoadData(GameData data)
    {
        if (data.tutorialDone)
        {
            anim.SetInteger("Change", 45);
            gameObject.SetActive(false);
        }
    }

    public void SaveData(ref GameData data) 
    {
        if (anim.GetInteger("Change") == 45)
        {
            data.tutorialDone = true;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        beeSelected = false;
        flowerPolenated = false;
        firstBee = FriendlyUnitCreator.Instance.firstBee;
        beeInHive = false;
        beeLeftHive = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Wait for bee selection
        if (anim.GetInteger("Change") == 4)
        {
            Debug.Log("select bee");
            if (UnitSelectionManager.Instance.unitsSelected.Count != 0)
            {
                Debug.Log("SELECTED!");
                changeAnimation();
            }
        }
        // Wait for flower polenation
        else if (anim.GetInteger("Change") == 5)
        {
            if (UnitSelectionManager.Instance.unitsSelected.Count == 0)
            {
                Debug.Log("UNSELECTED!");
                anim.SetInteger("Change", anim.GetInteger("Change") - 1);
            }

            if (ResourceCounter.Instance.getNectar() > 0)
            {
                Debug.Log("POLLENATED!");
                changeAnimation();
            }
        }
        // Wait for nectar collection
        else if (anim.GetInteger("Change") == 7)
        {
            if (ResourceCounter.Instance.getNectar() >= 50)
            {
                Debug.Log("COLLECTED!");
                changeAnimation();
            }
        }
        // Wait for hive enter
        else if (anim.GetInteger("Change") == 10)
        {
            if (ScreenManager.Instance.inHive)
            {
                changeAnimation();
            }
        }
        // Wait for build button opened
        else if (anim.GetInteger("Change") == 12)
        {
            if (buildButton.popUpOpen)
            {
                changeAnimation();
            }
        }
        // Wait for blue chamber selected
        else if (anim.GetInteger("Change") == 15)
        {
            if (BlueChamber.selected)
            {
                changeAnimation();
            }
        }
        // Wait for blue chamber placed
        else if (anim.GetInteger("Change") == 16)
        {
            if (!BlueChamber.selected)
            {
                changeAnimation();
            }
        }
        // Wait for honey refinery selected
        else if (anim.GetInteger("Change") == 20)
        {
            if (HoneyRefinery.selected)
            {
                changeAnimation();
            }
        }
        // Wait for honey refinery placed
        else if (anim.GetInteger("Change") == 21)
        {
            if (!HoneyRefinery.selected)
            {
                changeAnimation();
            }
        }
        // Wait for hive exit
        else if (anim.GetInteger("Change") == 22)
        {
            if (!ScreenManager.Instance.inHive)
            {
                changeAnimation();
            }
        }
        // Wait for bee selection
        else if (anim.GetInteger("Change") == 23)
        {
            Debug.Log("select bee");
            if (UnitSelectionManager.Instance.unitsSelected.Count != 0)
            {
                Debug.Log("SELECTED!");
                changeAnimation();
            }
        }
        // Wait for bee enter hive
        else if (anim.GetInteger("Change") == 24)
        {
            Debug.Log("send bee into hive");
            if (beeInHive)
            {
                Debug.Log("Entered!");
                changeAnimation();
            }
        }
        // Wait for green chamber selected
        else if (anim.GetInteger("Change") == 26)
        {
            if (GreenChamber.selected)
            {
                changeAnimation();
            }
        }
        // Wait for green chamber placed
        else if (anim.GetInteger("Change") == 27)
        {
            if (!GreenChamber.selected)
            {
                changeAnimation();
            }
        }
        // Wait for worker incubator selected
        else if (anim.GetInteger("Change") == 28)
        {
            if (WorkerIncubator.selected)
            {
                changeAnimation();
            }
        }
        // Wait for worker incubator placed
        else if (anim.GetInteger("Change") == 29)
        {
            if (!WorkerIncubator.selected)
            {
                changeAnimation();
            }
        }
        // Wait for bee selection
        else if (anim.GetInteger("Change") == 30)
        {
            Debug.Log("select bee");
            if (UnitSelectionManager.Instance.unitsSelected.Count != 0)
            {
                Debug.Log("SELECTED!");
                changeAnimation();
            }
        }
        // Wait for bee to move egg
        else if (anim.GetInteger("Change") == 31)
        {
            Debug.Log("assign bee to incubator");
            if (eggMoving)
            {
                Debug.Log("assigned!");
                startTime = Time.time;
                changeAnimation();
            }
        }
        // Wait for egg to hatch
        else if (anim.GetInteger("Change") == 33)
        {
            Debug.Log("wait");
            if (Time.time - startTime >= 15f)
            {
                Debug.Log("time past!");
                changeAnimation();
            }
        }
        // Wait for bee selection
        else if (anim.GetInteger("Change") == 35)
        {
            Debug.Log("select bee");
            if (UnitSelectionManager.Instance.unitsSelected.Count != 0)
            {
                Debug.Log("SELECTED!");
                changeAnimation();
            }
        }
        // Wait for bee to exit hive
        else if (anim.GetInteger("Change") == 36)
        {
            Debug.Log("press H");
            if (beeLeftHive)
            {
                Debug.Log("exited hive!");
                changeAnimation();
            }
        }
        else 
        {
            if ((Input.GetKeyDown("space")))
            {
                changeAnimation();
            }
        }
    }

    void changeAnimation()
    {
        anim.SetInteger("Change", anim.GetInteger("Change") + 1);
        Debug.Log(anim.GetInteger("Change"));
    }
}
