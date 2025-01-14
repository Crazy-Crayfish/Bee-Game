using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyUnit : MonoBehaviour
{
    NavMeshAgent agent;
    public int health;

    void Awake()
    {
		agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        agent.updateRotation = false;
		agent.updateUpAxis = false;
        UnitSelectionManager.Instance.allEnemiesList.Add(gameObject);
        this.health = -1;
    }

    void Update()
    {
        if (this.health <= 0 && this.health != -1)
        {
            
            this.gameObject.SetActive(false);
            Destroy(this);
        }
    }
    void OnDestroy()
    {
        UnitSelectionManager.Instance.allEnemiesList.Remove(this.gameObject);
    }
    
}
