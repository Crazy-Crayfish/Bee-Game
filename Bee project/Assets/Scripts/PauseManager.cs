using UnityEngine.SceneManagement;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public static PauseManager Instance { get; set; }

    [SerializeField] private GameObject pauseMenuUI;
    [SerializeField] private GameObject GameOverScreen;

    public bool isPaused = false;

    private void Awake() 
    {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
        } else {
            Instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        ResumeGame();
        GameOverScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }
    }

    public void PauseGame()
    {
        SfxManager.Instance.playButtonClickSound();
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0;
        isPaused = true;
    }

    public void ResumeGame()
    {
        //button click?? for some reason makes game over load instantly when world scene loads
        //SfxManager.Instance.playButtonClickSound();
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1;
        isPaused = false;
    }

    public void GameOver()
    {
        GameOverScreen.SetActive(true);
        Time.timeScale = 0;
        isPaused = true;
    }
    
    public void goToMainMenu()
    {
        SfxManager.Instance.playButtonClickSound();
        // maybe put a confirm check?

        SceneManager.LoadScene(0);
    }
}
