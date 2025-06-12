using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildButton : MonoBehaviour
{
    public bool popUpOpen;
    [SerializeField] GameObject popUpMenu;
    private Vector2 pos;
    // Start is called before the first frame update
    void Start()
    {
        popUpOpen = false;
        pos = popUpMenu.GetComponent<RectTransform>().anchoredPosition;
    }

    // // Update is called once per frame
    // void Update()
    // {
        
    // }

    public void popUp()
    {
        if (popUpOpen)
        {
            popUpMenu.GetComponent<RectTransform>().anchoredPosition = new Vector2(-1000, 0);
            popUpOpen = false;
        }
        else
        {
            // popUpMenu.transform.position = new Vector3(125, 260, 0);
            popUpMenu.GetComponent<RectTransform>().anchoredPosition = pos;
            popUpOpen = true;
        }
    }
}
