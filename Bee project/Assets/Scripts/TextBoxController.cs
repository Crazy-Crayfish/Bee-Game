using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimController : MonoBehaviour
{

    private int currentFrame;
    public int numFramesInAnim;

    // Start is called before the first frame update
    void Start()
    {
        currentFrame = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void nextFrame()
    {
        if (currentFrame < 1.00)
        {
            currentFrame += (1 / numFramesInAnim);
        }
        if (currentFrame >= 1.00)
        {
            
        }
    }
}
