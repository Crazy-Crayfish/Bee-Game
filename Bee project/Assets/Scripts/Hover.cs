using UnityEngine;

public class Hover : MonoBehaviour
{
    public Popup popupManager;
    public string message;

    private void OnMouseEnter()
    {
        popupManager.ShowPopup(message, transform.position);
    }

    private void OnMouseExit()
    {
        popupManager.HidePopup();
    }
}