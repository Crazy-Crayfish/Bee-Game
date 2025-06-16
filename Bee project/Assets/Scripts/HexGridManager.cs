using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class HexGridManager : MonoBehaviour, IDataPersistence
{
   
    public static HexGridManager Instance { get; set; }
    // Start is called before the first frame update
    [SerializeField] public int width, height;
    [SerializeField] private HexTile hexTilePreFab;
    public Sprite queen, nursery, production, storage;
    public GameObject[,] tileList;
    public GameObject HoveredTile;
    public GameObject DraggedTile;
    public GameObject GridQueen;
    public List<GameObject> hextiles;
    public List<GameObject> buildings;
    private void Awake()
    {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
        } else {
            Instance = this;
        }
    }
    public void LoadData(GameData data)
    {
        tileList = new GameObject[width, height]; // Initialize the array with correct dimensions
 
        // tileList = data.tileList;
        Debug.Log("Hexgridmanager received the data");
 
        production = data.production;
        storage = data.storage;
        nursery = data.nursery;
        queen = data.queen;
       
        foreach (HexTileSaveData tileData in data.hexTileSaveList)
        {
            // HexTile prefab = whichPreFab(tileData.tileType); // MUST BE IMPLEMENTED IN ORDER FOR CHAMBERS TO LOAD
            // if (prefab == null) continue;
            // hard coded to be hexTilePreFab which should be changed
 
            if (hexTilePreFab == null) Debug.Log("smth is up");
            HexTile newTile = Instantiate(hexTilePreFab, new Vector3(tileData.worldX, tileData.worldY, 1.0f), Quaternion.identity);
           
            newTile.name = $"HexTile {tileData.worldX} {tileData.worldY}";
            newTile.tileType = tileData.tileType;
            if ( whichPreFab(newTile.tileType) != null) {
                newTile.tileImage.gameObject.GetComponent<SpriteRenderer>().sprite = whichPreFab(newTile.tileType);
            }
            newTile.gridX = tileData.gridX;
            newTile.gridY = tileData.gridY;
            newTile.isBuiltOn = tileData.isBuiltOn;
            newTile.transform.localScale = new Vector3(2f, 2f, 1f);
            // newTile.transform.SetParent(this.gameObject.transform, false);
            
            tileList[newTile.gridX, newTile.gridY] = newTile.gameObject;
            if (tileData.isBuiltOn)
            {
                string path = tileData.structureName;
                var newBuilding = Instantiate(Resources.Load(path) as GameObject,
                        new Vector3(newTile.transform.position.x,newTile.transform.position.y, -5),
                                            Quaternion.identity);
                buildings.Add(newBuilding);
            }
        }
    }
    public void SaveData(ref GameData data) {
        data.hexTileSaveList = new List<HexTileSaveData>();
        // data.tileList = tileList;
 
        foreach (GameObject obj in hextiles) {
            var hextile = obj.GetComponent<HexTile>();
            if (hextile == null) {
                continue;
            }
            // Debug.Log(obj);
            // Debug.Log(hextile.structure); //.GetComponent<Structure>().prefabName
            HexTileSaveData tiledata = new HexTileSaveData {
               
                worldX = obj.transform.position.x,
                worldY = obj.transform.position.y,
                // z = obj.transform.position.z,
                tileType = hextile.tileType,
                isBuiltOn = hextile.isBuiltOn,
                gridX = hextile.gridX,
                gridY = hextile.gridY,

                
                
            };
            if (!tiledata.isBuiltOn || hextile.structure == null)
            {
                tiledata.structureName = null;
            }
            else
            {
                tiledata.structureName = hextile.structure.GetComponent<Structure>().prefabName;
            }
 
            data.hexTileSaveList.Add(tiledata);
       
        }
    data.production = production;
    data.nursery = nursery;
    data.queen = queen;
    data.storage = storage;
}
    void Start() {
 
        if (DataPersistenceManager.instance == null || !DataPersistenceManager.instance.IsNewGame)
        {
        // It's a load operation — skip new unit creation
            return;
        }
        GenerateGrid();
        HoveredTile = null;
    }
 
    public bool[] getEggList()
    {
        //Debug.Log(GridQueen.GetComponent<Queen>().eggPositions.Length);
        return GridQueen.GetComponent<Queen>().eggPositions;
    }
   
    public GameObject getEggAtPos(int pos)
    {
        // Debug.Log("HexGridManager thinks eggPos length is: " + GridQueen.GetComponent<Queen>().eggPositions.Length);
        // Debug.Log("HexGridManager thinks eggPos is: " + GridQueen.GetComponent<Queen>().eggPositions);
        // string a = "HexGridManager thinks eggPos is: ";
        // foreach(bool b in GridQueen.GetComponent<Queen>().eggPositions)
        // {
        //     a += b + " ";
        // }
        // Debug.Log(a);
 
        return GridQueen.GetComponent<Queen>().eggObjects[pos];
    }
 
    public void removeEggAtPos(int pos)
    {
        bool[] newList = new bool[GridQueen.GetComponent<Queen>().eggPositions.Length];
        for (int i = 0; i < GridQueen.GetComponent<Queen>().eggPositions.Length; i++)
        {
            if (i == pos)
            {
                newList[i] = false;
            }
            else
            {
                newList[i] = GridQueen.GetComponent<Queen>().eggPositions[i];
            }
        }
        GridQueen.GetComponent<Queen>().eggPositions = newList;
        GridQueen.GetComponent<Queen>().eggObjects[pos].SetActive(false);
        Destroy(GridQueen.GetComponent<Queen>().eggObjects[pos]);
        GridQueen.GetComponent<Queen>().eggObjects[pos] = null;
 
    }
   
 
 
    // EXAMPLE OF HOW POSITIONS WORK:
    /*   0
    * 5     1
    *   Src  
    * 4     2
    *    3
    */
    public GameObject getNeighborAtPos(GameObject sourceTile, int pos)
    {
        int sourceX = sourceTile.GetComponent<HexTile>().gridX;
        int sourceY = sourceTile.GetComponent<HexTile>().gridY;
        if (pos == 0) { return tileList[sourceX, sourceY + 2]; }
        if (pos == 3) { return tileList[sourceX, sourceY - 2]; }
       
        if (sourceY % 2 == 0)
        {
            if (pos == 1) { return tileList[sourceX, sourceY + 1]; }
            if (pos == 2) { return tileList[sourceX, sourceY - 1]; }
            if (pos == 4) { return tileList[sourceX - 1, sourceY - 1]; }
            if (pos == 5) { return tileList[sourceX - 1, sourceY + 1]; }
        }
        if (pos == 1) { return tileList[sourceX + 1, sourceY + 1]; }
        if (pos == 2) { return tileList[sourceX + 1, sourceY - 1]; }    
        if (pos == 4) { return tileList[sourceX, sourceY - 1]; }
        if (pos == 5) { return tileList[sourceX, sourceY + 1]; }
        // IF BAD INPUT, JUST RETURN THE SOURCE TILE
        return sourceTile;
    }
 
    public void buildChamber(GameObject chamber, int rotation) // rotation clockwise
    {
        // Build a chamber at level 1, centered at hoveredTile target location
        if (HoveredTile != null && HoveredTile.GetComponent<HexTile>() != null)
        {
           
            int sourceX = HoveredTile.GetComponent<HexTile>().gridX;
            int sourceY = HoveredTile.GetComponent<HexTile>().gridY;
            GameObject neighbor1 = getNeighborAtPos(HoveredTile, (1 + rotation) % 6);
            GameObject neighbor2 = getNeighborAtPos(HoveredTile, (2 + rotation) % 6);
            // getNeighborAtPos(HoveredTile, 4).GetComponent<HexTile>().changeType(chamber);
            if (HoveredTile.GetComponent<HexTile>().tileType == "empty" && neighbor1.GetComponent<HexTile>().tileType == "empty" && neighbor2.GetComponent<HexTile>().tileType == "empty")
            {
                HoveredTile.GetComponent<HexTile>().changeType(chamber);
                neighbor1.GetComponent<HexTile>().changeType(chamber);
                neighbor2.GetComponent<HexTile>().changeType(chamber);
            }
            neighbor1.GetComponent<HexTile>().activateHighlight(false);
            neighbor2.GetComponent<HexTile>().activateHighlight(false);
            // Debug.Log("x y" + sourceX + "   " + sourceY);
        }
 
    }
   
 
    // Not implemented
    public void upgradeChamber(GameObject chamber, int currentLevel)
    {
        switch(currentLevel)
        {
            case 1:
                break;
            case 2:
                break;
            case 3:
                break;
        }
    }
    public void buildOnHoveredTile(GameObject building)
    {
        // Debug.Log("building");
        if (HoveredTile != null)
        {
            if (building.GetComponent<Structure>().type == HoveredTile.GetComponent<HexTile>().tileType
            && HoveredTile.GetComponent<HexTile>().isBuiltOn == false
            && building.GetComponent<Structure>().waxCost <= ResourceCounter.Instance.getWax()
            && building.GetComponent<Structure>().honeyCost <= ResourceCounter.Instance.getHoney())
            {
                var newBuilding = Instantiate(building,
                        new Vector3(HoveredTile.transform.position.x,HoveredTile.transform.position.y, -5),
                                            Quaternion.identity);
                buildings.Add(newBuilding);
               
                HoveredTile.GetComponent<HexTile>().isBuiltOn = true;
                HoveredTile.GetComponent<HexTile>().structure = newBuilding;
                ResourceCounter.Instance.changeWax(-building.GetComponent<Structure>().waxCost);
                ResourceCounter.Instance.changeHoney(-building.GetComponent<Structure>().honeyCost);
            }
        }
 
    }
 
   void GenerateGrid(){
    tileList = new GameObject[width, height];
    for (int x = 0; x < width; x++) {
        for (int y = 0; y < height; y++) {
            var isOffSet = (y%2 != 0);
            // WIP: scalar does nothing right now,
            // want to eventually make it so the grid appearence supports different hex sizes
           // float scalar = (float)hexTilePreFab.GetComponent<Renderer>().transform.localScale.sqrMagnitude;
            float scalar = (float)1.0;
            float tempX = (float)x * (float) 1.3 * scalar;
            float tempY = (float)y * (float) 0.35 * scalar;
            if (isOffSet) {
                tempX += (float)0.65 * scalar;
            }
            var spawnedTile = Instantiate(hexTilePreFab, new Vector3(tempX,tempY), Quaternion.identity); //this.gameObject.transform.position.x +
            // hextiles.Add(spawnedTile);
            spawnedTile.transform.SetParent(this.gameObject.transform, false);
            spawnedTile.Init(isOffSet, x, y);
            tileList[x, y] = spawnedTile.gameObject;
            spawnedTile.name = $"HexTile {x} {y}";
           
           
 
        }
    }
    GameObject centerTile = tileList[width / 2, height / 2];
    // Debug.Log(centerTile.GetComponent<HexTile>().gridX + ", " + centerTile.GetComponent<HexTile>().gridY);
    // GameObject queenTile = (GameObject)Resources.Load("Rsources/QueenTile",typeOf(GameObject));
    GameObject queenTile = Resources.Load("QueenTile", typeof(GameObject)) as GameObject;
    GameObject queen = Resources.Load("Queen", typeof(GameObject)) as GameObject;
    centerTile.GetComponent<HexTile>().changeType(queenTile);
    
    GameObject queenInstance = Instantiate(queen,
                new Vector3(centerTile.transform.position.x, centerTile.transform.position.y, -5),
                Quaternion.identity);

    centerTile.GetComponent<HexTile>().structure = queenInstance;
    centerTile.GetComponent<HexTile>().isBuiltOn = true;
    GridQueen = queenInstance;
    getNeighborAtPos(centerTile, 0).GetComponent<HexTile>().changeType(queenTile);
    getNeighborAtPos(centerTile, 1).GetComponent<HexTile>().changeType(queenTile);
    getNeighborAtPos(centerTile, 2).GetComponent<HexTile>().changeType(queenTile);
    getNeighborAtPos(centerTile, 3).GetComponent<HexTile>().changeType(queenTile);
    getNeighborAtPos(centerTile, 4).GetComponent<HexTile>().changeType(queenTile);
    getNeighborAtPos(centerTile, 5).GetComponent<HexTile>().changeType(queenTile);
    
    // mark eggs too but not here 
 
   }
 
 
   public Sprite whichPreFab(string tileType) {
    switch(tileType) {
        case "queen": return queen;
        case "storage": return storage;
        case "nursery": return nursery;
        case "production": return production;
 
    }
 
    return null;
 
   }
 
   public void setPreFab(string tileType, Sprite sprite) {
        switch(tileType) {
        case "queen": queen = sprite; break;
        case "storage": storage = sprite; break;
        case "nursery": nursery = sprite; break;
        case "production": production = sprite; break;
 
    }
   }
   
}
 
[System.Serializable]
public class HexTileSaveData
{
    public float worldX;
    public float worldY;
    public string tileType;
    public bool isBuiltOn;
    public int gridX;
    public int gridY;
    public string structureName;
    // public string structureType;  // Save structure info if needed (name or ID)
}