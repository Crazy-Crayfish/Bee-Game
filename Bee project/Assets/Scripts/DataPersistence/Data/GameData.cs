using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData 
{
    public List<UnitSaveData> allFriendlyUnits;
    public List<TileSaveData> tileSaveList;
    public List<GameObject> hextiles;
    public List<GameObject> buildings;
    public List<HexTileSaveData> hexTileSaveList;
    public List<EnemySaveData> allEnemyUnits;
    
    public Sprite queen, production, storage, nursery;
    public int nectar, honey, DNA, wax;
    public float waveTimer;
    public int waveNum;
    
    public GameData() {
        // honey = 50;
        // nectar = 0;
        // DNA = 0;
        // wax = 50;
        allFriendlyUnits = new List<UnitSaveData>();
        tileSaveList = new List<TileSaveData>();
        allEnemyUnits = new List<EnemySaveData>();
        hexTileSaveList = new List<HexTileSaveData>();
    }

    // public void Awake() {
    //     instance = this;
    // }

    // public void newGame() {
    //     this.gameData - new gameData();
    // }

    // public void loadGame() {
    //     if (this.gameData == null) {
    //         Debug.Log("no Data found");
    //         newGame();
    //     }
    // }

    // public void saveGame() {
    //     // this.allUnitsList = UnitSelectionManager.Instance.allUnitsList;
    //     // this.Flower = GridManager.Instance.Flower;
    //     // this.Obstacle = GridManager.Instance.Obstacle;
    //     // this.allFriendlyUnits = FriendlyUnitCreator.Instance.
    //     // this.hextiles = HexGridManager.Instance.hextiles;
    //     // this.buildings = HexGridManager.Instance.buildings;
    //     // this.nectar = ResourceCounter.Instance.nectar;
    //     // this.DNA = ResourceCounter.Instance.DNA;
    //     // this.wax = ResourceCounter.Instance.wax;
    //     // this.honey = ResourceCounter.Insert.honey;

    // }
}
