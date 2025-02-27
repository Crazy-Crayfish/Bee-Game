using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = System.Random;

public class GridManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static GridManager Instance; 
    [SerializeField] private int width, height, difficultyModifier;
    [SerializeField] private Tile tilePreFab;
    [SerializeField] private Transform camera;
    [SerializeField] private Transform hive;
    [SerializeField] private Flower flowerTilePreFab;
    [SerializeField] private Tile pinkFlower;
    [SerializeField] private Tile blueFlower;
    [SerializeField] private Tile sunFlower;
    [SerializeField] private Tile redFlower;
    [SerializeField] private Tile treeTile;
    private int bottom, west = 0;
    // Not a real difficultyModifier Yet

    void Start() {
        Instance = this;
        GenerateGrid();
    }

   void GenerateGrid(){
    for (int x = 0; x < width; x++) {
        for (int y = 0; y < height; y++) {
            
            var spawnedTile = Instantiate(tilePreFab, new Vector3(x,y), Quaternion.identity);
            spawnedTile.name = $"Tile {x} {y}";

            // var isOffSet = (x % 2 == 0 && y%2 !=0) || (y%2 == 0 && x%2 != 0);
            // spawnedTile.Init(isOffSet);
            // spawnedTile.setVal(0);
        }
    }
   
   camera.transform.position = new Vector3((float)width/2 -0.5f, (float)height / 2 -0.5f,-20);
   hive.transform.position = new Vector3((float)width/2 -0.5f, (float)height / 2 -0.5f,-10);
    Random rnd = new Random();
    int seed = rnd.Next(1111111,9999999);

    GenerateElements(seed);

   }
   void GenerateElements(int modifier){
<<<<<<< Updated upstream
    int flowerMod = modifier/100000;
    // can be used to make game easier
    // if(flowerMod > 43 ) {flowerMod /= 2; }
=======
    long flowerMod = (long) Math.Pow(modifier, difficultyModifier);
 
>>>>>>> Stashed changes
    GenerateFlowers(flowerMod);
    GenerateTrees();


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
        

    flowerType(zzz, z, y);
    tempSeed = tempSeed / 100;
    if(tempSeed == 0) {
        mod = mod *2;
        tempSeed = mod;
    }
    
    }
   }




   
   
   void GenerateTrees() { 
    int area = height * width; 
    int y = 0; 
    Random rnd = new Random(); 

    for (int x = 0; x < area; x += (int)(rnd.Next(1,100))) { 
        int z = x % width; 
        y = x / width; 

        if (!IsFlowerTile(z, y)) { 
            // Debug.Log($"Attempting to instantiate treeTile at {z}, {y}");
            var spawnedTile = Instantiate(treeTile, new Vector3(z, y), Quaternion.identity); 
            spawnedTile.name = $"treeTile {z} {y}"; 
            int a = z;
            int b = y;
            for(int i = 0; i < (int) rnd.Next(3,6); i++) {
                
                int dice = (int) (rnd.Next(1,5));
                

                switch(dice) {
                    case 1:
                        
                        if (!isOccupied(a+1,b) && isInBounds(a+1,b)) {
                            a++;
                            var spawnedTile1 = Instantiate(treeTile, new Vector3(a, b), Quaternion.identity); 
                            spawnedTile1.name = $"treeTile {a} {b}";
                            // Debug.Log($"right"+a+" "+b);
                        }
                        break;
                    
                    case 2:
                        if (!isOccupied(a,b+1) && isInBounds(a,b+1)) {
                            b++;
                            var spawnedTile2 = Instantiate(treeTile, new Vector3(a, b), Quaternion.identity); 
                            spawnedTile2.name = $"treeTile {a} {b}";
                            // Debug.Log($"up"+a+" "+b);
                        }
                        break;    

                    case 3:
                        if (!isOccupied(a-1,b) && isInBounds(a-1,b)) {
                            a--;
                            var spawnedTile3 = Instantiate(treeTile, new Vector3(a, b), Quaternion.identity); 
                            spawnedTile3.name = $"treeTile {a} {b}";
                            // Debug.Log($"left"+a+" "+b);
                        }
                        break;

                    case 4:
                        if (!isOccupied(a,b-1) && isInBounds(a,b-1)) {
                            b--;
                            var spawnedTile4 = Instantiate(treeTile, new Vector3(a, b), Quaternion.identity); 
                            spawnedTile4.name = $"treeTile {a} {b}";
                            // Debug.Log($"down"+a+" "+b);
                        }
                        break;    

                }



                }

            
            }
        } 
    }
   
   
   
   //Check if tile is occupied by flower
    bool IsFlowerTile(int x, int y) { // Check if a tile at position (x, y) is a flower tile 
        var tileName = $"FlowerTile {x} {y}";
            GameObject tileObject = GameObject.Find(tileName); 
            if (tileObject != null && (tileObject.name.Contains("Flower"))) 
            { 
                return true; 
            } 
            return false;
        }

    //check if tile is occupied by tree    
    bool IsTreeTile(int x, int y) {
        var tileName = $"TreeTile {x} {y}";
        GameObject tileObject = GameObject.Find(tileName);
        if (tileObject != null && (tileObject.name.Contains("Tree")))
        {
            return true;
        }
        return false;
    }


    //check if tile is occupied
    bool isOccupied(int x, int y) {
        return (IsTreeTile(x,y) || IsFlowerTile(x,y));
    }

    //check if tile is in bounds
    bool isInBounds(int x, int y) {
        if ((x < west || x >= width) || (y < bottom || y >= height))
            return false;
        return true;
    }


    //grows map
    public void growMap() {
        Debug.Log($"growing map");
        bottom--;
        west--;
        height++;
        width++;
        //spawning side stripes
        for( int i = bottom; i < height; i++) {
            var spawnedTile1 = Instantiate(tilePreFab, new Vector3(west,i,1), Quaternion.identity);
            spawnedTile1.name = $"Tile {west} {i}";
            var spawnedTile2 = Instantiate(tilePreFab, new Vector3(width-1,i,1), Quaternion.identity);
            spawnedTile2.name = $"Tile {width-1} {i}";
            
        }
        for ( int j = west+1; j < width; j++) {
            var spawnedTile3 = Instantiate(tilePreFab, new Vector3(j,height-1,1), Quaternion.identity);
            spawnedTile3.name = $"Tile {j} {height-1}";
            var spawnedTile4 = Instantiate(tilePreFab, new Vector3(j,bottom,1), Quaternion.identity);
            spawnedTile4.name = $"Tile {j} {bottom}";
            
            
        }
    }


    //uses modifier to spawn the right color flower
    void flowerType(long zzz, int z, int y) { 
        if(zzz>=0 && zzz <= 39) {
            var spawnedTile = Instantiate(pinkFlower, new Vector3(z,y+0.1f), Quaternion.identity);
            spawnedTile.name = $"FlowerTile {z} {y}";
            spawnedTile.setVal(50);
        }
        else if (zzz >= 40 && zzz <= 69) {
            var spawnedTile = Instantiate(blueFlower, new Vector3(z,y+0.1f), Quaternion.identity);
            spawnedTile.name = $"FlowerTile {z} {y}";
            spawnedTile.setVal(80);
        }
        else if (zzz >= 70 && zzz <= 89) {
            var spawnedTile = Instantiate(sunFlower, new Vector3(z,y+0.1f), Quaternion.identity);
            spawnedTile.name = $"FlowerTile {z} {y}";
            spawnedTile.setVal(100);
        }
         else {
            var spawnedTile = Instantiate(redFlower, new Vector3(z,y+0.1f), Quaternion.identity);
            spawnedTile.name = $"FlowerTile {z} {y}";
            spawnedTile.setVal(150);
        }
    }


    //Regenerates map resources
    public void regenRes() {
        Debug.Log($"regen resources");
        Random rnd = new Random();
        for (int i = 0; i< (height-bottom) * (width-west);i += (int) rnd.Next(1,200)  ) {
            int x = (i % (width-west)) + west;
            int y = i / (width - west) + bottom;

            if(!isOccupied(x,y)) {
                int zzz = (int) rnd.Next(0, 100);
                flowerType(zzz, x, y);  
            }

        }
    }
<<<<<<< Updated upstream
   }




   
   
   void GenerateTrees(){ int area = height * width; 
   int y = 0; 
   Random rnd = new Random(); 
//    long treeSeed = (long)Math.Pow(rnd.Next(1111111, 9999999), difficultyModifier); 
//    long treetemp = treeSeed; 
   for (int x = 0; x < area; x += (int)(rnd.Next(1,100))) { 
    int z = x % width; 
    y = x / width; 
    // long zzz = treetemp % 100; 
    // treetemp /= 100; 
    // if (treetemp == 0) { 
    //     treeSeed = treeSeed * 2; 
    //     treetemp = treeSeed; 
    //     } 
     if (!IsFlowerTile(z, y)) { 
        Debug.Log($"Attempting to instantiate treeTile at {z}, {y}");
        var spawnedTile = Instantiate(treeTile, new Vector3(z, y), Quaternion.identity); 
        spawnedTile.name = $"treeTile {z} {y}"; 
            
         }
             } 
   }
   
   
   
   
    bool IsFlowerTile(int x, int y) { // Check if a tile at position (x, y) is a flower tile 
   var tileName = $"FlowerTile {x} {y}";
    GameObject tileObject = GameObject.Find(tileName); 
    if (tileObject != null && (tileObject.name.Contains("Flower"))) 
    { 
        return true; 
    } 
    return false;
}
<<<<<<< Updated upstream
=======
=======
>>>>>>> Stashed changes

}
>>>>>>> Stashed changes
