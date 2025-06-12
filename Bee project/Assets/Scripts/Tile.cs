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
    public string type;

    void Start()
    {
        if (GridManager.Instance == null)
    {
        Debug.LogError("GridManager.Instance is null in Tile.Start!");
    }
        GridManager.Instance.tileList.Add(gameObject);
    }

    public void Init(bool isOffset)
    {
        renderer.color = isOffset ? alternateColor : baseColor;
    }

    void OnMouseEnter()
    {
        highlight.SetActive(true);
        MenuManager.Instance.showSelectedRes(this);
    }

    void OnMouseExit()
    {
        highlight.SetActive(false);
        MenuManager.Instance.showSelectedRes(null);
    }

    void OnMouseOver()
    {
        MenuManager.Instance.showSelectedRes(this);
    }

    public void setVal(int x)
    {
        value = x;
        maxValue = x;
    }

    public void decrement()
    {
        value--;
        if (value == 0)
        {
            this.gameObject.SetActive(false);
            Destroy(this);
            GridManager.Instance.tileList.Remove(gameObject);
        }
    }

    // === SAVE SYSTEM METHODS BELOW ===

    // public TileSaveData GetSaveData()
    // {
    //     return new TileSaveData
    //     {
    //         x = transform.position.x,
    //         y = transform.position.y,
    //         z = transform.position.z,
    //         value = this.value,
    //         maxValue = this.maxValue,
    //         type = this.type
    //     };
    // }

    // public void LoadFromData(TileSaveData data)
    // {
    //     transform.position = new Vector3(data.x, data.y, data.z);
    //     value = data.value;
    //     maxValue = data.maxValue;
    //     type = data.type;
    // }

}
