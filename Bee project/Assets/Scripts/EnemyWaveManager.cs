using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using UnityEngine.Random;

public class EnemyWaveManager : MonoBehaviour
{
    public static EnemyWaveManager Instance { get; set; }
    [SerializeField] private GameObject hive;
    [SerializeField] private GameObject enemyPreFab;
    public float totalWaveTimer;
    public float currentWaveTimer;
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

    // Start is called before the first frame update
    void Start()
    {
        totalWaveTimer = 30.0f;
        currentWaveTimer = totalWaveTimer;
        enemyCount = 1;
        waveNum = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
        currentWaveTimer -= Time.deltaTime;

        if (currentWaveTimer <= 0.0f)
        {
            waveNum++;
            if (waveNum % 3 == 0)
            {
                enemyCount++;
            }
            SummonWave(hive.transform.position + new Vector3(0, 0, -hive.transform.position.z));
            currentWaveTimer = totalWaveTimer;

        }
        
    }
    private void SummonWave(Vector3 center)
    {
        // Random RNG = new Random();
        // int side = RNG.Next(0,4); // 0 = top, 1 = right, 2 = down, 3 = left
        // for (int i = 0; i < 4; i++)
        // {
        //     Vector3 offset = (RNG.Next(-10,11),RNG.Next(-10,11),0);
        //     var newEnemy = Instantiate(enemyPreFab, (location * Quaternion.Euler(0, 0, side * 90)) + offset, Quaternion.identity);
        //     newEnemy.AddComponent<EnemyWaspLogic>();
        // }
        
        //int side = RNG.Next(0,4); // 0 = top, 1 = right, 2 = down, 3 = left
        Vector3 randomDirection = new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f), 0);
        randomDirection.Normalize();
        randomDirection = randomDirection * 20; // random direction, magnitude 20. purpose is to start them a distance away
        for (int i = 0; i < enemyCount; i++)
        {
            Vector3 offset = new Vector3(Random.Range(-5.0f, 5.0f), Random.Range(-5.0f, 5.0f), 0);
            var newEnemy = Instantiate(enemyPreFab, (center + randomDirection) + offset, Quaternion.identity);
            newEnemy.GetComponent<EnemyUnit>().health = 150;
        }

    }
}
