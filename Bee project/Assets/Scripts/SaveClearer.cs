using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveClearer : MonoBehaviour
{
    public static SaveClearer Instance{get; private set;}
    private string fileName = "gameData.json";  
    
    private void Awake()
    {
            if (Instance != null && Instance != this) {
                Destroy(gameObject);
            } else {
                Instance = this;
            }
    }
    public void ClearSaves()
    {
        string fullPath = Path.Combine(Application.persistentDataPath, fileName);
        Debug.Log(fullPath);
        if (File.Exists(fullPath))
        {
            File.Delete(fullPath);
            Debug.Log("gameData.json deleted successfully.");
        }
        else
        {
            Debug.Log("No gameData.json file found to delete.");
        }
    }
}
