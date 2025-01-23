using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class SceneSwapButton : MonoBehaviour
{
    [SerializeField] private Transform Cam;
    
    // Start is called before the first frame update
        void Start()
    {

    }

    void OnMouseDown()
    {
        ChangeScene();
    }
    public void ChangeScene()
    {
        Debug.Log("change scene");
        Camera_Controller cameraController = Cam.gameObject.GetComponent<Camera_Controller>();
        // cameraController.noMovement();
        // Camera.main.gameObject.transform.position = new Vector3(4000, 0, -20);
        ScreenManager.Instance.newState();
        //cameraController.normalMovement();
    }
    // public void ChangeScene()
    // {
    //     // if (mode == "Additive")
    //     // {
    //     //     GameObject[] obj = Scene.GetRootGameObjects();
    //     //     foreach (object o in obj)
    //     //     {
    //     //         if (o.GetComponent<SpriteRenderer> () != null)
    //     //         {
    //     //             o.GetComponent<SpriteRenderer>.SetActive(false);
    //     //             // Debug.Log(o.name);
    //     //         }
                
                
    //     //     }
    //     //     SceneManager.LoadScene(newSceneName, LoadSceneMode.Additive);
    //     // } 
    //     // else
    //     // {
    //     //     SceneManager.LoadScene(newSceneName, LoadSceneMode.Single);
    //     // // }
    //     // SceneManager.LoadScene(newSceneName, LoadSceneMode.Single);

    // }
}