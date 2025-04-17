using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using System.Linq;

public class Queen : MonoBehaviour
{
    
    public bool[] eggPositions;
    public GameObject[] eggObjects;
    private bool eggsFull;
    [SerializeField] private GameObject eggToSpawn;

    // Start is called before the first frame update
    void Awake()
    {
        eggPositions = new bool[6] {false, false, false, false, false, false};
        eggObjects = new GameObject[6] {null, null, null, null, null, null};

        
        eggsFull = false;
        // spawn eggs every 20sec
        InvokeRepeating("SummonEgg", 0.0f, 20.0f);
    }
    private void SummonEgg()
    {
        
        // Debug.Log("Queen thinks eggPos length is: " + eggPositions.Length);
        // string a = "Queen thinks eggPos is: ";
        // foreach(bool b in eggPositions)
        // {
        //     a += b + " ";
        // }
        // Debug.Log(a);
        // HexGridManager.Instance.getEggAtPos(0);
        if (!eggsFull)
        {
            GameObject centerTile = HexGridManager.Instance.tileList[HexGridManager.Instance.width / 2, HexGridManager.Instance.height / 2];
            int pos = Random.Range(0,6);
            while (eggPositions[pos])
            {
                pos = Random.Range(0,6);
            }
            eggPositions[pos] = true;
            GameObject tileToSpawnOn = HexGridManager.Instance.getNeighborAtPos(centerTile, pos);
            GameObject egg = Instantiate(eggToSpawn,
                            new Vector3(tileToSpawnOn.transform.position.x,tileToSpawnOn.transform.position.y, -5), 
                            Quaternion.identity);
            eggObjects[pos] = egg;
            // Update eggsFull
            eggsFull = true;
            foreach (bool filled in eggPositions)
            {
                if (!filled)
                {
                    eggsFull = false;
                }
            }            
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
