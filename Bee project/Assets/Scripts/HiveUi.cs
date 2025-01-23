using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiveUi : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        ScreenManager.Instance.hiveUiList.Add(gameObject);
    }

    // Update is called once per frame
    // void OnDestroy()
    // {
    //     ScreenManager.Instance.hiveUiList.Remove(gameObject);
    // }
}
