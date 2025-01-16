using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private Color baseColor, alternateColor;
    [SerializeField] private SpriteRenderer renderer;
    [SerializeField] private GameObject highlight;
    public int value;
    public int maxValue;
    public void Init(bool isOffset){
        renderer.color = isOffset ? alternateColor : baseColor;

    }
    
    void OnMouseEnter() {
        highlight.SetActive(true);
        MenuManager.Instance.showSelectedRes(this);
        
    }
    void OnMouseExit() {
        highlight.SetActive(false);
        MenuManager.Instance.showSelectedRes(null);
    }

    public void setVal(int x) {
        value = x;
        maxValue = x;
    }
    public void OnMouseDown()
    {
        if (value > 9) {
            value -= 10;
            MenuManager.Instance.showSelectedRes(this);

        }
        else if(value > 0) {
            value = 0;
            MenuManager.Instance.showSelectedRes(this);
        }
        else{
            return;
            }
    }
}
