/*
    This manages what Units exist, and which are selected.
    See the Unit Script for a definition of what is meant by Unit.

*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSelectionManager : MonoBehaviour
{
    // Singleton stuff (maybe should do this for other managers too?)
    public static UnitSelectionManager Instance { get; set; }

    // Lists of all units and active units
    public List<GameObject> allUnitsList = new List<GameObject>();
    public List<GameObject> unitsSelected = new List<GameObject>();

    public LayerMask ground;
    public LayerMask clickable;
    public GameObject groundMarker;

    private Camera cam;

    // More Singleton stuff, destroy extras
    private void Awake() 
    {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
        } else {
            Instance = this;
        }
    }

    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        // If LMB pressed
        if (Input.GetMouseButtonDown(0)) 
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            // Debug.Log (Physics.Raycast(ray, out hit, 100, clickable));
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, clickable)) 
            {
                if (Input.GetKey(KeyCode.LeftControl)) 
                {
                    MultiSelect(hit.collider.gameObject);
                }
                else
                {
                    SelectByClicking(hit.collider.gameObject);
                }
                
                // Debug.Log ("unit clicked");
            } 
            else 
            {
                if (!Input.GetKey(KeyCode.LeftControl)) 
                {
                    DeselectAll();
                }
            }
             
        }



        if (unitsSelected.Count > 0 && Input.GetMouseButtonDown(1)) 
        {
            // Debug.Log ("right clicked");
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            // RaycastHit hit;

            if (Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity, ground)) 
            {
                // Debug.Log ("right clicked on ground");
                RaycastHit2D hit = Physics2D.Raycast(cam.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
                groundMarker.transform.position = hit.collider.gameObject.transform.position;  
                

                groundMarker.SetActive(false);
                groundMarker.SetActive(true);
            } 
        }
    }

    private void SelectByClicking(GameObject unit) 
    {
        DeselectAll();
        unitsSelected.Add(unit);
        TriggerSelectionIndicator(unit, true);
        SetUnitMovement(unit, true);
    }    

    private void MultiSelect(GameObject unit) 
    {
        if (!unitsSelected.Contains(unit))
        {
            unitsSelected.Add(unit);
            TriggerSelectionIndicator(unit, true);
            SetUnitMovement(unit, true);
        }
        else
        {
            SetUnitMovement(unit, false);
            TriggerSelectionIndicator(unit, false);
            unitsSelected.Remove(unit);
        }
    }

    private void DeselectAll() 
    {
        foreach (var unit in unitsSelected) 
        {
            SetUnitMovement(unit, false);
            TriggerSelectionIndicator(unit, false);

        }
        unitsSelected.Clear();
    }

    private void SetUnitMovement(GameObject unit, bool shouldMove)   
    {
        unit.GetComponent<UnitMovement>().enabled = shouldMove;
    }
    
    private void TriggerSelectionIndicator(GameObject unit, bool isVisible)
    {
        unit.transform.GetChild(0).gameObject.SetActive(isVisible);
    }
}
