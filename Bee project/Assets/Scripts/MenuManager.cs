using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MenuManager : MonoBehaviour
{


  public static MenuManager Instance;

  [SerializeField] private GameObject lfVal;


  void Awake(){
    Instance = this;
  }


  public void showSelectedRes(Tile tile) {


    if(tile == null|| tile.maxValue == 0) {
        lfVal.SetActive(false);
        return;
    }
    //     if(tile.value==50) {
    //     tile.setVal(10000);
    // } USE FOR TESTING 

    lfVal.GetComponentInChildren<Text>().text = "Resources "+tile.value.ToString()+"/" + tile.maxValue.ToString();
    lfVal.SetActive(true);
  }
}
