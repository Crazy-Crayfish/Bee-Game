using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnitMovement : MonoBehaviour {
    Camera cam;
    NavMeshAgent agent;
    void Start() {
        cam = Camera.main;
		agent = GetComponent<NavMeshAgent>();
		agent.updateRotation = false;
		agent.updateUpAxis = false;
	}

    // Update is called once per frame
    void Update() {
        // Is RMB down?
        if (Input.GetMouseButtonDown(1)) {
            SetDestinationToMousePosition();
        }
    }
    
    void SetDestinationToMousePosition() {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        // if (Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity, ground)) 
        // {
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);
        // RaycastHit2D hit = Physics2D.Raycast(cam.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        agent.destination = hit.collider.gameObject.transform.position;  
        // }
        // var target = cam.ScreenToWorldPoint(Input.mousePosition);
        // target.z = 0;
        // agent.destination = target;

        // Vector2 worldPoint = Camera.main.ScreenToWorldPoint( Input.mousePosition );
        // RaycastHit2D hit = Physics2D.Raycast( worldPoint, Vector2.zero );
        // agent.SetDestination(hit.point);
    }
}