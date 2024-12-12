using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileDetector : MonoBehaviour
{
    [SerializeField] private Collision2D Chamber;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //this.transform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        //Chamber = col;
        //this.transform.localPosition = Chamber.transform.position;
    }
}
