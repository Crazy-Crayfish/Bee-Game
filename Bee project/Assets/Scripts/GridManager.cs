using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = System.Random;

public class GridManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private int width, height, difficultyModifier;
    [SerializeField] private Tile tilePreFab;
    [SerializeField] private Transform camera;
    [SerializeField] private Transform hive;
    [SerializeField] private Flower flowerTilePreFab;
    [SerializeField] private Tile pinkFlower;
    [SerializeField] private Tile blueFlower;
    [SerializeField] private Tile sunFlower;
    [SerializeField] private Tile redFlower;
    // Not a real difficultyModifier Yet

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
            spawnedTile.setVal(0);
        }
    }
   
   camera.transform.position = new Vector3((float)width/2 -0.5f, (float)height / 2 -0.5f,-20);
   hive.transform.parent.transform.position = new Vector3((float)width/2 -0.5f, (float)height / 2 -0.5f,-10);
    Random rnd = new Random();
    int seed = rnd.Next(1111111,9999999);

    GenerateElements(seed);

   }
   void GenerateElements(int modifier){
    // int flowerMod = modifier/100000;
    // can be used to make game easier
    // if(flowerMod > 43 ) {flowerMod /= 2; }
    long flowerMod = (long) Math.Pow(modifier, difficultyModifier);
 
    GenerateFlowers(flowerMod);



   }


   void GenerateFlowers(long mod){
   
    int area = height*width;
    int y = 0;
    Random rnd1 = new Random();
    long flowSeed = (long) Math.Pow(rnd1.Next(1111111,9999999), difficultyModifier);
    long flowtemp = flowSeed;
    
    long tempSeed = mod;


    
    for(int x = 0; x<area; x+=(int) (tempSeed%100)) {
        int z = x%width;
        y = x/width;
        // var spawneTile = Instantiate(flowerTilePreFab, new Vector3(z,y), Quaternion.identity); //PURELY COSMETIC FOR NOW
        // spawneTile.name = $"flowerTileBase {z} {y}";
        // REIMPLEMENT LATER AFTER FIXING DEPTH ISSUES

       
        long zzz = flowtemp%100;
        flowtemp /= 100;
        if(flowtemp == 0) {
            flowSeed = flowSeed*2;
            flowtemp = flowSeed;
        }
        

        if(zzz>=0 && zzz <= 39) {
            var spawnedTile = Instantiate(pinkFlower, new Vector3(z,y+0.1f), Quaternion.identity);
            spawnedTile.name = $"pinkFlowerTile {z} {y}";
            spawnedTile.setVal(50);
        }
        else if (zzz >= 40 && zzz <= 69) {
            var spawnedTile = Instantiate(blueFlower, new Vector3(z,y+0.1f), Quaternion.identity);
            spawnedTile.name = $"blueFlowerTile {z} {y}";
            spawnedTile.setVal(80);
        }
        else if (zzz >= 70 && zzz <= 89) {
            var spawnedTile = Instantiate(sunFlower, new Vector3(z,y+0.1f), Quaternion.identity);
            spawnedTile.name = $"sunFlowerTile {z} {y}";
            spawnedTile.setVal(100);
        }
         else {
            var spawnedTile = Instantiate(redFlower, new Vector3(z,y+0.1f), Quaternion.identity);
            spawnedTile.name = $"redFlowerTile {z} {y}";
            spawnedTile.setVal(150);
        }

    tempSeed = tempSeed / 100;
    if(tempSeed == 0) {
        mod = mod *2;
        tempSeed = mod;
    }
    
    }
   }
}

// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using System;
// using Random = System.Random;

// public class GridManager : MonoBehaviour
// {
//     // Start is called before the first frame update
//     [SerializeField] private int width, height;
//     [SerializeField] private Tile tilePreFab;
//     [SerializeField] private Transform camera;
//     [SerializeField] private Transform hive;
//        [SerializeField] private Tile flowerTilePreFab;

//     void Start() {
//         GenerateGrid();
//     }

//    void GenerateGrid(){
//     for (int x = 0; x < width; x++) {
//         for (int y = 0; y < height; y++) {
            
//             var spawnedTile = Instantiate(tilePreFab, new Vector3(x,y), Quaternion.identity);
//             spawnedTile.name = $"Tile {x} {y}";
//             var isOffSet = (x % 2 == 0 && y%2 !=0) || (y%2 == 0 && x%2 != 0);
//             spawnedTile.Init(isOffSet);

//         }
//     }
   
//    camera.transform.position = new Vector3((float)width/2 -0.5f, (float)height / 2 -0.5f,-20);
//    hive.transform.position = new Vector3((float)width/2 -0.5f, (float)height / 2 -0.5f,-10);
//     Random rnd = new Random();
//     int seed = rnd.Next(1111111,9999999);

//     GenerateElements(seed);

//    }
//    void GenerateElements(int modifier){
//     int flowerMod = modifier/100000;
//     // can be used to make game easier
//     if(flowerMod > 43 ) {flowerMod /= 2; }
//     GenerateFlowers(flowerMod);



//    }


//    void GenerateFlowers(int mod){
//     int area = height*width;
//     int y = 0;
//     Random rnd1 = new Random();
    
//     for(int x = 0; x<area; x+=rnd1.Next(1, mod)) {
//         int z = x%width;
//         y = x/width;
//          var spawneTile = Instantiate(flowerTilePreFab, new Vector3(z,y), Quaternion.identity);
//             spawneTile.name = $"flowerTile {z} {y}";


//     }
//    }
// }