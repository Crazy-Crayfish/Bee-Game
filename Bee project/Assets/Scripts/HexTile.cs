using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexTile : MonoBehaviour
{
   // [SerializeField] private Color baseColor, alternateColor;
    // [SerializeField] private SpriteRenderer renderer;
    [SerializeField] private GameObject highlight;
    [SerializeField] private GameObject tileImage;
    public int gridX;
    public int gridY;
    public string tileType; // "empty", "queen", "storage", "production", "nursery"
    
    public void Init(bool isOffset, int x, int y){
       // renderer.color = isOffset ? alternateColor : baseColor;
       tileType = "empty";
       gridX = x;
       gridY = y;
    }

    // public void changeSprite(Sprite newSprite) {
    //     renderer.sprite = newSprite;
    // }

    public void changeType(GameObject newTilePreFab) {
        // newSprite = newTilePreFab.GetComponent<SpriteRenderer>().sprite;
        tileImage.gameObject.GetComponent<SpriteRenderer>().sprite = newTilePreFab.GetComponent<SpriteRenderer>().sprite; /////////////////////// Renderer is changing sprite but nothing changes visually. Look at renderer docs
        // Debug.Log("x y" + this.gridX + "   " + this.gridY + "    Sprite: " + tileImage.gameObject.GetComponent<SpriteRenderer>().sprite);

        tileType =  newTilePreFab.GetComponent<Chamber>().type;
    }
    

    void OnMouseEnter() {
        activateHighlight(true);
        HexGridManager.Instance.HoveredTile = this.gameObject;
        if (HexGridManager.Instance.DraggedTile != null)
        {
            int rot = HexGridManager.Instance.DraggedTile.GetComponent<ChamberConstruct>().rotation;
            HexGridManager.Instance.getNeighborAtPos(this.gameObject, (1 + rot) % 6).GetComponent<HexTile>().activateHighlight(true);
            HexGridManager.Instance.getNeighborAtPos(this.gameObject, (2 + rot) % 6).GetComponent<HexTile>().activateHighlight(true);
        }
        // Debug.Log("x y" + this.gridX + "   " + this.gridY + "    Sprite: " + this.gameObject.GetComponent<SpriteRenderer>().sprite);
    }
    
    public void activateHighlight(bool onOff)
    {
        highlight.SetActive(onOff);
    }

    // private void updateHighlights(bool onOff)
    // {

    // }

    void OnMouseExit() {
        activateHighlight(false);
        if (HexGridManager.Instance.DraggedTile != null)
        {
            int rot = HexGridManager.Instance.DraggedTile.GetComponent<ChamberConstruct>().rotation;
            HexGridManager.Instance.getNeighborAtPos(this.gameObject, (1 + rot) % 6).GetComponent<HexTile>().activateHighlight(false);
            HexGridManager.Instance.getNeighborAtPos(this.gameObject, (2 + rot) % 6).GetComponent<HexTile>().activateHighlight(false);
        }
    }
}
