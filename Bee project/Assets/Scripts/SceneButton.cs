using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class SceneButton : MonoBehaviour
{

    public bool backward;
    private float progressPercent = 0;
    private bool bLoadDone = false;
    private SfxManager SfxManager;
    [SerializeField] private GameObject loadScreen;
    [SerializeField] private Text loadProgressText;
    [SerializeField] private bool NewGame;

    void Start()
    {
        if (!backward)
        {
            backward = false;
        }
        loadScreen.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        if (progressPercent > 0) 
        {
            loadProgressText.text = (progressPercent * 100) + "%";
        }
        loadProgressText.text = (progressPercent * 100) + "%";
    }
    

    void OnMouseDown()
    {
        SfxManager.Instance.playButtonClickSound();
        ChangeScene();
    }

    public void ChangeScene()
    {
        // SceneManager.LoadSceneAsync(1);
        if (NewGame)
        {
            SaveClearer.Instance.ClearSaves();
        }
        
        IEnumerator LoadAsyncScene()
        {
            AsyncOperation asyncLoad;
            asyncLoad = SceneManager.LoadSceneAsync(1, LoadSceneMode.Single);
            asyncLoad.allowSceneActivation = false;
            //wait until the asynchronous scene fully loads
            while (!asyncLoad.isDone)
            {
                progressPercent = Mathf.Clamp01(asyncLoad.progress / .9f);
                //scene has loaded as much as possible,
                // the last 10% can't be multi-threaded
                if (asyncLoad.progress >= 0.9f)
                {
                    asyncLoad.allowSceneActivation = true;
                }
                yield return null;
            }
            bLoadDone = asyncLoad.isDone;
        }
    StartCoroutine(LoadAsyncScene()); //call to begin loading scene

    loadScreen.SetActive(true);
    }
}