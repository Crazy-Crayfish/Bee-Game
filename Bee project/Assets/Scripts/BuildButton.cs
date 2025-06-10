using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildButton : MonoBehaviour
{
    private bool popUpOpen;
    [SerializeField] GameObject popUpMenu;
    // Start is called before the first frame update
    void Start()
    {
        popUpOpen = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void popUp()
    {
        if (popUpOpen)
        {
            popUpMenu.transform.position = new Vector3(-1000, 0, 0);
            popUpOpen = false;
        }
        else
        {
            popUpMenu.transform.position = new Vector3(125, 260, 0);
            popUpOpen = true;
        }
    }
}
