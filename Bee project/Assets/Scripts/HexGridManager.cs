using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexGridManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private int width, height;
    [SerializeField] private HexTile hexTilePreFab;
    
    void Start() {
        GenerateGrid();
    }

   void GenerateGrid(){
    for (int x = 0; x < width; x++) {
        for (int y = 0; y < height; y++) {
            var isOffSet = (y%2 != 0);
            // WIP: scalar does nothing right now, 
            // want to eventually make it so the grid appearence supports different hex sizes
           // float scalar = (float)hexTilePreFab.GetComponent<Renderer>().transform.localScale.sqrMagnitude;
            float scalar = (float)41.0;
            float tempX = (float)x * (float) 1.3 * scalar;
            float tempY = (float)y * (float) 0.35 * scalar;
            if (isOffSet) {
                tempX += (float)0.65 * scalar;
            }
            var spawnedTile = Instantiate(hexTilePreFab, new Vector3(tempX,tempY), Quaternion.identity);
            spawnedTile.Init(isOffSet);
            spawnedTile.name = $"HexTile {x} {y}";
            

        }
    }
   }
   
}
