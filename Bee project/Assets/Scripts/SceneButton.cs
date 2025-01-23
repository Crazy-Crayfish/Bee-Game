using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneButton : MonoBehaviour
{

    // Start is called before the first frame update
    public bool backward;

    void Start()
    {
        if (!backward)
        {
            backward = false;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseDown()
    {
        ChangeScene();
    }

    public void ChangeScene()
    {
        SceneManager.LoadScene(1);

    }
}