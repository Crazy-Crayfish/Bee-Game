using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutorialManagerScript : MonoBehaviour
{

    
    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space")) 
        {
            changeAnimation();
        }
    }

    void changeAnimation()
    {
        anim.SetInteger("Change", anim.GetInteger("Change") + 1);
        Debug.Log(anim.GetInteger("Change"));
    }
}
