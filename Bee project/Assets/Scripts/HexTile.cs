using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexTile : MonoBehaviour
{
    [SerializeField] private Color baseColor, alternateColor;
    [SerializeField] private SpriteRenderer renderer;
    [SerializeField] private GameObject highlight;

    
    public void Init(bool isOffset){
        renderer.color = isOffset ? alternateColor : baseColor;
    }
    public void changeSprite(Sprite newSprite) {
        renderer.sprite = newSprite;
    }
    void OnMouseEnter() {
        highlight.SetActive(true);
    }
    void OnMouseExit() {
        highlight.SetActive(false);
    }
}
