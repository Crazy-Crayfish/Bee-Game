using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenManager : MonoBehaviour
{
    [SerializeField] private GameObject ui1;
    [SerializeField] private GameObject ui2;
    [SerializeField] private GameObject ui3;
    [SerializeField] private GameObject ui4;
    [SerializeField] private GameObject ui5;
    [SerializeField] private GameObject ui6;
    [SerializeField] private GameObject ui7;
    [SerializeField] private GameObject ui8;
    [SerializeField] private GameObject ui9;
    [SerializeField] private GameObject ui10;
    [SerializeField] private GameObject ui11;
    [SerializeField] private GameObject ui12;
    [SerializeField] private Transform camerra;
    public static ScreenManager Instance;
    private bool inHive;

    // Start is called before the first frame update
    void Awake(){
        Instance = this;
        inHive = false;
        return;
    }
    public void newState(){
        if(inHive) {
            inHive = false;
            hidden();
            return;
        }
        if(!inHive) {
            inHive = true;
            unhide();
            return;
        }
    }
    public void hidden(){
        ui1.SetActive(false);
        ui2.SetActive(false);
        ui3.SetActive(false);
        ui4.SetActive(false);
        ui5.SetActive(false);
        ui6.SetActive(false);
        ui7.SetActive(false);
        ui8.SetActive(false);
        ui9.SetActive(false);
        ui10.SetActive(false);
        ui11.SetActive(false);
        ui12.SetActive(false);
        camerra.transform.position = new Vector3(20, 20, -20);


    }
    public void unhide(){
        ui1.SetActive(false);
        ui2.SetActive(true);
        ui3.SetActive(true);
        ui4.SetActive(true);
        ui5.SetActive(true);
        ui6.SetActive(true);
        ui7.SetActive(true);
        ui8.SetActive(true);
        ui9.SetActive(true);
        ui10.SetActive(true);
        ui11.SetActive(true);
        ui12.SetActive(true);

        camerra.transform.position = new Vector3(4020, 20, -20);
            }
    // Update is called once per frame

}
