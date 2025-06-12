using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsButton : MonoBehaviour
{
    public bool open;
    // Start is called before the first frame update
    void Start()
    {
        if(!open){
            open = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnMouseDown()
    {
        //code to open menu
    }
}
