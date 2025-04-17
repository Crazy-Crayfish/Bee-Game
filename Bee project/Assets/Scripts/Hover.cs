using UnityEngine;

public class Hover : MonoBehaviour
{
    // public Popup popupManager;
    [SerializeField] private string message;

    private void OnMouseEnter()
    {
        Popup.Instance.ShowPopup(message, transform.position);
    }

    private void OnMouseExit()
    {
        Popup.Instance.HidePopup();
    }
}