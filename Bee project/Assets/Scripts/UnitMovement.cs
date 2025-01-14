using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnitMovement : MonoBehaviour 
{
    Camera cam;
    NavMeshAgent agent;
    void Awake() 
    {
        cam = Camera.main;
		agent = GetComponent<NavMeshAgent>();
		agent.updateRotation = false;
		agent.updateUpAxis = false;
	}

    // Update is called once per frame
    void Update() 
    {
        // Is RMB down?
        if (Input.GetMouseButtonDown(1)) 
        {
            SetDestinationToMousePosition();
        }
    }
    
    void SetDestinationToMousePosition() 
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);
        
        agent.destination = hit.collider.gameObject.transform.position;  
    }

}