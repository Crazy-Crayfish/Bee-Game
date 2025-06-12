using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    private SfxManager SfxManager;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void openScene(string name)
    {
        SfxManager.Instance.playButtonClickSound();
        SceneManager.LoadScene(name);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
