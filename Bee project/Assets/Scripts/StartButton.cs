using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartButton : MonoBehaviour
{
    [SerializeField] private GameObject screen;
    [SerializeField] private GameObject loadScreen;
    
    // Start is called before the first frame update
    void Start()
    {
        screen.SetActive(false);
        loadScreen.SetActive(false);
    }
    void OnMouseDown()
    {
        SfxManager.Instance.playButtonClickSound();
        screen.SetActive(true);
    }
    // // Update is called once per frame
    // void Update()
    // {
        
    // }
}
