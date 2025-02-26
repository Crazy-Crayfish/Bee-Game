using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StructureConstruct : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private bool selected;
    private Collision2D currentTile;
    [SerializeField] private GameObject structRef;
    [SerializeField] private GameObject buildingPreFab;
    // Start is called before the first frame update
    void Start()
    {
        selected = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!selected)
        {
            this.transform.position = structRef.transform.position;
        }
    }

    public void OnBeginDrag(PointerEventData data)
    {
        selected = true;
    }

    public void OnDrag(PointerEventData data)
    {
        this.transform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);
    }


    public void OnEndDrag(PointerEventData data)
    {
        selected = false;
        HexGridManager.Instance.buildOnHoveredTile(buildingPreFab);
    }

    
    
}
