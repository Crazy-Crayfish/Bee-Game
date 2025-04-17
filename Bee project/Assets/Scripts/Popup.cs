using UnityEngine;
using UnityEngine.UI;

public class Popup : MonoBehaviour
{
    public GameObject popupPanel;
    public Text popupText;

    void Start()
    {
        popupText.gameObject.SetActive(false);
    }

    public void ShowPopup(string message, Vector3 worldPosition)
    {
        popupText.text = message;
        popupText.transform.position = Camera.main.WorldToScreenPoint(worldPosition + new Vector3(0, 1f, 0)); // offset above object
        popupText.gameObject.SetActive(true);
    }

    public void HidePopup()
    {
        popupText.gameObject.SetActive(false);
    }
}