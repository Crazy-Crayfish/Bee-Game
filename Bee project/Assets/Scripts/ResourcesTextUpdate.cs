using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourcesTextUpdate : MonoBehaviour
{

    public Text ResourcesText;

    // Start is called before the first frame update
    void Start()
    {
        ResourcesText.text = ResourceCounter.Resources();
    }

    // Update is called once per frame
    void Update()
    {
        ResourcesText.text = ResourceCounter.Resources();
    }
}
