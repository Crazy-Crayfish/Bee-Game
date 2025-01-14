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
        ChangeScene();
    }

    public void ChangeScene()
    {
        SceneManager.LoadScene(newSceneName, LoadSceneMode.Additive);

    }
}