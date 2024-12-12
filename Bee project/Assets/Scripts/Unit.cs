/*
    This script is used by every Unit. A Unit is any object that moves and can be selected.
    This includes Worker Bees, Soldier Bees, and enemies.

*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Unit : MonoBehaviour
{
    NavMeshAgent agent;
    // Start is called before the first frame update
    void Start()
    {
		agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        agent.updateRotation = false;
		agent.updateUpAxis = false;
        UnitSelectionManager.Instance.allUnitsList.Add(gameObject);
    }
    void OnDestroy()
    {
        UnitSelectionManager.Instance.allUnitsList.Remove(gameObject);
    }
}
