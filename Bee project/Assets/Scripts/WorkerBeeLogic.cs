using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WorkerBeeLogic : MonoBehaviour {
    Camera cam;
    NavMeshAgent agent;
    // [SerializeField] private Sprite idleSprite, movingSprite; 
    private SpriteRenderer renderer;
    private Animator animator;

    void Start() 
    {
        animator = GetComponent<Animator>();
        cam = Camera.main;
        renderer = GetComponent<SpriteRenderer>();
		agent = GetComponent<NavMeshAgent>();
        // renderer.sprite = idleSprite;
        
	}

    // Update is called once per frame
    void Update() 
    {   
        
        if (agent.destination.x < gameObject.transform.position.x && renderer.flipX)
        {
            renderer.flipX = false;
        } else if (agent.destination.x > gameObject.transform.position.x && !renderer.flipX) {
            renderer.flipX = true;       
        }


        if (agent.velocity.magnitude > 2 && !animator.GetBool("isMoving")) 
        {
            animator.SetBool("isMoving", true);
           // Debug.Log ("moving");
        }
        else if (agent.velocity.magnitude < 1 && animator.GetBool("isMoving"))
        {
            animator.SetBool("isMoving", false);
        }
        // if (agent.velocity.magnitude > 2 && renderer.sprite != movingSprite) {
        //     renderer.sprite = movingSprite;
        // } else if (agent.velocity.magnitude < 1 && renderer.sprite != idleSprite) {
        //     renderer.sprite = idleSprite;
        // }
    }
}