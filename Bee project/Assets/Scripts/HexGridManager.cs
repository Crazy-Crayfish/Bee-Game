using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexGridManager : MonoBehaviour
{
    
    public static HexGridManager Instance { get; set; }
    // Start is called before the first frame update
    [SerializeField] private int width, height;
    [SerializeField] private HexTile hexTilePreFab;
    public GameObject HoveredTile;
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

    public void buildOnHoveredTile(GameObject building)
    {
        // Debug.Log("building");
        if (HoveredTile != null)
        {
            if (building.GetComponent<Structure>().waxCost <= ResourceCounter.Instance.getWax()
            && building.GetComponent<Structure>().honeyCost <= ResourceCounter.Instance.getHoney())
            {
                var newBuilding = Instantiate(building, 
                        new Vector3(HoveredTile.transform.position.x,HoveredTile.transform.position.y, -5), 
                                            Quaternion.identity);
                ResourceCounter.Instance.changeWax(-building.GetComponent<Structure>().waxCost);
                ResourceCounter.Instance.changeHoney(-building.GetComponent<Structure>().honeyCost);
            }
        }

    }

   void GenerateGrid(){
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
            spawnedTile.Init(isOffSet);
            spawnedTile.name = $"HexTile {x} {y}";
            

        }
    }
   }
   
}
