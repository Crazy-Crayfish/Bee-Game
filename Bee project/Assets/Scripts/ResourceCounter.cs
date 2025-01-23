<<<<<<< Updated upstream
=======
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
>>>>>>> Stashed changes

public class ResourceCounter {
    private int nectar = 0;
    private int honey = 0;
    private int wax = 0;
    private int dna = 0;

<<<<<<< Updated upstream
    //nectar
=======
public class ResourceCounter : MonoBehaviour 
{
    public static ResourceCounter Instance { get; set; }


    private static int nectar;
    private static int honey;
    private static int wax;
    private static int dna;
    
    private int hiveLevel;

    //public Text onScreen;

    private void Awake() 
    {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
        } else {
            Instance = this;
        }
        // DontDestroyOnLoad(gameObject);
    }
    public void Start()
    {
        hiveLevel = 1;

        nectar = 0;
        honey = 50;
        wax = 0;
        dna = 0;
        //UpdateResText();
        InvokeRepeating("UpdateResText", 1.0f, 1.0f);
        // InvokeRepeating("LoseHoney", 3.0f, 3.0f);
    }

    // passively lose honey based on hive level (currently never changes)
    private void LoseHoney()
    {
        changeHoney(-1 * hiveLevel);
    }
    
    // private void UpdateResText()
    // {
    //     onScreen.text = "RESOURCES: \tNectar: " + getNectar() + "   Honey: " + getHoney() + "   Wax: " + getWax() + "   DNA: " + getDna() + "\n";
        
    // }

    public static string Resources() {
        return "RESOURCES:   Nectar: " + nectar + "  Honey: " + honey + "  Wax: " + wax + "  DNA: " + dna ;
    }

    // nectar
>>>>>>> Stashed changes
    public int getNectar() {
        return nectar;
    }
    public void setNectar(int setTo) {
        nectar = setTo;
    }
    public void addNectar(int plusBy) {
        nectar += plusBy;
    }
    public void takeNectar(int minusBy) {
        nectar -= minusBy;
    }

    //honey
    public int getHoney() {
        return honey;
    }
    public void setHoney(int setTo) {
        honey = setTo;
    }
    public void addHoney(int plusBy) {
        honey += plusBy;
    }
    public void takeHoney(int minusBy) {
        honey -= minusBy;
    }

    //wax
    public int getWax() {
        return wax;
    }
    public void setWax(int setTo) {
        wax = setTo;
    }
    public void addWax(int plusBy) {
        wax += plusBy;
    }
    public void takeWax(int minusBy) {
        wax -= minusBy;
    }

<<<<<<< Updated upstream
    //dna
    public int getDna() {
=======
    // dna
    public  int getDna() {
>>>>>>> Stashed changes
        return dna;
    }
    public void setDna(int setTo) {
        dna = setTo;
    }
    public void addDna(int plusBy) {
        dna += plusBy;
    }
    public void takeDna(int minusBy) {
        dna -= minusBy;
    }
}
