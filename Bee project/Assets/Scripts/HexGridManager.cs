using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexGridManager : MonoBehaviour
{
    
    public static HexGridManager Instance { get; set; }
    // Start is called before the first frame update
    [SerializeField] private int width, height;
    [SerializeField] private HexTile hexTilePreFab;
    public GameObject[,] tileList;
    public GameObject HoveredTile;
    public GameObject DraggedTile;
        private void Awake() 
    {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
        } else {
            Instance = this;
        }
    }
    void Start() {
        GenerateGrid();
        HoveredTile = null;
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
            if (building.GetComponent<Structure>().cost <= ResourceCounter.Instance.getWax() && building.GetComponent<Structure>().type == HoveredTile.GetComponent<HexTile>().tileType)
            {
                var newBuilding = Instantiate(building, 
                        new Vector3(HoveredTile.transform.position.x,HoveredTile.transform.position.y, -5), 
                                            Quaternion.identity);
                ResourceCounter.Instance.changeWax(-building.GetComponent<Structure>().cost);
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
            var spawnedTile = Instantiate(hexTilePreFab, new Vector3(this.gameObject.transform.position.x + tempX,tempY), Quaternion.identity);
            spawnedTile.Init(isOffSet, x, y);
            tileList[x, y] = spawnedTile.gameObject;
            spawnedTile.name = $"HexTile {x} {y}";
            

        }
    }
    GameObject centerTile = tileList[width / 2, height / 2];
    Debug.Log(centerTile.GetComponent<HexTile>().gridX + ", " + centerTile.GetComponent<HexTile>().gridY);
    // GameObject queenTile = (GameObject)Resources.Load("Rsources/QueenTile",typeOf(GameObject));
    GameObject queenTile = Resources.Load("QueenTile", typeof(GameObject)) as GameObject;
    GameObject queen = Resources.Load("Queen", typeof(GameObject)) as GameObject;
    centerTile.GetComponent<HexTile>().changeType(queenTile);
    Instantiate(queen, 
                new Vector3(centerTile.transform.position.x, centerTile.transform.position.y, -5), 
                Quaternion.identity);
    getNeighborAtPos(centerTile, 0).GetComponent<HexTile>().changeType(queenTile);
    getNeighborAtPos(centerTile, 1).GetComponent<HexTile>().changeType(queenTile);
    getNeighborAtPos(centerTile, 2).GetComponent<HexTile>().changeType(queenTile);
    getNeighborAtPos(centerTile, 3).GetComponent<HexTile>().changeType(queenTile);
    getNeighborAtPos(centerTile, 4).GetComponent<HexTile>().changeType(queenTile);
    getNeighborAtPos(centerTile, 5).GetComponent<HexTile>().changeType(queenTile);
   }
   
}
