/*  A Unit is any object that moves and can be selected.
    This script is used by every friendly Unit. 
    This includes Worker Bees and Soldier Bees.

*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Unit : MonoBehaviour
{
    NavMeshAgent agent;
    public int health;
    
    void Awake()
    {
		agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        agent.updateRotation = false;
		agent.updateUpAxis = false;
        UnitSelectionManager.Instance.allUnitsList.Add(gameObject);
        this.health = -1;
    }

    void Update()
    {
        if (this.health <= 0 && this.health != -1)
        {
            // if (this.gameObject.GetComponent<UnitMovement>() != null)
            // {
            //     this.GetComponent<UnitMovement>().SetActive(false);
            // }
            this.gameObject.SetActive(false);
            Destroy(this);
        }
    }
    void OnDestroy()
    {
        UnitSelectionManager.Instance.allUnitsList.Remove(gameObject);
    }
    
}
