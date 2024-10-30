using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private int width, height;
    [SerializeField] private Tile tilePreFab;
    [SerializeField] private Transform camera;
    [SerializeField] private Transform hive;

    void Start() {
        GenerateGrid();
    }

   void GenerateGrid(){
    for (int x = 0; x < width; x++) {
        for (int y = 0; y < height; y++) {
            
            var spawnedTile = Instantiate(tilePreFab, new Vector3(x,y), Quaternion.identity);
            spawnedTile.name = $"Tile {x} {y}";

            var isOffSet = (x % 2 == 0 && y%2 !=0) || (y%2 == 0 && x%2 != 0);
            spawnedTile.Init(isOffSet);
        }
    }
   
   camera.transform.position = new Vector3((float)width/2 -0.5f, (float)height / 2 -0.5f,-20);
   hive.transform.position = new Vector3((float)width/2 -0.5f, (float)height / 2 -0.5f,-10);
   }
}
