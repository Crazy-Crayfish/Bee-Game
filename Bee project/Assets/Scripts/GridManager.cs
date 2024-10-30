using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = System.Random;

public class GridManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private int width, height;
    [SerializeField] private Tile tilePreFab;
    [SerializeField] private Transform camera;
    [SerializeField] private Transform hive;
       [SerializeField] private Tile flowerTilePreFab;

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
    Random rnd = new Random();
    int seed = rnd.Next(1111111,9999999);

    GenerateElements(seed);

   }
   void GenerateElements(int modifier){
    int flowerMod = modifier/100000;
    // can be used to make game easier
    // if(flowerMod > 43 ) {flowerMod /= 2; }
    GenerateFlowers(flowerMod);



   }


   void GenerateFlowers(int mod){
    int area = height*width;
    int y = 0;
    Random rnd1 = new Random();
    
    for(int x = 0; x<area; x+=rnd1.Next(1, mod)) {
        int z = x%width;
        y = x/width;
         var spawneTile = Instantiate(flowerTilePreFab, new Vector3(z,y), Quaternion.identity);
            spawneTile.name = $"flowerTile {z} {y}";


    }
   }
}
