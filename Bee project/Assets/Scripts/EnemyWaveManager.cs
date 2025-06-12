using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// using UnityEngine.Random;

public class EnemyWaveManager : MonoBehaviour, IDataPersistence
{
    public static EnemyWaveManager Instance { get; set; }
    [SerializeField] private GameObject hive;
    [SerializeField] private GameObject enemyAntPreFab;
    [SerializeField] private GameObject enemySpiderPreFab;
    [SerializeField] private GameObject enemyBadgerPreFab;
    [SerializeField] public float waveCooldown;
    [SerializeField] private float timeUntilFirstWave;
    [SerializeField] private Text waveTimerText;
    [SerializeField] private Text waveAlertText;
    private SfxManager SfxManager;

    private float timeUntilNextWave;

    private int enemyCount;
    private int waveNum;
    
    private void Awake() 
    {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
        } else {
            Instance = this;
        }
    }

    public void SaveData(ref GameData data)
    {
        data.allEnemyUnits = new List<EnemySaveData>();
        data.waveTimer = timeUntilNextWave;
        data.waveNum = waveNum;
        
        foreach (GameObject enemy in UnitSelectionManager.Instance.allEnemiesList)
        {
            if (enemy == null) continue;

            EnemyUnit e = enemy.GetComponent<EnemyUnit>();
            if (e == null) continue;

            string type = "";
            if (enemy.name.Contains("Ant")) type = "Ant";
            else if (enemy.name.Contains("Spider")) type = "Spider";
            else if (enemy.name.Contains("Badger")) type = "Badger";
            else continue;

            EnemySaveData saveData = new EnemySaveData
            {
                x = enemy.transform.position.x,
                y = enemy.transform.position.y,
                z = enemy.transform.position.z,
                health = e.health,
                enemyType = type
            };

            data.allEnemyUnits.Add(saveData);
        }
    }

    public void LoadData(GameData data)
    {
        if (data.allEnemyUnits == null) return;
        timeUntilNextWave = data.waveTimer;
        waveNum = data.waveNum;
        foreach (EnemySaveData enemyData in data.allEnemyUnits)
        {
            Vector3 pos = new Vector3(enemyData.x, enemyData.y, enemyData.z);
            GameObject enemy = null;

            if (enemyData.enemyType == "Ant")
            {
                enemy = Instantiate(enemyAntPreFab, pos, Quaternion.identity);
                enemy.name = "Ant";
                enemy.GetComponent<EnemyUnit>().health = enemyData.health;
            }
            else if (enemyData.enemyType == "Spider")
            {
                enemy = Instantiate(enemySpiderPreFab, pos, Quaternion.identity);
                enemy.name = "Spider";
                enemy.GetComponent<EnemyUnit>().health = enemyData.health;
            }
            else if (enemyData.enemyType == "Badger")
            {
                enemy = Instantiate(enemyBadgerPreFab, pos, Quaternion.identity);
                enemy.name = "Badger";
                enemy.GetComponent<EnemyUnit>().health = enemyData.health;
            }
        }
    }


    // Start is called before the first frame update
    void Start()
    {
         if (DataPersistenceManager.instance == null || !DataPersistenceManager.instance.IsNewGame)
    {
        // It's a load operation — skip new unit creation
        waveAlertText.gameObject.SetActive(false);

        return;
    }
        //// CHANGE THESE TO ADJUST DIFFICULTY 
        // waveCooldown = 10.0f;
        // float timeUntilFirstWave = 10.0f; // 2:30
        enemyCount = 1;
        ////
        timeUntilNextWave = timeUntilFirstWave;
        waveAlertText.gameObject.SetActive(false);
        // Make waves start spawning regularly
        waveNum = 0;
        InvokeRepeating("TriggerWave", timeUntilFirstWave, waveCooldown);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.L)) // Debug wave summoner
        {
            TriggerWave();
        }
        
        if (timeUntilNextWave > 0)
        {
            if (timeUntilNextWave < waveCooldown - 8.0f && waveAlertText.gameObject.activeSelf == true)
            {
                waveAlertText.gameObject.SetActive(false);
            }
            timeUntilNextWave -= Time.deltaTime;
        }
        else
        {
            // ShowWaveAlert();
            waveAlertText.gameObject.SetActive(true);
            timeUntilNextWave = waveCooldown; // Ensure timer doesn't go negative
        }
        waveTimerText.text = "Enemy wave " + (waveNum + 1) + " in: " + Mathf.Ceil(timeUntilNextWave).ToString() + "s";
    }
    
    // private void ShowWaveAlert()
    // {
    //     waveAlertText.gameObject.SetActive(true);
    //     float timeToVanish = 8.0f;
    //     float timePassed = 0.0f;
    //     while (timePassed < timeToVanish)
    //     {
    //         timePassed += Time.deltaTime;
    //     }
    //     waveAlertText.gameObject.SetActive(false);
    // }

    private void TriggerWave()
    {
        // Wave music
        BackgroundMusicController.Instance.WaveMusicOn();
        // waveNum increment
        waveNum++;
        GridManager.Instance.growMap();
        // Increase enemies by 1 every 2 waves
        if (waveNum % 2 == 0)
        {
            enemyCount++;
            GridManager.Instance.regenRes();
        }

        if (waveNum % 6 == 0){
            GridManager.Instance.rocks();
        }

        // Spawn enemies
        SummonWave(hive.transform.position + new Vector3(0, 0, -hive.transform.position.z));
    }

    private void SummonWave(Vector3 center)
    {
        // Make a random direction vector
        Vector3 randomDirection = new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f), 0);
        
        // Extend it by 20 units (CHANGE THIS TO CHANGE HOW FAR AWAY ENEMIES SPAWN ON AVERAGE)
        randomDirection.Normalize();
        randomDirection = randomDirection * 20; 
        if (waveNum >= 6)
        {
            Vector3 offset = new Vector3(Random.Range(-5.0f, 5.0f), Random.Range(-5.0f, 5.0f), 0);
            var newBadger = Instantiate(enemyBadgerPreFab, (center + randomDirection) + offset, Quaternion.identity);
            // BADGERS HAVE 3X HEALTH BUT 0.5X SPEED
            newBadger.GetComponent<EnemyUnit>().health = 450;
            newBadger.name = "Badger";
        }
        else
        {
            // For each enemy needed, create a new enemy instance offset by a slight random value
            for (int i = 0; i < enemyCount; i++)
            {
                Vector3 offset = new Vector3(Random.Range(-5.0f, 5.0f), Random.Range(-5.0f, 5.0f), 0);
                if (waveNum < 3)
                {
                var newAnt = Instantiate(enemyAntPreFab, (center + randomDirection) + offset, Quaternion.identity);            
                newAnt.GetComponent<EnemyUnit>().health = 150;
                newAnt.name = "Ant";
                }
                if (waveNum >= 3 && waveNum < 6)
                {
                    // SPIDERS HAVE 2/3X HEALTH BUT 1.5X SPEED
                    var newSpider = Instantiate(enemySpiderPreFab, (center + randomDirection) + offset, Quaternion.identity);
                    newSpider.name = "Spider";
                    newSpider.GetComponent<EnemyUnit>().health = 100;
                }

            }
        }



    }
}

[System.Serializable]
public class EnemySaveData
{
    public float x, y, z;
    public int health;
    public string enemyType; // "Ant", "Spider", or "Badger"
}
