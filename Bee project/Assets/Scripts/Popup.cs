using UnityEngine;
using UnityEngine.UI;

public class Popup : MonoBehaviour
{
    public GameObject popupPanel;
    public Text popupText;
    public static Popup Instance { get; set; }

    private void Awake() 
    {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
        } else {
            Instance = this;
        }
    }

    void Start()
    {
        popupPanel.gameObject.SetActive(false);
    }

    public void ShowPopup(string message, Vector3 worldPosition)
    {
        Debug.Log("showing popup");
        popupText.text = message;
        popupPanel.transform.position = Camera.main.WorldToScreenPoint(worldPosition + new Vector3(0, 1f, 0)); // offset above object
        popupPanel.gameObject.SetActive(true);
    }

    public void HidePopup()
    {
        popupPanel.gameObject.SetActive(false);
    }
}

// better version too lazy to finish now
// using UnityEngine;
// using UnityEngine.UI;

// public class PopupManager : MonoBehaviour
// {
//     // public GameObject popupPanel;
//     public Text popupText;

//     void Start()
//     {
//         // popupText.gameObject.SetActive(false);
//     }

//     public void ShowPopup(string message, Vector3 worldPosition)
//     {
//         GameObject newPopUp = Instantiate()
//         popupText.text = message;
//         popupText.transform.position = Camera.main.WorldToScreenPoint(worldPosition + new Vector3(0, 1f, 0)); // offset above object
//         popupText.gameObject.SetActive(true);
//     }

//     public void HidePopup()
//     {
//         popupText.gameObject.SetActive(false);
//     }
// }