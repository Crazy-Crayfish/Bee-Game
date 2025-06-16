using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Structure : MonoBehaviour
{

    [SerializeField] public string type;
    [SerializeField] public int waxCost;
    [SerializeField] public int honeyCost;
    public string prefabName;
    // Start is called before the first frame update
    void Start()
    {
        string n = gameObject.name;
        int index = n.IndexOf("(");
        this.prefabName = n.Substring(0, index);
        Debug.Log(this.prefabName);
    }
}
