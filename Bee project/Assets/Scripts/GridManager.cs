using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = System.Random;

public class GridManager : MonoBehaviour, IDataPersistence
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
    [SerializeField] private Tile rockTile;
    public List <GameObject> tileList;
    private int bottom, west = 0;
    public KdTree2D kdTree;
    // Not a real difficultyModifier
    void Awake() {
        Instance = this;
        Debug.Log("GridManager instance set");

        tileList = new List<GameObject>();
    }                                            
    void Start() {
        hive.transform.position = new Vector3((float)width/2 -0.5f, (float)height / 2 -0.5f, -3);

        
        kdTree = new KdTree2D();
        if (DataPersistenceManager.instance == null || !DataPersistenceManager.instance.IsNewGame)
        {
        // It's a load operation — skip new unit creation
            return;
        }
        Debug.Log("it's a new Game!");
        GenerateGrid();
    }


public void LoadData(GameData data)
{
    tileList.Clear();
    foreach (TileSaveData tileData in data.tileSaveList)
    {
        Tile prefab = GetPrefabByType(tileData.type);
        if (prefab == null) continue;

        Tile newTile = Instantiate(prefab, new Vector3(tileData.x, tileData.y, tileData.z), Quaternion.identity);
        newTile.name = $"{tileData.type}Tile {tileData.x} {tileData.y}";
        newTile.type = tileData.type;
        newTile.value = tileData.value;
        newTile.maxValue = tileData.maxValue;

        // Only setVal if it’s a flower type (optional)
        if (tileData.type.Contains("Flower"))
        {
            newTile.setVal(tileData.value);
            // Flower.Add(newTile.gameObject); // Flower list is assumed to exist
        }
        // else
        // {
        //     Obstacle.Add(newTile.gameObject); // Obstacle list is assumed to exist
        // }
        // tileList.Add(newTile.gameObject);

    }
}



    public void SaveData(ref GameData data) {
        data.tileSaveList = new List<TileSaveData>();

        foreach (GameObject obj in tileList) {
            var tile = obj.GetComponent<Tile>();
            if (tile == null) {
                continue;
            }

            TileSaveData tiledata = new TileSaveData {
                
                x = obj.transform.position.x,
                y = obj.transform.position.y,
                z = obj.transform.position.z,
                type = tile.type,
                value = tile.value,
                maxValue = tile.maxValue
                
            };

            data.tileSaveList.Add(tiledata);
        }

}

    void GenerateGrid(){
        for (int x = 0; x < width; x++) {
            for (int y = 0; y < height; y++) {
                
                var spawnedTile = Instantiate(tilePreFab, new Vector3(x,y), Quaternion.identity);
                spawnedTile.name = $"Tile {x} {y}";
                spawnedTile.type = "Grass";

                // var isOffSet = (x % 2 == 0 && y%2 !=0) || (y%2 == 0 && x%2 != 0);
                // spawnedTile.Init(isOffSet);
                // spawnedTile.setVal(0);
            }
        }
    
        camera.transform.position = new Vector3((float)width/2 -0.5f, (float)height / 2 -0.5f,-20);
        Random rnd = new Random();
        int seed = rnd.Next(1111111,9999999);

        GenerateElements(seed);

   }

   void GenerateElements(int modifier){
    long flowerMod = (long) Math.Pow(modifier, difficultyModifier);
 
    GenerateFlowers(flowerMod);
    GenerateTrees();


   }


   void GenerateFlowers(long mod){
   
    int area = height*width;
    int y = 0;
    Random rnd1 = new Random();
    long flowSeed = (long) Math.Pow(rnd1.Next(1111111,9999999), difficultyModifier); // this is where the flowers spawn
    long flowtemp = flowSeed; // this is the type of flower generated
    
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
        
    if(!isOccupied(z,y))
    {
        flowerType(zzz, z, y);
        // WILL ADD FLOWERS TO KD TREE
        kdTree.Insert(z,y);
    }
    
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
        int treeCount = 0;
        int dige = rnd.Next(1,100000000);
        if(dige%2==0) { dige++;}
        for (int x = 0; x < area; x += (int)(rnd.Next(1,100))) { 
            int z = x % width; 
            y = x / width; 
            int dice = dige;
            if (!IsFlowerTile(z, y) && !isHive(z, y)) { 
                treeCount++; //Debug.Log($"update tree gang "+treeCount+" whatever the hell dige is "+dige);
                // Debug.Log($"Attempting to instantiate treeTile at {z}, {y}");
                var spawnedTile = Instantiate(treeTile, new Vector3(z, y), Quaternion.identity); 
                spawnedTile.name = $"treeTile {z} {y}"; 
                int a = z;
                int b = y;
                long dagep = dice*treeCount;

                for(int i = 0; i < 5; i++) {
                                    
                    //figure out algorithm to fix your stupid treecount 
                    switch(((dagep)%10)%4) {
                        case 0:
                            
                            if (!isOccupied(a+1,b) && isInBounds(a+1,b)) {
                                a++;
                                spawnTree(a,b);
                                dagep/=10;
                                // Debug.Log($"right"+a+" "+b+" "+dagep);
                            }
                            break;
                        
                        case 1:
                            if (!isOccupied(a,b+1) && isInBounds(a,b+1)) {
                                b++;
                                spawnTree(a,b);

                                dagep /= 10;
                                // Debug.Log($"up"+a+" "+b+" "+dagep);
                            }
                            break;    

                        case 2:
                            if (!isOccupied(a-1,b) && isInBounds(a-1,b)) {
                                a--;
                                spawnTree(a,b);

                                dagep /= 10;
                                // Debug.Log($"left"+a+" "+b+" "+dagep);
                            }
                            break;

                        case 3:
                            if (!isOccupied(a,b-1) && isInBounds(a,b-1)) {
                                b--;
                                spawnTree(a,b);
                                dagep /= 10;
                                // Debug.Log($"down"+a+" "+b+" "+dagep);
                            }
                            break;    

                        }
                        // Debug.Log($" before "+dagep);
                        // dagep/=10;
                        // Debug.Log($" after divide "+dagep);
                    }

                
                }
            } 
    }

    public void spawnTree(int a, int b) {
        var spawnedTile4 = Instantiate(treeTile, new Vector3(a, b), Quaternion.identity); 
        spawnedTile4.name = $"treeTile {a} {b}";
        spawnedTile4.type = "Tree";
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
        var tileName = $"treeTile {x} {y}";
        GameObject tileObject = GameObject.Find(tileName);
        if (tileObject != null && (tileObject.name.Contains("tree")))
        {
            return true;
        }
        return false;
    }


    //check if tile is occupied
    bool isOccupied(int x, int y) {
        return (IsTreeTile(x,y) || IsFlowerTile(x,y) || isHive(x,y));
    }

    //check if tile is in bounds
    bool isInBounds(int x, int y) {
        if ((x < west || x >= width) || (y < bottom || y >= height))
            return false;
        return true;
    }

    bool isHive(int x, int y) {
        if((x >= 23 && x <= 26) && (y >= 23 && y <= 26)) {
            return true;
        }
        return false;
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
            spawnedTile1.type = "Grass";
            var spawnedTile2 = Instantiate(tilePreFab, new Vector3(width-1,i,1), Quaternion.identity);
            spawnedTile2.name = $"Tile {width-1} {i}";
            spawnedTile2.type = "Grass";

            
        }
        for ( int j = west+1; j < width; j++) {
            var spawnedTile3 = Instantiate(tilePreFab, new Vector3(j,height-1,1), Quaternion.identity);
            spawnedTile3.name = $"Tile {j} {height-1}";
            spawnedTile3.type = "Grass";

            var spawnedTile4 = Instantiate(tilePreFab, new Vector3(j,bottom,1), Quaternion.identity);
            spawnedTile4.name = $"Tile {j} {bottom}";
            spawnedTile4.type = "Grass";
           
            
        }
    }


    //uses modifier to spawn the right color flower
    void flowerType(long zzz, int z, int y) { 
        if(zzz>=0 && zzz <= 39) {
            var spawnedTile = Instantiate(pinkFlower, new Vector3(z,y), Quaternion.identity);
            spawnedTile.name = $"FlowerTile {z} {y}";
            spawnedTile.type = "PinkFlower"; 

            spawnedTile.setVal(50);
        }
        else if (zzz >= 40 && zzz <= 69) {
            var spawnedTile = Instantiate(blueFlower, new Vector3(z,y), Quaternion.identity);
            spawnedTile.name = $"FlowerTile {z} {y}";
            spawnedTile.type = "BlueFlower";

            spawnedTile.setVal(80);
        }
        else if (zzz >= 70 && zzz <= 89) {
            var spawnedTile = Instantiate(sunFlower, new Vector3(z,y), Quaternion.identity);
            spawnedTile.name = $"FlowerTile {z} {y}";
            spawnedTile.type = "SunFlower";

            spawnedTile.setVal(100);
        }
         else {
            var spawnedTile = Instantiate(redFlower, new Vector3(z,y), Quaternion.identity);
            spawnedTile.name = $"FlowerTile {z} {y}";
            spawnedTile.type = "RedFlower";

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

    public void rocks() {
        Debug.Log($" rocks coming ");
        Random rnd = new Random();
        for (int i = 0; i < (height-bottom)*(width-west); i += (int) rnd.Next(1,300) ) {
            int x = ( i % (west - width)) + west;
            int y = (i/ (height-bottom) + bottom);
            if(!isOccupied(x,y)) {
                var spawnedTile4 = Instantiate(rockTile, new Vector3(x, y), Quaternion.identity); 
                spawnedTile4.name = $"rockTile {x} {y}";
                spawnedTile4.type = "Rock";
            }

        }
    }
   
   private Tile GetPrefabByType(string type) {
    switch (type) {
        case "PinkFlower": return pinkFlower;
        case "BlueFlower": return blueFlower;
        case "SunFlower": return sunFlower;
        case "RedFlower": return redFlower;
        case "Tree": return treeTile;
        case "Rock": return rockTile;
        case "Grass": return tilePreFab; 
        default: return null;
    }
}





   
   
//    void GenerateTrees(){ 
//     int area = height * width; 
//     int y = 0; 
//    Random rnd = new Random(); 
// //    long treeSeed = (long)Math.Pow(rnd.Next(1111111, 9999999), difficultyModifier); 
// //    long treetemp = treeSeed; 
//    for (int x = 0; x < area; x += (int)(rnd.Next(1,100))) { 
//     int z = x % width; 
//     y = x / width; 
//     // long zzz = treetemp % 100; 
//     // treetemp /= 100; 
//     // if (treetemp == 0) { 
//     //     treeSeed = treeSeed * 2; 
//     //     treetemp = treeSeed; 
//     //     } 
//      if (!IsFlowerTile(z, y)) { 
//         Debug.Log($"Attempting to instantiate treeTile at {z}, {y}");
//         var spawnedTile = Instantiate(treeTile, new Vector3(z, y), Quaternion.identity); 
//         spawnedTile.name = $"treeTile {z} {y}"; 
            
//          }
//              } 
//    }
   
   
   
   
//     bool IsFlowerTile(int x, int y) { // Check if a tile at position (x, y) is a flower tile 
//    var tileName = $"FlowerTile {x} {y}";
//     GameObject tileObject = GameObject.Find(tileName); 
//     if (tileObject != null && (tileObject.name.Contains("Flower"))) 
//     { 
//         return true; 
//     } 
//     return false;
// }
}

[System.Serializable]
public class TileSaveData
{
    public float x, y, z;
    public int value;
    public int maxValue;
    public string type;
}