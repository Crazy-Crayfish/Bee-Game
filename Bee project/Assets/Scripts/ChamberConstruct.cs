using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class ChamberConstruct : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private bool selected;
    private Collision2D currentTile;
    public int rotation;
    [SerializeField] private GameObject chamberRef;
    [SerializeField] private GameObject chamberPreFab;
    
    // Start is called before the first frame update
    void Start()
    {
        selected = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (selected && Input.GetKeyDown(KeyCode.R))
        {
            HexGridManager.Instance.getNeighborAtPos(HexGridManager.Instance.HoveredTile, (rotation + 1) % 6).GetComponent<HexTile>().activateHighlight(false);
            HexGridManager.Instance.getNeighborAtPos(HexGridManager.Instance.HoveredTile, (rotation + 3) % 6).GetComponent<HexTile>().activateHighlight(true);
            rotation += 1;
        }
        if (!selected)
        {
            this.transform.position = chamberRef.transform.position;
            
        }
    }

    public void OnBeginDrag(PointerEventData data)
    {
        selected = true;
        rotation = 0;
        HexGridManager.Instance.DraggedTile = this.gameObject;
    }

    public void OnDrag(PointerEventData data)
    {
        this.transform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);

    }


    public void OnEndDrag(PointerEventData data)
    {
        selected = false;
        HexGridManager.Instance.buildChamber(chamberPreFab, rotation);
        HexGridManager.Instance.DraggedTile = null;
    }

    
    
}
