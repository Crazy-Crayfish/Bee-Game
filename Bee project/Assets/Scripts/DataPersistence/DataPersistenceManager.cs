using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DataPersistenceManager : MonoBehaviour
{
    [Header("File Storage Config")]
    [SerializeField] private string fileName;
    private GameData gameData;
    private List<IDataPersistence> dataPersistenceObjects;
    private FileDataHandler dataHandler;
    public bool IsNewGame { get; private set; } = true;


    public static DataPersistenceManager instance{get; private set;}

   private void Awake()
   {
    if (instance != null) {
        Debug.LogError("Found more than one DataPersistenceManager in the scene.");
    }
    instance = this;
   }

   public void Start()
   {
    this.dataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
    this.dataPersistenceObjects = FindAllDataPersistenceObjects();
    LoadGame();
   }

   public void NewGame()
   {
        this.gameData = new GameData();
        IsNewGame = true;
        gameData.tutorialDone = false;
        
   }

   public void LoadGame()
   {
    this.gameData = dataHandler.Load();
    // LOAD ANY SAVE DATA FROM a file using the data handler
    if (this.gameData == null) {
        Debug.Log("No Data was found. Initializing data to defaults.");
        NewGame();
        
    }

    else {
        IsNewGame = false;
    }
    // push loaded data to all other scripts that need the data

    foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects) {
        dataPersistenceObj.LoadData(gameData);
    }
    Debug.Log("Data sent Allegedly");
   }

   public void SaveGame(){
    // pass the data to other scripts so they can update it
    // save that data to a file using the data handler 

        foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects) {
        dataPersistenceObj.SaveData(ref gameData);
    }

    Debug.Log("Saved Data");

    // save that data to a file using handler

    dataHandler.Save(gameData);
   }

   public void OnApplicationQuit()
   {
    SaveGame();
   }

   public List<IDataPersistence> FindAllDataPersistenceObjects() {
    IEnumerable<IDataPersistence> dataPersistenceObjects = FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistence>();
   return new List<IDataPersistence>(dataPersistenceObjects);
   }
}
