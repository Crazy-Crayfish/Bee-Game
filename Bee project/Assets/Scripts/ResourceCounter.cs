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
    private int DNA;
    private int maxNectar;
    private int maxHoney;
    private int maxWax;
    private int maxDNA;
    
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
        DNA = 0;
        maxHoney = 100;
        maxNectar = 100;
        maxWax = 100;
        maxDNA = 100;
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
        ResourcesText.text = ("RESOURCES:   Nectar: " + getNectar() + "/" + getMaxNectar() + "   Honey: " + getHoney() + "/" + getMaxHoney() + "   Wax: " + getWax() + "/" + getMaxWax() + "   DNA: " + getDNA() +  "/" + getMaxDNA());
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

    // DNA
    public int getDNA() {
        return DNA;
    }
    public void setDNA(int setTo) {
        DNA = setTo;
    }
    public void changeDNA(int plusBy) {
        if (DNA + plusBy >= 0)
        {
            DNA += plusBy;
        }
    }
    // maxNectar
    public int getMaxNectar() {
        return maxNectar;
    }
    public void setMaxNectar(int setTo) {
        maxNectar = setTo;
    }
    public void changeMaxNectar(int plusBy) {
        if (maxNectar + plusBy >= 0)
        {
            maxNectar += plusBy;
        }
    }

    // maxHoney 
    public int getMaxHoney() {
        return maxHoney;
    }
    public void setMaxHoney(int setTo) {
        maxHoney = setTo;
    }
    public void changeMaxHoney(int plusBy) {
        if (maxHoney + plusBy >= 0)
        {
            maxHoney += plusBy;
        }
    }

    // maxWax
    public int getMaxWax() {
        return maxWax;
    }
    public void setMaxWax(int setTo) {
        maxWax = setTo;
    }
    public void changeMaxWax(int plusBy) {
        if (maxWax + plusBy >= 0)
        {
            maxWax += plusBy;
        }
    }

    // maxDNA
    public int getMaxDNA() {
        return maxDNA;
    }
    public void setMaxDNA(int setTo) {
        maxDNA = setTo;
    }
    public void changeMaxDNA(int plusBy) {
        if (maxDNA + plusBy >= 0)
        {
            maxDNA += plusBy;
        }
    }
}
