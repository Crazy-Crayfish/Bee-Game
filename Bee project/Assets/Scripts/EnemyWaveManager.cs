using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using UnityEngine.Random;

public class EnemyWaveManager : MonoBehaviour
{
    public static EnemyWaveManager Instance { get; set; }
    [SerializeField] private GameObject hive;
    [SerializeField] private GameObject enemyPreFab;
    public float waveCooldown;
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
        //// CHANGE THESE TO ADJUST DIFFICULTY 
        waveCooldown = 60.0f;
        float timeUntilFirstWave = 1.0f;
        enemyCount = 1;
        ////

        // Make waves start spawning regularly
        waveNum = 0;
        InvokeRepeating("TriggerWave", timeUntilFirstWave, waveCooldown);
    }

    private void TriggerWave()
    {
        waveNum++;
        // Increase enemies by 1 every 3 waves
        if (waveNum % 3 == 0)
        {
            enemyCount++;
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

        // For each enemy needed, create a new enemy instance offset by a slight random value
        for (int i = 0; i < enemyCount; i++)
        {
            Vector3 offset = new Vector3(Random.Range(-5.0f, 5.0f), Random.Range(-5.0f, 5.0f), 0);
            var newEnemy = Instantiate(enemyPreFab, (center + randomDirection) + offset, Quaternion.identity);
            newEnemy.GetComponent<EnemyUnit>().health = 150;
        }

    }
}
