using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceCounter : MonoBehaviour 
{
    public static ResourceCounter Instance { get; set; }


    private int nectar;
    private int honey;
    private int wax;
    private int dna;
    
    private int hiveLevel;

    [SerializeField] private Text ResourcesText;
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

        honey = 50;
        nectar = 0;
        wax = 50;
        dna = 0;
        UpdateResText();
        InvokeRepeating("UpdateResText", 1.0f, 1.0f);
        // InvokeRepeating("LoseHoney", 3.0f, 3.0f);
    }


    // passively lose honey based on hive level (currently never changes)
    private void LoseHoney()
    {
        changeHoney(-1 * hiveLevel);
    }

    //Update text 
    private void UpdateResText()
    {
        ResourcesText.text = ("RESOURCES:   Nectar: " + getNectar() + "   Honey: " + getHoney() + "   Wax: " + getWax() + "   DNA: " + getDna() );
    }

    // nectar
    public int getNectar() {
        return nectar;
    }
    public void setNectar(int setTo) {
        nectar = setTo;
    }
    public void changeNectar(int plusBy) {
        if (nectar + plusBy >= 0)
        {
            nectar += plusBy;
        }
    }

    // honey
    public int getHoney() {
        return honey;
    }
    public void setHoney(int setTo) {
        honey = setTo;
    }
    public void changeHoney(int plusBy) {
        if (honey + plusBy >= 0)
        {
            honey += plusBy;
        }
    }

    // wax
    public int getWax() {
        return wax;
    }
    public void setWax(int setTo) {
        wax = setTo;
    }
    public void changeWax(int plusBy) {
        if (wax + plusBy >= 0)
        {
            wax += plusBy;
        }
    }

    // dna
    public int getDna() {
        return dna;
    }
    public void setDna(int setTo) {
        dna = setTo;
    }
    public void changeDna(int plusBy) {
        if (dna + plusBy >= 0)
        {
            dna += plusBy;
        }
    }
}
