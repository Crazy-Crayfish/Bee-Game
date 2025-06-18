using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitButton : MonoBehaviour
{
    void OnMouseDown()
    {
        SfxManager.Instance.playButtonClickSound();
        //Debug.Log("quit");
        Application.Quit();
    }
}
