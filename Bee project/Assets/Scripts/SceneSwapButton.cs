using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwapButton : MonoBehaviour
{
    [SerializeField] private string newSceneName;
    // Start is called before the first frame update
        void Start()
    {

    }
        void Update()
    {

    }
    void OnMouseDown()
    {
        Debug.Log("change scene");
        ChangeScene();
    }

    public void ChangeScene()
    {
        
        SceneManager.LoadScene(newSceneName);

    }
}