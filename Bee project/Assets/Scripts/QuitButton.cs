using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitButton : MonoBehaviour
{
    void OnMouseDown()
    {
        Debug.Log("quit");
        Application.Quit();
    }
}
