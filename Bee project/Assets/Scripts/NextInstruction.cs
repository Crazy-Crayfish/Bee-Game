using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextInstruction : MonoBehaviour
{
    private float currentFrame;
    public int numFramesInAnim;

    public Animator anim;
    public string animName;

	void Start () {
        currentFrame = 1.00f / numFramesInAnim;
        Debug.Log("Current frame: " + currentFrame);
        anim.speed = 0;
		anim.Play(animName, 0, currentFrame);
        
	}

    void Update()
    {
        if (Input.GetKeyDown("space")) 
        {
            nextFrame();
        }
    }

    public void nextFrame()
    {
        Debug.Log("Current frame: " + currentFrame);
        if (currentFrame < 1.00f)
        {
            currentFrame += (1.00f / numFramesInAnim);
            anim.Play(animName, 0, currentFrame);
            Debug.Log("play next frame");
        }
        else
        {
            anim.Play(animName, 0, 1f);
            changeAnimation();
        }
    }

    void changeAnimation()
    {
        anim.SetInteger("Change", anim.GetInteger("Change") + 1);
        Debug.Log(anim.GetInteger("Change"));
    }
}
