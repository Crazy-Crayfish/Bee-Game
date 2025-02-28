using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using UnityEngine.Random;

public class Queen : MonoBehaviour
{
    private int[] eggPositions;
    // Start is called before the first frame update
    void Start()
    {
        eggPositions = new int[6];
        InvokeRepeating("SummonEgg", 30.0f, 30.0f);
    }

    private void SummonEgg()
    {
        
        // GameObject centerTile = HexGridManager.Instance.tileList[HexGridManager.Instance.width / 2, HexGridManager.Instance.height / 2];
        // int pos = Random.Range(0,6);
        // while (eggPositions.Contains(pos))
        // {
        //     pos = Random.Range(0,6);
        // }
        // eggPositions.Add(pos);
        

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
