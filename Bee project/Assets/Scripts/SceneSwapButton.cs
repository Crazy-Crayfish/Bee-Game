using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class SceneSwapButton : MonoBehaviour
{
    [SerializeField] private string newSceneName;
    [SerializeField] private string mode;
    
    // Start is called before the first frame update
        void Start()
    {

    }

    void OnMouseDown()
    {
        Debug.Log("change scene");
        ChangeScene();
    }

    public void ChangeScene()
    {
        // if (mode == "Additive")
        // {
        //     GameObject[] obj = Scene.GetRootGameObjects();
        //     foreach (object o in obj)
        //     {
        //         if (o.GetComponent<SpriteRenderer> () != null)
        //         {
        //             o.GetComponent<SpriteRenderer>.SetActive(false);
        //             // Debug.Log(o.name);
        //         }
                
                
        //     }
        //     SceneManager.LoadScene(newSceneName, LoadSceneMode.Additive);
        // } 
        // else
        // {
        //     SceneManager.LoadScene(newSceneName, LoadSceneMode.Single);
        // }
        SceneManager.LoadScene(newSceneName, LoadSceneMode.Single);

    }
}